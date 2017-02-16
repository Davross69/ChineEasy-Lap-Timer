using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChineEasy
{
    public class RacerInfo
    {
        // Can't exceed this
        public const int _maxRacers = 6;
        private const String blankLap = "--";
        private const String blankTime = "-- : ---";

        private String name = "Unknown";
        private Color uiColour = new Color();
        private List<ulong> lapTimes = new List<ulong>();

        private int bestLapIndex = 0;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String BestLap
        {
            get { return (bestLapIndex > 0) ? (bestLapIndex + 1).ToString() : blankLap; }
        }

        public double BestDeltaTime
        {
            get { return (bestLapIndex > 0) ? lapTimes[bestLapIndex] - lapTimes[bestLapIndex - 1] : double.MaxValue; }
        }

        public String BestLapDelta
        {
            get { return (bestLapIndex > 0) ? LapDelta(bestLapIndex) : blankTime; }
        }

        public String CurrentLap
        {
            get { return (lapTimes.Count > 0) ? lapTimes.Count.ToString() : blankLap; }
        }

        public String CurrentLapTime
        {
            get { return (lapTimes.Count > 0) ? LapTime(lapTimes.Count - 1) : blankTime; }
        }

        public String LapTime(int index)
        {
            return TimeTosshh(lapTimes[index]);
        }

        public String LapDelta(int index)
        {
            return (index > 0) ? TimeTosshh(lapTimes[index] - lapTimes[index - 1]) : blankTime;
        }

        public String CurrentLapDelta
        {
            get { return (lapTimes.Count > 0) ? LapDelta(lapTimes.Count - 1) : blankTime; }
        }

        public int LapCount
        {
            get { return lapTimes.Count; }
        }

        public Color UIColour
        {
            get { return uiColour; }
            set { uiColour = value; }
        }

        public void AddLap(int lap, ulong time)
        {     
            int count = lapTimes.Count; 
            if (count < 1)
            {
                bestLapIndex = 0;
            }
            else
            {                
                double delta = time - lapTimes[count - 1];
                if (delta < BestDeltaTime) bestLapIndex = count;
            }
            lapTimes.Add(time);
        }

        public void ClearLaps()
        {
            if (lapTimes.Count > 0)
            {
                lapTimes.Clear();
                bestLapIndex = 0;
            }
        }

        public String TimeTosshh(ulong time)
        {
            String sshh = (time / 1000).ToString("D2");
            sshh += " : ";
            sshh += (time % 1000).ToString("D3");
            return sshh;
        }
    }
}
