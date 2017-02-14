#ifndef _SERIALLNK_
#define _SERIALLNK_

#include "defines.h"
#include "rssi.h"

const byte _maxBuff = 10;

char buff[_maxBuff];
bool startLn = false;
bool endLn = false;
byte insert = 0;

bool newState = false;
    
void serialTick()
{
  // Check queue
  unsigned short availBytes = Serial.available();
  if (availBytes > 0)
  {
    // Cap max read for timing
    if (availBytes > 255) availBytes = 255;
    for(unsigned short b = 0; !endLn && (b < availBytes); b++)
    {
      char ch = Serial.read();
      if (startLn)
      {  
        if (ch == '\r')
        {
          // Sentence end
          endLn = true;
        } else {
          // Fill buffer
          buff[insert++] = ch;
                    
          // Read garbage so start fresh
          if (insert >= _maxBuff) startLn = false;
        }
      } else {
        // Starts with $
        if (ch == '$')
        {
          startLn = true;
          insert = 0;
        }
      }
    }
    
    // Valid sentence?
    if (startLn && endLn && (insert > 0))
    {
      // Store header as caps
      char header = buff[0];
      if (header > 'Z') header -= 32;

      // Decode value, or find ?
      bool query = false;
      int newValue = 0;
      if (insert > 1)
      {
        if (buff[1] == '?')
        {
          query = true;
        } else {
          buff[insert] = 0; // null terminate
          newValue = atoi(&buff[1]);
        }
      }      
      switch(header)
      {
        case 'V':
          if (query) Serial.println(_version);
          break;
        case 'H':
          Serial.println("$V?   - query version info");
          Serial.println("$C?   - query number of racers");
          Serial.println("$D    - toggle live RSSI data");
          Serial.println("$L    - toggle extended Lap info");
          Serial.println("$B    - toggle lap beeps");
          Serial.println("$S(?) - change/query race state");
          Serial.println("$M(?) - change/query minimum lap time (ms)");
          Serial.println("$N(?) - change/query noise time (ms)");
          Serial.println("$P(?) - change/query RSSI peak threshold");
          Serial.println("$E(?) - change/query RSSI edge threshold");
          Serial.println("$Q(?) - change/query minimum RSSI peak width (ms)");
          Serial.println("$W(?) - change/query maximum RSSI peak width (ms)");
          break;
        case 'C':
          if (query) Serial.println("$C?" + String(_rxCount));
          break;
        case 'D':
          debugRSSI = !debugRSSI;
          break;
        case 'L':
          extendedLapInfo = !extendedLapInfo;
          break;
        case 'B':
          lapBeeps = !lapBeeps;
          break;
        case 'S':
          if (query)
          {
            Serial.println(_timeState[timerState]);
          } else {
            // New state          
            newState = true;
          }
          break;
        case 'P':
          if (query)
          {
            Serial.println("$P?" + String(peakThreshold));
          } else {
            peakThreshold = (unsigned short)newValue;
          }
          break;
        case 'E':
          if (query)
          {
            Serial.println("$E?" + String(edgeThreshold));
          } else {
            edgeThreshold = (unsigned short)newValue;
          }
          break;
        case 'Q':
          if (query)
          {
            Serial.println("$Q?" + String(peakMinWidthReject));
          } else {
            peakMinWidthReject = (unsigned long)newValue;
          }
          break;
        case 'W':
          if (query)
          {
            Serial.println("$W?" + String(peakMaxWidthReject));
          } else {
            peakMaxWidthReject = (unsigned long)newValue;
          }
          break;
        case 'M':
          if (query)
          {
            Serial.println("$M?" + String(minLaptime));
          } else {
            minLaptime = (unsigned long)newValue;
          }
          break;
        case 'N':
          if (query)
          {
            Serial.println("$N?" + String(noiseCountThreshold));
          } else {
            noiseCountThreshold = (unsigned long)newValue;
          }
          break;
        default:
          // Unhandled
          String error = "$ERR(";
          error += header;
          error += ")";
          Serial.println(error);
          break;
      }
      startLn = false;
      endLn = false;
    }
  }
}

#endif // _SERIALLNK_

