#ifndef _RSSI_
#define _RSSI_

#include "defines.h"
#include "lapinfo.h"

TimerState timerState = PAUSED;

// RSSI data
// http://dev.theomader.com/gaussian-kernel-calculator/

#define GAUSS11 1

#ifdef GAUSS7

const byte _filterSize =  7; // gauss filter - 1 sigma
const unsigned long _gaussWeight[] = { 383, 242, 61, 6 };

#endif // GAUSS7

#ifdef GAUSS11

const byte _filterSize =  11; // gauss filter - 2 sigma
const unsigned long _gaussWeight[] = { 198, 176, 122, 66, 28, 9 };

#endif // GAUSS11

const unsigned long _gaussTotal = 1000; // _gaussWeight[0] + (2 * (_gaussWeight[1] + _gaussWeight[...] + _gaussWeight[_windowSize]))
const byte _windowSize =  (_filterSize - 1) / 2; // gauss filter window

enum PulseState
{
  NOISE = 0,
  EDGE,
  PEAK
};

// Buffers
unsigned long startMillis = 0; // time of start
unsigned long frameCount = 0; // acquisition frame counter 
unsigned short gauss[_filterSize][_rxCount];
unsigned long rxTime[_filterSize];
unsigned short noiseFloor[_rxCount];

// Set defaults for peak finding
unsigned short edgeThreshold = _edgeThreshold;
unsigned short peakThreshold = _peakThreshold;

unsigned long peakMinWidthReject = _peakMinWidthReject;
unsigned long peakMaxWidthReject = _peakMaxWidthReject;
unsigned long noiseCountThreshold = _noiseCountThreshold;

// Edge detection
PulseState pulseLast[_rxCount]; // the last reading
unsigned long peakStart[_rxCount]; // set this once
unsigned long peakEnd[_rxCount]; // may rise several times
unsigned long noiseCount[_rxCount];

unsigned short peakRSSI[_rxCount]; // for reporting
unsigned long peakPos[_rxCount];

// Spit out raw RSSI 
bool debugRSSI = false;

void rssiInit()
{
  for(byte rx = 0; rx < _rxCount; rx++)
  {
    noiseFloor[rx] = _noiseFloor;
  }
}

void rssiReset(unsigned long currentMillis)
{
  frameCount = 0;
  startMillis = currentMillis;

  // Clear RSSI buffers & edge detectors
  for(byte rx = 0; rx < _rxCount; rx++)
  {
    pulseLast[rx] = NOISE;
    peakStart[rx] = 0;
    peakEnd[rx] = 0;
    noiseCount[rx] = 0;
    
    peakRSSI[rx] = 0;
    peakPos[rx] = 0;
   
    for(byte gaussIns = 0; gaussIns < _filterSize; gaussIns++)
    {
      rxTime[gaussIns] = 0;
      gauss[gaussIns][rx] = 0;
    }
  }
}

bool rssiTick(unsigned long currentMillis)
{
  bool lapFound = false;
  
  // Read RSSI's in one go to best sync timers
  byte insert = frameCount % _filterSize; // insert point
  rxTime[insert] = currentMillis - startMillis;
  for(byte rx = 0; rx < _rxCount; rx++)
  {
    // Fill gauss filter buffers
    gauss[insert][rx] = (unsigned short)(analogRead(rx)) - noiseFloor[rx];
  }

  // Perform filter after buffer filled
  if (frameCount >= _filterSize)
  {
    // Filter all racers - NB offset due to filter run-up
    byte gaussCentre = (frameCount - _windowSize) % _filterSize;
    
    String stringPulse = "";
    if (debugRSSI) stringPulse += String(rxTime[gaussCentre]);
    
    for(byte rx = 0; rx < _rxCount; rx++)
    {  
      // Accumulate from middle of filter window
      unsigned long accum = _gaussWeight[0] * (unsigned long)gauss[gaussCentre][rx];
      
      for(byte offset = 1; offset <= _windowSize; offset++)
      {
        byte plusIndex = (frameCount - _windowSize + offset) % _filterSize;
        byte minusIndex = (frameCount - _windowSize - offset) % _filterSize;
        accum += _gaussWeight[offset] * (unsigned long)gauss[plusIndex][rx];
        accum += _gaussWeight[offset] * (unsigned long)gauss[minusIndex][rx];
      }
      accum /= _gaussTotal;
      unsigned short filteredRSSI = (unsigned short)accum;

      // For live RSSI
      if (debugRSSI)
      {
        stringPulse += ",";
        stringPulse += String(filteredRSSI);  
      }
        
      // Noise, edge or peak?
      PulseState pulseNow = NOISE;
      if (filteredRSSI > edgeThreshold) pulseNow = EDGE;
      if (filteredRSSI > peakThreshold) pulseNow = PEAK;

      // Wipe noise counter
      if (pulseNow != NOISE) noiseCount[rx] = 0;
            
      // Peak machine
      switch(pulseLast[rx])
      {
        case NOISE: 
          // Track continuous noise after a peak
          if ((pulseNow == NOISE) && (peakEnd[rx] > 0))noiseCount[rx]++;
          // NB: Deliberate flow through, no break  
        case EDGE:
           // Rises to peak for first time
          if ((pulseNow == PEAK) && (peakStart[rx] == 0)) peakStart[rx] = rxTime[gaussCentre];
          break;     
        case PEAK:
          // It's no longer peaked so store endtime
          if (pulseNow != PEAK) peakEnd[rx] = rxTime[gaussCentre];
          break;
      }
      pulseLast[rx] = pulseNow;
      
      // Sanity check noiseCount - should never happen
      if (noiseCount[rx] > peakMaxWidthReject)
      {
        peakStart[rx] = 0;
        peakEnd[rx] = 0;
        peakRSSI[rx] = 0;
        peakPos[rx] = 0;
        noiseCount[rx] = 0;
      }
      
      // Start of a peak
      if (peakStart[rx] > 0)
      {
        // Track max peak
        if (filteredRSSI > peakRSSI[rx])
        {
          peakRSSI[rx] = filteredRSSI;
          peakPos[rx] = rxTime[gaussCentre];
        }
        
        // Process peak after continuous noise count reached
        // avoids brief dips in RSSI prematurely tripping peak logic
        if ((noiseCount[rx] > noiseCountThreshold) && (peakEnd[rx] > 0))
        {
          // Sanity check start/end values
          if (peakEnd[rx] > peakStart[rx])
          {
            unsigned long peakWidth = peakEnd[rx] - peakStart[rx];
            if ((peakWidth >= peakMinWidthReject) && (peakWidth <= peakMaxWidthReject))
            {
              // addLap our peak centre - either pealPos or middle of peakWidth
              lapFound = addLap(rx, peakPos[rx], peakRSSI[rx], peakStart[rx], peakEnd[rx]);
            }
          }
          peakStart[rx] = 0;
          peakEnd[rx] = 0;
          peakRSSI[rx] = 0;
          peakPos[rx] = 0;
          noiseCount[rx] = 0;
        }
      }
    }
    if (debugRSSI) Serial.println(stringPulse);
  }
  frameCount++;

  return lapFound;
}

#endif // _RSSI_

