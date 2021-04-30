using System.Net.Http;
using System;
using System.Net;
using System.Net.Sockets;
namespace JacobAssistant.Services
{
    public class WakeOnLanService
    {
        
        public static int WakeUp(string mac, int port, string ip)
        {
            byte[] magicBytes = GetMagicPacket(mac);
            IPEndPoint point = new IPEndPoint(GetOrCheckIp(ip), port);//广播模式:255.255.255.255
            try
            {
                UdpClient client = new UdpClient();
                return client.Send(magicBytes, magicBytes.Length, point);
            }
            catch (SocketException e) { /*MessageBox.Show(e.Message);*/ }
            return -100;
        }
        
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        
        private static byte[] GetMagicPacket(string macString)
        {
            byte[] returnBytes = new byte[102];
            string commandString = "FFFFFFFFFFFF";
            for (int i = 0; i < 6; i++)
                returnBytes[i] = Convert.ToByte(commandString.Substring(i * 2, 2), 16);
            byte[] macBytes = StrToHexByte(macString);
            for (int i = 6; i < 102; i++)
            {
                returnBytes[i] = macBytes[i % 6];
            }
            return returnBytes;
        }

        private static IPAddress GetOrCheckIp(string hostOrIp)
        {
            IPAddress IPA; 
            if (!IPAddress.TryParse(hostOrIp, out IPA))
            {
                //IPHostEntry host = Dns.GetHostByName(HostOrIP);
                IPHostEntry host = Dns.GetHostEntry(hostOrIp ?? string.Empty);
                IPA = host.AddressList[0];
            }
            return IPA;
        }
    }
}