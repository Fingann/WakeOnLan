using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace WakeOnLan
{
    class Program
    {
        static void Main(string[] args)
        {

            ComputerNameServer Server = new ComputerNameServer();
            var assetList = Server.GetComputers();

            foreach (Computer computer in assetList.Computers)
            {
                //string[] macAddr = computer.MAC.Split(':');
                //byte[] macBytes = new byte[5];
                //int count = 0;
                //foreach (var s in macAddr)
                //{
                //    macBytes[count] = BitConverter.
                //    count++;
                //}

                //long value = long.Parse(macAddr, NumberStyles.HexNumber, CultureInfo.CurrentCulture.NumberFormat);
                var macParse = computer.MAC.Replace(":", "");
                
                WakeOnLan(StringToByteArray(macParse));
                

            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        private static void WakeOnLan(byte[] mac)
        {
            // WOL packet is sent over UDP 255.255.255.0:40000.
            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 40000);

            // WOL packet contains a 6-bytes trailer and 16 times a 6-bytes sequence containing the MAC address.
            byte[] packet = new byte[17 * 6];

            // Trailer of 6 times 0xFF.
            for (int i = 0; i < 6; i++)
                packet[i] = 0xFF;

            // Body of magic packet contains 16 times the MAC address.
            for (int i = 1; i <= 16; i++)
                for (int j = 0; j < 6; j++)
                    packet[i * 6 + j] = mac[j];

            // Send WOL packet.
            client.Send(packet, packet.Length);
        }
    }
    }
