using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChineEasy
{
    public partial class MainForm : Form
    {
        private const String title = "ChineEasy Lap Timer";

        private int numberOfRacers = -1;
        private RacerInfo[] racers = new RacerInfo[RacerInfo._maxRacers];
        private SerialLink serial = new SerialLink();

        private Timer uiTimer = new Timer();
        private bool[] updateUI = new bool[RacerInfo._maxRacers];
        private bool updateMain = false;
        private ToolStripMenuItem menuItemStatus = new ToolStripMenuItem();
        private int focusIndex = -1; 

        private int raceState = 0;
        private int beeper = 0;
        private ulong minLapms = 0;
        private ulong minPeakms = 0;
        private ulong maxPeakms = 0;
        private ulong noiseTimems = 0;
        private byte edgeRSSI = 0;
        private byte peakRSSI = 0;

        public byte EdgeRSSI
        {
            get { return edgeRSSI; }
            set { edgeRSSI = value; }
        }

        public byte PeakRSSI
        {
            get { return peakRSSI; }
            set { peakRSSI = value; }
        }

        public int Beep
        {
            get { return beeper; }
            set { beeper = value; }
        }

        public ulong NoiseTimems
        {
            get { return noiseTimems; }
            set { noiseTimems = value; }
        }

        public ulong MinLapms
        {
            get { return minLapms; }
            set { minLapms = value; }
        }

        public ulong MinPeakms
        {
            get { return minPeakms; }
            set { minPeakms = value; }
        }

        public ulong MaxPeakms
        {
            get { return maxPeakms; }
            set { maxPeakms = value; }
        }

        public MainForm()
        {
            // Look for serial
            if (InitialiseSerial())
            {
                // Load defaults
                ReadDefaults();

                // Construct UI
                InitializeComponent();

                // Create right click menu..
                ContextMenuStrip strip = new ContextMenuStrip();
                strip.Font = new System.Drawing.Font("Segoe UI", 20F);
                ToolStripMenuItem menuItemConfig = new ToolStripMenuItem();

                // Add right click menu item for start 
                menuItemStatus.Text = "Start";
                menuItemStatus.Click += start_Click;

                // Add right click menu item for start 
                menuItemConfig.Text = "Settings...";
                menuItemConfig.Click += config_Click;

                // Add items
                strip.Items.Add(menuItemStatus);
                strip.Items.Add("-");
                strip.Items.Add(menuItemConfig);
                this.ContextMenuStrip = strip;

                // UpdateUI timer
                uiTimer.Tick += new EventHandler(UpdateUI);
                uiTimer.Interval = 200; // 5 times per sec
                uiTimer.Start();

                // Query state
                serial.GetPrefs();
                serial.ChangeState(true);

                for (int r = 0; r < RacerInfo._maxRacers; r++) updateUI[r] = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Attach UI panels
            for (int r = 0; r < numberOfRacers; r++)
            {
                this.racerUI[r].AttachRacerInfo(racers[r]);
            }            
        }

        void start_Click(object sender, EventArgs e)
        {
            // Poke a state change
            serial.ChangeState(false);
        }

        void config_Click(object sender, EventArgs e)
        {
            ConfigureUI config = new ConfigureUI();
            config.checkBeep.Checked = (beeper == 1);
            config.textBoxMinLap.Text = minLapms.ToString();
            config.textBoxMinPeak.Text = MinPeakms.ToString();
            config.textBoxMaxPeak.Text = MaxPeakms.ToString();

            config.ShowDialog(this);

            if (config.checkBeep.Checked)
            {
                if (beeper == 0)
                {
                    serial.SendCommand("$B\r\n");
                    beeper = 1;
                }
            }
            else
            {
                if (beeper == 1)
                {
                    serial.SendCommand("$B\r\n");
                    beeper = 0;
                }
            }
            if (config.textBoxMinLap.Text != minLapms.ToString())
            {
                serial.SendCommand("$M" + config.textBoxMinLap.Text + "\r\n");
            }
        }

        private bool InitialiseSerial()
        {
            // Open serial comms
            bool init = serial.Initialise(this);
            if (init)
            {
                // Hardware supports how many?
                numberOfRacers = serial.GetRacers();
                if (numberOfRacers > 0)
                {
                    for (int r = 0; r < RacerInfo._maxRacers; r++)
                    {
                        racers[r] = new RacerInfo();
                    }
                }
                else
                {
                    init = false;
                }
            }
            return init;
        }

        public void UpdateRacer(int racerNo, int lapNo, ulong timems)
        {
            racers[racerNo].AddLap(lapNo, timems);
            updateUI[racerNo] = true;
        }

        public void UpdateState(int state)
        {
            raceState = state;
            updateMain = true;
        }

        private void UpdateUI(object sender, EventArgs e)
        {
            if (updateMain)
            {
                menuItemStatus.Enabled = (raceState != 0);
                switch (raceState)
                {
                    case 0:
                        menuItemStatus.Text = "Initialising";
                        break;
                    case 1:
                        // Clear old
                        for (int r = 0; r < numberOfRacers; r++)
                        {
                            racers[r].ClearLaps();
                            updateUI[r] = true;
                        }
                        menuItemStatus.Text = "Start";
                        this.Text = title + " - waiting";
                        break;
                    case 2:
                        menuItemStatus.Text = "Abort";
                        this.Text = title + " - under orders";
                        break;
                    case 3:
                        menuItemStatus.Text = "Stop Race";
                        this.Text = title + " - timing";
                        break;
                    case 4:
                        menuItemStatus.Text = "Clear Laps";
                        this.Text = title + " - reporting";
                        break;
                }
                updateMain = false;
            }
            bool reporting = (raceState == 4);
            focusIndex = (reporting) ? focusIndex + 1 : -1;
            for (int r = 0; r < numberOfRacers; r++)
            {
                if (reporting)
                {
                    this.racerUI[r].UpdateUI(focusIndex / 3);
                }
                if (updateUI[r])
                {
                    this.racerUI[r].UpdateUI(-1);
                    updateUI[r] = false;
                }
            }
        }

        private void ReadDefaults()
        {
            for (int r = 0; r < numberOfRacers; r++)
            {
                racers[r].Name = "Racer " + (r + 1).ToString();
            }
            racers[0].UIColour = Color.FromArgb(255, 0, 0);
            racers[1].UIColour = Color.FromArgb(0, 255, 0);
            racers[2].UIColour = Color.FromArgb(0, 0, 255);
            racers[3].UIColour = Color.FromArgb(0, 255, 255);
        }

        private void SaveDefaults()
        {
            // Save all
            for (int r = 0; r < RacerInfo._maxRacers; r++)
            {
            }
        }
    }
}
