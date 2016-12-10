#ifndef _DEFINES_
#define _DEFINES_

enum TimerState
{
  PAUSED = 0,
  STARTING,
  RACING,
  REPORTING
};

const String _timeState[] = { "$WAIT", "$STARTING", "$RACING", "$REPORTING" };

// Version
const String _version = "$LapThat! v1.0";

// number of racers, 1 to 6
const byte _rxCount =  1; 

// default peak detection based on experiments
const unsigned short _edgeThreshold = 50; // rssi is over noise threshold
const unsigned short _peakThreshold = 80;
const unsigned long _noiseCountThreshold = 200;

const unsigned long _peakMinWidthReject = 200;
const unsigned long _peakMaxWidthReject = 2500;

const unsigned short _noiseFloor = 140;

// Lap stuff
const unsigned long _minLaptime = 4000;

#endif // _DEFINES_

