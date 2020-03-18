using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MacNetConnector_GUI
{
    class McNetModule
    {
        public string ip;
        public bool Connected;

        private Socket skt;
        private int port = 57570;
        
              
        public delegate void OnDisplayerHandler(object obj, string msg);
        public event OnDisplayerHandler OnDisplayMessage;

        private void Display(string msg)
        {
            if (OnDisplayMessage != null)
                OnDisplayMessage(this, msg);
        }

        //List<string> messagesToSend;
        List<string> requestDataMsgsToSend = new List<string>() { McNetMessages.getChannelCurrent(),
                                                                  McNetMessages.getChannelVoltage() };

        List<string> requestInfoMsgsToSend = new List<string>() { McNetMessages.getSystemInfo(),
                                                                  McNetMessages.getChannelStatus() };

        List<string> messagesReceived; // not used yet

        byte[] receiveBuffer = new byte[4096];

        public void Init(string hostIP)
        {
            Connected = false;
            ip = hostIP;
        }

        public void Connect()
        {
            int i;
            Connected = false;
            string LogInMsg = McNetMessages.getLoginMessage();
            if (String.IsNullOrEmpty(ip)) return;

            try {
                skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.Connect(ip, port);
                Connected = true;
                i = skt.Send(Encoding.UTF8.GetBytes(LogInMsg));
                Display("sent login request");

                // \todo check response
                //i = skt.Receive(receiveBuffer);
            }
            catch(Exception ex)
            {
                Display(ex.Message);
            }
        }

        public void Close()
        {
            int i = skt.Send(Encoding.UTF8.GetBytes(McNetMessages.getLoginMessage()));
            skt.Close();
            Connected = false;            
        }

        public void GetInfo()
        {
            int i;
            try
            {
                foreach (var message in requestInfoMsgsToSend)
                {
                    i = skt.Send(Encoding.UTF8.GetBytes(message));  // Blocks until send returns.

                    // Get reply from the server.
                    // \todo put sleep here if server needs time to prepare data
                    i = skt.Receive(receiveBuffer);
                    // Display();
                    // \todo update database
                }
            }
            catch (Exception ex)
            {
                Display("GetInfo: " + ex.Message);
            }
        }

        public void GetData()
        {
            int i;
            try
            {
                foreach (var message in requestDataMsgsToSend)
                {
                    i = skt.Send(Encoding.UTF8.GetBytes(message));  // Blocks until send returns.

                    // Get reply from the server.
                    // \todo put sleep here if server needs time to prepare data
                    i = skt.Receive(receiveBuffer);
                    // Display();
                    // \todo update database
                }
            }
            catch (Exception ex)
            {
                Display("GetInfo: " + ex.Message);
            }
        }

    }    
}
