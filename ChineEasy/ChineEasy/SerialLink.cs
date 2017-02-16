using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Ports;

namespace ChineEasy
{
    public class SerialLink
    {
        private MainForm parent = null;
        private SerialPort serialPort = new SerialPort();
        private String comPort = "";
        private Queue<byte> receivedData = new Queue<byte>();
        private int racerCount = -1;
        private String readBuffer = "";

        public String COMPort       
        {
            get { return comPort; }
        }

        public int GetRacers()
        {
            return racerCount;
        }

        public bool Initialise(MainForm parent)
        {
            // Allows poking form
            this.parent = parent;

            // Set the read/write timeouts
            serialPort.BaudRate = 115200;            
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;

            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            comPort = "";
            for(int p = 0; (comPort.Length == 0) && (p < ports.Length); p++)
            {
                serialPort.PortName = ports[p];
                System.Diagnostics.Debug.WriteLine("Trying " + serialPort.PortName);
                try
                {
                    // Open port and try handshaking
                    serialPort.Open();
                    try
                    {
                        serialPort.Write("$V?\r\n");
                        try
                        {
                            // Check for reply
                            String version = serialPort.ReadLine();
                            if (version.Length > 3)
                            {
                                if (version.Contains("ChineEasy v1.0"))
                                {
                                    comPort = ports[p];
                                    System.Diagnostics.Debug.WriteLine("Found ChineEasy on " + comPort);
                                }
                            }
                        }
                        catch (TimeoutException exTime)
                        {
                            // Times out
                            System.Diagnostics.Debug.WriteLine("Bad Version");
                        }
                        if (comPort.Length > 0)
                        {
                            serialPort.Write("$C?\r\n");
                            try
                            {
                                // Check for reply
                                String racers = serialPort.ReadLine();
                                if (racers.Length > 3)
                                {
                                    if (racers.Contains("$C"))
                                    {
                                        try
                                        {
                                            int count = Int32.Parse(racers[2].ToString());
                                            racerCount = count;
                                        }
                                        catch (FormatException)
                                        {
                                            comPort = "";
                                            System.Diagnostics.Debug.WriteLine("{0}: RacerCount Bad Format", racers[3].ToString());
                                        }
                                        catch (OverflowException)
                                        {
                                            comPort = "";
                                            System.Diagnostics.Debug.WriteLine("{0}: RacerCount Overflow", racers[3].ToString());
                                        }  
                                    }
                                }
                            }
                            catch (TimeoutException exTime)
                            {
                                // Times out
                                comPort = "";
                                System.Diagnostics.Debug.WriteLine("Bad RacerCount");
                            }
                        }
                    }
                    catch (TimeoutException exTime)
                    {
                        // Times out
                        comPort = "";
                    }
                    finally
                    {
                        if (serialPort.IsOpen) serialPort.Close();
                    }
                }
                catch (IOException ex)
                {
                   // Can legitimately fail here
                   System.Diagnostics.Debug.WriteLine(serialPort.PortName + " is not a ChineEasy");
                }
                finally
                {
                    if (serialPort.IsOpen) serialPort.Close();
                }
            }

            // Found a com port - so open for long term use
            if (comPort.Length > 0)
            {
                serialPort.PortName = comPort;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.Open();

            }
            return (comPort.Length > 0);
        }

