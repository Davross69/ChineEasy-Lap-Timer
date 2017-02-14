#ifndef _LAPINFO_
#define _LAPINFO_

#include "defines.h"

const byte _maxLaps = 30; // governed by memory

unsigned long minLaptime = _minLaptime;

unsigned long lapTimes[_maxLaps][_rxCount];
unsigned short bestLap[_rxCount];
byte insertLap[_rxCount];

bool extendedLapInfo = true;
bool lapBeeps = true;

void resetLaps()
{
  // Clear lap data arrays
  for(byte rx = 0; rx < _rxCount; rx++)
  {
    bestLap[rx] = 0;
    insertLap[rx] = 0;
    for(byte lap = 0; lap < _maxLaps; lap++)
    {
      lapTimes[lap][rx] = 0;
    }
  }
}

bool addLap(byte rx, unsigned long pkPos, unsigned short pkRSSI, unsigned long pkStart, unsigned long pkEnd)
{
  // Alternative lapTime
  unsigned long lapTime = (pkStart + pkEnd) / 2;
  
  bool goodLap = false;
  if (insertLap[rx] == 0)
  {
    // First lap
    goodLap = true;
    lapTimes[0][rx] = lapTime;
    bestLap[rx] = lapTime;

    // Spew forth laptime
    String stringPeak = "$R";
    stringPeak += rx;
    stringPeak += ",L1,T";
    stringPeak += String(lapTime);
    stringPeak += ",D0";
    Serial.println(stringPeak); 
      
    /* No client version - simple text debug
    String stringPeak = "$R";
    stringPeak += rx;
    stringPeak += ": Lap\t1\t@ Time:\t";
    stringPeak += String(1E-3 * (float)lapTime, 2);
    
    if (extendedLapInfo)
    {
      stringPeak += "\t\t\t(Peak: ";
      stringPeak += String(pkRSSI);
      stringPeak += ",\t";
      stringPeak += String(pkPos);
      stringPeak += ",\t";
      stringPeak += String(pkStart);
      stringPeak += ",\t";
      stringPeak += String(pkEnd);
      stringPeak += ",\t";
      stringPeak += String(pkEnd - pkStart);
      stringPeak += ")";
    }    
    Serial.println(stringPeak);
    */
      
    insertLap[rx]++;
  } else {
    // lap data arrays are cyclic in case of exceeding _maxLaps
    unsigned short delta = lapTime - lapTimes[(insertLap[rx] - 1) % _maxLaps][rx];

    // Stop people just flying circles
    if (delta >= minLaptime)
    {
      goodLap = true;
      lapTimes[insertLap[rx] % _maxLaps][rx] = lapTime;
      bool hotLap = false;
      if (delta < bestLap[rx])
      {
        bestLap[rx] = delta;
        hotLap = true;
      }

      // Spew forth laptime
      String stringPeak = "$R";
      stringPeak += rx;
      stringPeak += ",L";
      stringPeak += String(insertLap[rx] + 1);      
      stringPeak += ",T";
      stringPeak += String(lapTime);
      stringPeak += ",D";
      stringPeak += String(delta); 
      Serial.println(stringPeak); 
      
      /* No client version - simple text debug
      String stringPeak = "R";
      stringPeak += rx;
      stringPeak += ": Lap\t";      
      stringPeak += String(insertLap[rx] + 1);
      stringPeak += "\t@ Time:\t"; 
      stringPeak += String(1E-3 * (float)lapTime, 2);
      stringPeak += "\t Delta:\t";
      stringPeak += String(1E-3 * (float)delta, 2);
      if (hotLap) stringPeak += String(" **");

      if (extendedLapInfo)
      {
        stringPeak += "\t(Peak: ";
        stringPeak += String(pkRSSI);
        stringPeak += ",\t";
        stringPeak += String(pkPos);
        stringPeak += ",\t";
        stringPeak += String(pkStart);
        stringPeak += ",\t";
        stringPeak += String(pkEnd);
        stringPeak += ",\t";
        stringPeak += String(pkEnd - pkStart);
        stringPeak += ",\tBest: ";
        stringPeak += String(1E-3 * (float)bestLap[rx], 2);
        stringPeak += ")";
      }      
      Serial.println(stringPeak);*/

      insertLap[rx]++;
    }
  }
  return goodLap;
}

#endif // _LAPINFO_

