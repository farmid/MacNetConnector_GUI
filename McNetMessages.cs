using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MacNetConnector_GUI
{
    public class McNetMessages
    {
        public static string getLoginMessage()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("UserID", "wdt_db"), new JProperty("Password", "7Ksd!38*"), new JProperty("FClass", 8), new JProperty("FNum", 1)))
                );

            return (g.ToString());
        }

        public static string getLogoutMessage()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("FClass", 8), new JProperty("FNum", 2)))
                );

            return (g.ToString());
        }


        public static string getEchoMessage()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("UserID", "wdt_db"), new JProperty("Password", "7Ksd!38*"), new JProperty("FClass", 0), new JProperty("FNum", 0)))
                );

            return (g.ToString());
        }

        public static string getSystemInfo()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("FClass", 1), new JProperty("FNum", 1)))
                );

            return (g.ToString());
        }



        public static string getChannelStatus()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("FClass", 4), new JProperty("FNum", 1), new JProperty("Chan", 0), new JProperty("Len", 96)))
                );

            return (g.ToString());
        }

        public static string getChannelVoltage()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("FClass", 4), new JProperty("FNum", 2), new JProperty("Chan", 0), new JProperty("Len", 96)))
                );

            return (g.ToString());
        }

        public static string getChannelCurrent()
        {
            JObject g = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", "MacNet"),
                new JProperty("id", 1987),
                new JProperty("params", new JObject(new JProperty("FClass", 4), new JProperty("FNum", 3), new JProperty("Chan", 0), new JProperty("Len", 96)))
                );

            return (g.ToString());
        }
    }
}