        public void SendCommand(String send)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(send);
                }
                catch (TimeoutException exTime)
                {
                    // Times out
                    System.Diagnostics.Debug.WriteLine("Failed write: " + send);
                }
            }
        }

        public void GetPrefs()
        {
            SendCommand("$B?\r\n");
            SendCommand("$E?\r\n");
            SendCommand("$M?\r\n");
            SendCommand("$N?\r\n");
            SendCommand("$P?\r\n");
            SendCommand("$Q?\r\n");
            SendCommand("$W?\r\n");
        }

        public bool ChangeState(bool query)
        {
            bool change = false;
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(query? "$S?\r\n" : "$S\r\n");
                    change = true;
                }
                catch (TimeoutException exTime)
                {
                    // Times out
                    System.Diagnostics.Debug.WriteLine("Failed Status write");
                }
            }
            return change;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Read buffer
            SerialPort sp = (SerialPort)sender;
            String indata = sp.ReadExisting();

            // Append to buffer so far
            readBuffer += indata;
            //System.Diagnostics.Debug.WriteLine(readBuffer);

            // Split into sentences
            bool split = (readBuffer.Length > 3);
            while (split)
            {
                // Start stripping sentences
                int first = readBuffer.IndexOf('$');
                int last = (first != -1)? readBuffer.IndexOf("\r\n", first + 1) : -1;
                if ((first != -1) && (last != -1))
                {
                    String sentence = readBuffer.Substring(first + 1, last - first - 1);
                    if (sentence.Length > 0) ProcessSentence(sentence);
                    readBuffer = readBuffer.Substring(last + 1, readBuffer.Length - last - 1);
                } else {
                    split = false;
                }
            }
        }

        private bool ParseField(String sentence, out long value)
        {
            bool good = false;
            value = -1;
            try
            {
                // Arduino only has UInt32 so can convert these to ulong
                value = Int64.Parse(sentence.Substring(1, sentence.Length - 1));
                good = true;
            }
            catch (FormatException)
            {
                System.Diagnostics.Debug.WriteLine("Bad Format");
            }
            catch (OverflowException)
            {
                System.Diagnostics.Debug.WriteLine("Overflow");
            }
            return good;
        }

        private void ProcessSentence(String sentence)
        {
            System.Diagnostics.Debug.WriteLine(sentence);
            char cmd = sentence[0];
            switch (cmd)
            {
                case 'B':
                    long beep;
                    if (ParseField(sentence, out beep)) parent.Beep = (int)beep;
                    break;
                case 'E':
                    long edgeRSSI;
                    if (ParseField(sentence, out edgeRSSI)) parent.EdgeRSSI = (byte)edgeRSSI;
                    break;
                case 'M':
                    long minLapms;
                    if (ParseField(sentence, out minLapms)) parent.MinLapms = (ulong)minLapms;
                    break;
                case 'N':
                    long noiseTimems;
                    if (ParseField(sentence, out noiseTimems)) parent.NoiseTimems = (ulong)noiseTimems;
                    break;
                case 'P':
                    long peakRSSI;
                    if (ParseField(sentence, out peakRSSI)) parent.PeakRSSI = (byte)peakRSSI;
                    break;
                case 'Q':
                    long minPeakms;
                    if (ParseField(sentence, out minPeakms)) parent.MinPeakms = (ulong)minPeakms;
                    break;
                case 'W':
                    long maxPeakms;
                    if (ParseField(sentence, out maxPeakms)) parent.MaxPeakms = (ulong)maxPeakms;
                    break;
                case 'S':
                    long state;
                    if (ParseField(sentence, out state)) parent.UpdateState((int)state);
                    break;
                case 'R':
                    String [] lapStrings = sentence.Split(',');
                    if (lapStrings.Count() == 3)
                    {
                        try
                        {
                            int racerNo = Int32.Parse(lapStrings[0].Substring(1, lapStrings[0].Length - 1));
                            int lapNo = Int32.Parse(lapStrings[1].Substring(1, lapStrings[1].Length - 1));
                            ulong timems = UInt32.Parse(lapStrings[2].Substring(1, lapStrings[2].Length - 1));
                            parent.UpdateRacer(racerNo, lapNo, timems);
                        }
                        catch (FormatException)
                        {
                            System.Diagnostics.Debug.WriteLine("Lap Bad Format");
                        }
                        catch (OverflowException)
                        {
                            System.Diagnostics.Debug.WriteLine("Lap Overflow");
                        }  
                    }
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("Unhandled command: " + cmd);
                    break;
            }
        }
    }         
}
