using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacNetConnector_GUI
{
    public partial class Form1 : Form
    {
        const int MaxModules = 2; //5;
        FlowLayoutPanel[] panels;
        McNetModule[] modules;
        string[] ips;
        bool doneInfo = true;
        bool doneData = true;

        public Form1()
        {
            InitializeComponent();
            Init();
        }


        private void OnDisplay(object obj, string newMsg)
        {
            Invoke(new Action(() =>
            {
                Display(newMsg);
            }));
        }

        private void Subscribers()
        {
            for (int i=0;i< MaxModules;i++)
            modules[i].OnDisplayMessage += OnDisplay;            
        }

        private void Init()
        {
            ips = new string[MaxModules];
            panels = new FlowLayoutPanel[MaxModules];
            modules = new McNetModule[MaxModules];
            panels[0] = flowLayoutPanel1;
            panels[1] = flowLayoutPanel2;
            panels[2] = flowLayoutPanel3;
            panels[3] = flowLayoutPanel4;
            panels[4] = flowLayoutPanel5;

            ips[0] = "192.168.5.3";
            ips[1] = "192.168.5.4";
            ips[2] = "192.168.5.5";
            ips[3] = "192.168.5.6";
            ips[4] = "192.168.5.7";
            tbIP1.Text = ips[0];
            tbIP2.Text = ips[1];
            tbIP3.Text = ips[2];
            tbIP4.Text = ips[3];
            tbIP5.Text = ips[4];

            for (int i = 0; i < MaxModules; i++) {
                modules[i] = new McNetModule();
                modules[i].Init(ips[i]);              
            }

            Subscribers();
        }
       
        private void btnStartAll_Click(object sender, EventArgs e)
        {
            for (int i=0;i< MaxModules; i++)
            {
                // don't connect if module's IP is not set
                if ( !String.IsNullOrEmpty(modules[i].ip))
                {
                    try
                    {
                        modules[i].Connect();
                    }
                    catch (Exception ex)
                    {
                        Display("M " + i.ToString() + "Error: " + ex.Message);
                    }
                }
            }

            GetInfoLoop();
            GetDataLoop();
        }

        private void GetInfoLoop()
        {
            doneInfo = false;
            while (!doneInfo)
            {
                for (int i=0;i< MaxModules; i++)
                {
                    if (modules[i].Connected)
                    {
                        modules[i].GetInfo();
                        Thread.Sleep(500);  // i second
                    }
                    
                    Application.DoEvents();
                }
            }                
        }

        private void GetDataLoop()
        {
            doneData = false;
            while (!doneData)
            {
                for (int i = 0; i < MaxModules; i++)
                {
                    if (modules[i].Connected)
                    {
                        modules[i].GetData();
                        Thread.Sleep(500);  // i second
                    }
                    Application.DoEvents();
                }
            }
        }


        private void tbIP1_TextChanged1(object sender, EventArgs e)
        {
            modules[0].ip = tbIP1.Text;
        }

        private void Display(string msg)
        {
            rtbConsole.Text += (msg + "\n");
            rtbConsole.SelectionStart = rtbConsole.Text.Length;
            rtbConsole.ScrollToCaret();
            Application.DoEvents();
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            for (int i=0;i< MaxModules; i++)
            {
                if (modules[i].Connected)
                    modules[i].Close();
            }
        }
    }
}
