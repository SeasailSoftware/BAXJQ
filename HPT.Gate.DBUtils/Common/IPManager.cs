using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Management;
using System.Collections.Specialized;
namespace HPT.Gate.Utils.Common
{
    public class IPManager
    {
        /// <summary>
        /// 获取子网掩码
        /// </summary>
        /// <returns></returns>
        public static string GetSubnetMark()
        {
            //using System.Management;
            string subNet = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    subNet = (nic["IPSubnet"] as String[])[0];
                    //MessageBox.Show((nic["IPAddress"] as String[])[0]);
                    //MessageBox.Show((nic["IPSubnet"] as String[])[0]);
                    //MessageBox.Show((nic["DefaultIPGateway"] as String[])[0]);
                }
            }
            return subNet;
        }
        /// <summary> 
        /// 获得广播地址 
        /// </summary> 
        /// <param name="ipAddress">IP地址</param> 
        /// <param name="subnetMask">子网掩码</param> 
        /// <returns>广播地址</returns> 
        public static string GetBroadcast(string ipAddress, string subnetMask)
        {

            byte[] ip = IPAddress.Parse(ipAddress).GetAddressBytes();
            byte[] sub = IPAddress.Parse(subnetMask).GetAddressBytes();

            // 广播地址=子网按位求反 再 或IP地址 
            for (int i = 0; i < ip.Length; i++)
            {
                ip[i] = (byte)((~sub[i]) | ip[i]);
            }
            return new IPAddress(ip).ToString();
        }

        /// <summary>
        /// 获取网卡或无线网卡的第一个IP地址字符串
        /// </summary>
        /// <returns></returns>
        public static string GetIPStr()
        {
            var nifs = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nifs)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    var ip = ni.GetIPProperties();
                    if (ip.UnicastAddresses.Count > 0)
                    {
                        for (var i = 0; i < ip.UnicastAddresses.Count; i++)
                        {
                            ///过滤IPv6版本IP地址
                            if (ip.UnicastAddresses[i].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.UnicastAddresses[i].Address.ToString();
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取网卡或无线网卡的第一个IPv4版本地址(过滤IPv6版本IP地址)
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIPAddress()
        {
            var nifs = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nifs)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    var ip = ni.GetIPProperties();
                    if (ip.UnicastAddresses.Count > 0)
                    {
                        for (var i = 0; i < ip.UnicastAddresses.Count; i++)
                        {
                            ///过滤IPv6版本IP地址
                            if (ip.UnicastAddresses[i].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.UnicastAddresses[i].Address;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static string GetLocalIpv4()
        {
            string localIP = string.Empty;
            NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            int len = interfaces.Length;

            for (int i = 0; i < len; i++)
            {
                NetworkInterface ni = interfaces[i];
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (ni.Name == "本地连接")
                    {
                        IPInterfaceProperties property = ni.GetIPProperties();
                        foreach (UnicastIPAddressInformation ip in
                            property.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                localIP = ip.Address.ToString();
                            }
                        }
                    }
                }

            }
            return localIP;
        }
        /// <summary>
        /// 获取网卡或无线网卡的第一个IPv4版本子网掩码地址(过滤IPv6版本IP地址)
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIPMask()
        {
            var nifs = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nifs)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    var ip = ni.GetIPProperties();
                    if (ip.UnicastAddresses.Count > 0)
                    {
                        for (var i = 0; i < ip.UnicastAddresses.Count; i++)
                        {
                            ///过滤IPv6版本IP地址
                            if (ip.UnicastAddresses[i].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.UnicastAddresses[i].IPv4Mask;
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取IPv4版本局域网广播地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetBroadcast()
        {
            var nifs = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nifs)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    var ip = ni.GetIPProperties();
                    if (ip.UnicastAddresses.Count > 0)
                    {
                        for (var i = 0; i < ip.UnicastAddresses.Count; i++)
                        {
                            ///过滤IPv6版本IP地址
                            if (ip.UnicastAddresses[i].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                var sIP = ip.UnicastAddresses[i].Address.ToString();
                                var sMask = ip.UnicastAddresses[i].IPv4Mask.ToString();

                                var sIPArray = sIP.Split('.');
                                var sMaskArray = sMask.Split('.');
                                var sMaskBitArray = GetDecimalConvertBinary(sMaskArray);
                                var sMaskBitwiseArray = Bitwise(sMaskBitArray);
                                var BroadcastArray = BitwiseAND(sIPArray, sMaskBitwiseArray);
                                var strBroadcast = string.Empty;
                                for (var j = 0; j < BroadcastArray.Length; j++)
                                {
                                    strBroadcast += BroadcastArray[j];
                                    if (j + 1 < BroadcastArray.Length)
                                    {
                                        strBroadcast += ".";
                                    }
                                }
                                var ipBroadcast = (IPAddress)null;
                                IPAddress.TryParse(strBroadcast, out ipBroadcast);
                                return ipBroadcast;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static void GetIPAddressInfo()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType.ToString().Equals("Ethernet"))
                {
                    var ip = adapter.GetIPProperties();
                    for (var i = 0; i < ip.UnicastAddresses.Count; i++)
                    {
                        ///过滤IPv6版本IP地址
                        if (ip.UnicastAddresses[i].Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ip.UnicastAddresses[0].Address.ToString();
                            ip.UnicastAddresses[0].IPv4Mask.ToString();
                        }
                        if (ip.GatewayAddresses.Count > 0)
                        {
                            ip.GatewayAddresses[0].Address.ToString();
                        }
                        if (ip.DnsAddresses.Count > 0)
                        {
                            ip.DnsAddresses[0].ToString();
                            if (ip.DnsAddresses.Count > 1)
                            {
                                ip.DnsAddresses[1].ToString();
                            }
                        }
                    }
                }
            }
        }

        public static string GetBinaryConvertDecimal(string sBinary)
        {
            return Convert.ToInt32(sBinary, 2).ToString();
        }

        public static string GetDecimalConvertBinary(string sDecimal)
        {
            return Convert.ToString(Convert.ToInt32(sDecimal), 2).PadLeft(8, '0');
        }

        public static string[] GetDecimalConvertBinary(string[] sMaskArray)
        {
            var sBinaryAarry = new string[sMaskArray.Length];
            for (var i = 0; i < sMaskArray.Length; i++)
            {
                sBinaryAarry[i] = Convert.ToString(Convert.ToInt32(sMaskArray[i]), 2).PadLeft(8, '0');
            }
            return sBinaryAarry;
        }

        public static string Bitwise(string sBinary)
        {
            var str = new StringBuilder();
            foreach (char s in sBinary)
            {
                if (s == '0')
                {
                    str.Append("1");
                }
                else
                {
                    str.Append("0");
                }
            }
            return str.ToString();
        }

        public static string[] Bitwise(string[] sBinary)
        {
            var sBitwiseAarry = new string[sBinary.Length];
            for (var i = 0; i < sBinary.Length; i++)
            {
                var str = new StringBuilder();
                for (var j = 0; j < sBinary[i].Length; j++)
                {
                    if (sBinary[i][j] == '0')
                    {
                        str.Append("1");
                    }
                    else
                    {
                        str.Append("0");
                    }
                }
                sBitwiseAarry[i] = str.ToString();
            }
            return sBitwiseAarry;
        }

        public static string[] BitwiseAND(string[] sIPArray, string[] sMaskBitwiseArray)
        {
            var sBitwiseANDAarry = new string[sIPArray.Length];
            for (var i = 0; i < sIPArray.Length; i++)
            {
                sBitwiseANDAarry[i] = Convert.ToString(Convert.ToInt32(sIPArray[i]) | Convert.ToInt32(sMaskBitwiseArray[i], 2));
            }
            return sBitwiseANDAarry;
        }
        /// <summary>  
        /// 验证IP地址字符串的正确性  
        /// </summary>
        /// <param name="strIP">要验证的IP地址字符串</param>
        /// <returns>格式是否正确，正确为 true 否则为 false</returns>
        public static bool CheckIPAddr(string strIP)
        {
            if (string.IsNullOrEmpty(strIP))
            {
                return false;
            }

            var bRes = true;
            var iTmp = 0;
            var ipSplit = strIP.Split('.');
            if (ipSplit.Length < 4 || string.IsNullOrEmpty(ipSplit[0]) ||
                string.IsNullOrEmpty(ipSplit[1]) ||
                string.IsNullOrEmpty(ipSplit[2]) ||
                string.IsNullOrEmpty(ipSplit[3]))
            {
                bRes = false;
            }
            else
            {
                for (var i = 0; i < ipSplit.Length; i++)
                {
                    if (!int.TryParse(ipSplit[i], out iTmp) || iTmp < 0 || iTmp > 255)
                    {
                        bRes = false;
                        break;
                    }
                }
            }

            return bRes;
        }

        /// <summary>
        /// 判断数据库服务器是否远程计算机
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsRemote(string server)
        {
            ///判断数据库服务器是否一个IP地址
            if (System.Text.RegularExpressions.Regex.IsMatch(server, @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))"))
            {
                string localIp = GetLocalIpv4();
                if (server.Equals(localIp) || server.Equals("127.0.0.1"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (server.Equals(".") || server.ToUpper().Equals("LOCAL"))
                {
                    return false;
                }
                ///获取本机计算机名
                string machineName = System.Environment.MachineName;
                if (server.Equals(machineName))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }


    }
}
