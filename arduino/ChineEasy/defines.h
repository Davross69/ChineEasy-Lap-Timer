#ifndef _DEFINES_
#define _DEFINES_

// Timer runs with no RSSI
//#define DEBUG 1

// RSSI averages
#define RSSI_AVERAGES 4

// Pick a gauss filter
#define GAUSS7 1
//#define GAUSS11 1

enum TimerState
{
  PAUSED = 0,
  STARTING,
  RACING,
  REPORTING
};

const String _timeState[] = { "$S1", "$S2", "$S3", "$S4" };

// Version
const String _version = "ChineEasy v1.0";

// number of racers, 1 to 6
const byte _rxCount =  1; 

// Spit out raw RSSI 
byte debugRSSI = _rxCount;

// default peak detection based on experiments
const unsigned short _edgeThreshold = 180; // rssi is over noise threshold
const unsigned short _peakThreshold = 200;
const unsigned long _noiseCountThreshold = 200;

const unsigned long _peakMinWidthReject = 200;
const unsigned long _peakMaxWidthReject = 2500;

const unsigned short _noiseFloor = 140;

// Lap stuff
const unsigned long _minLaptime = 4000;
const unsigned long _longInterval = 3000; // for race start delay
const unsigned long _startInterval = 1500; // for race start beep

#endif // _DEFINES_

