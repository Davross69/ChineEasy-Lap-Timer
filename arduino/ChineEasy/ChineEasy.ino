// ChineEasy Lap Timer
//
// By Kev Cross, 2016

#include "serialLink.h"

const byte _buzzerPin = 11;

const unsigned long _lapInterval = 100; // about 3 per 2 second
const unsigned long _fastInterval = 150; // about 3 per 2 second
const unsigned long _slowInterval = 500;

unsigned long lastMillis = 0;
unsigned long buzzMillis = 0; // buzzer management
short buzzCount = -1; // -1 is off, other values manage beeps

unsigned long raceCountMillis = 0; // time to start race

void setup()
{
  // Buzzer
  pinMode(_buzzerPin, OUTPUT);

  // Turn off Buzzer
  digitalWrite(_buzzerPin, LOW);
 
  // May want a special AI reference
  analogReference(DEFAULT);
  
  // Analog inputs for RSSI
  for(byte rx = 0; rx < _rxCount; rx++)
  {
    pinMode(rx, INPUT);
  }

  rssiInit();
  
  // For debug
  Serial.begin(115200);
  while (!Serial); // Wait until Serial is ready - Leonardo

   // Initialised...
  Serial.println("$INIT");
}

void loop()
{
  // Time since boot
  unsigned long currentMillis = millis();
  if (currentMillis == lastMillis)
  {
    // Use downtime to read serial
    serialTick();
  } else {
    // State machine
    switch(timerState)
    {
      case PAUSED:
        // Manage button
        if (newState)
        {
          // Random 3-6 sec pause
          raceCountMillis = currentMillis + random(_longInterval, 2 * _longInterval);

          // Trigger beeps
          buzzMillis = currentMillis;
          buzzCount = 0;

          // Starting race soon...
          timerState = STARTING;
        }
        break;
      case STARTING:
        // Manage button
        if (newState)
        {
          // Kill buzzer if triggered
          digitalWrite(_buzzerPin, LOW);
          buzzCount = -1;
          
          // Aborting race...
          timerState = PAUSED;    
        } else {
          if (currentMillis >= raceCountMillis)
          {
            // Reset timing for new race
            rssiReset(currentMillis);

            // Trigger start beep
            buzzMillis = currentMillis;
            buzzCount = 0;

            // Go, go, go...
            timerState = RACING;
          } else {
            // Do some beeping before start
            if (buzzCount >= 0) ManageBuzzer(currentMillis, 4, _fastInterval, _slowInterval);
          }
        }
        break;     
     case RACING:
        // Manage button
        if (newState)
        {
          // Kill buzzer if triggered
          digitalWrite(_buzzerPin, LOW);
          buzzCount = -1;
          
          // Ending race...
          timerState = REPORTING;
        } else {
          // Process rssi - true if someone laps
          if (rssiTick(currentMillis))
          {
            // Trigger beep on lap if not alreay beeping
            if (lapBeeps && (buzzCount == -1))
            {
              buzzMillis = currentMillis;
              buzzCount = 0;
            }
          }
          // Race beeps, start or lap
          if (buzzCount >= 0)
          {
            // Must be a start beep
            if (currentMillis < raceCountMillis + minLaptime)
            {
              // Bleep start alert here
              ManageBuzzer(currentMillis, 1, _longInterval, 0);
            } else {
              // Lap bleep
              ManageBuzzer(currentMillis, 1, _lapInterval, 0); 
            }
          }
        }
        break;
     case REPORTING:
        // Manage button
        if (newState) 
        {
          // Clear laps
          resetLaps();
          
          // Waiting to race...
          timerState = PAUSED;
        } else {
          // Spew forth a summary
        }
        break;
    }
    if (newState)
    {
      // Report new state
      Serial.println(_timeState[timerState]);
      newState = false;
    }
    
    // 1 ms loop
    lastMillis = currentMillis;
  }
}

void ManageBuzzer(unsigned long currentMillis, byte beeps, unsigned long buzzOnTime, unsigned long buzzOffTime)
{
  if (currentMillis >= buzzMillis)
  {
    // Toggle state
    bool buzzOn = ((buzzCount % 2)  == 0);
    if (buzzCount < 2 * beeps)
    {
      if (buzzOn)
      {
        // Set next off time
        buzzMillis = currentMillis + buzzOnTime;
      } else {
        // Set next on time
        buzzMillis = currentMillis + buzzOffTime;
      }
      buzzCount++;
      digitalWrite(_buzzerPin, buzzOn);
    } else {
      // Stop beeps
      digitalWrite(_buzzerPin, LOW);
      buzzCount = -1;
    }
  }
}


