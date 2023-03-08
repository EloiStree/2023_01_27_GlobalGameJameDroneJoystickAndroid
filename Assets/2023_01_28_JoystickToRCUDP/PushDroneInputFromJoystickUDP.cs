using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System;

public class PushDroneInputFromJoystickUDP : MonoBehaviour
{
    public string m_ipOfServer;
    public int m_portOfServer;

    public float m_leftRightPercent;
    public float m_downUpPercent;
    public float m_backFrontPercent;
    public float m_leftRightRotationPercent;
    [Range(0f,1f)]
    public float m_generalSensibilityInPercent=1f;

    public void SetServerIP(string ip) { m_ipOfServer = ip; }
    public void SetServerPort(string port) {if(! int.TryParse(port, out m_portOfServer))m_portOfServer=2509; }
    public void SetServerPort(int port) { m_portOfServer = port; }

    public void SetLeftRightPercent(float percent) { m_leftRightPercent = percent; m_hadChanged = true; }
    public void SetDownUpPercent(float percent) { m_downUpPercent = percent; m_hadChanged = true; }
    public void SetBackFrontPercent(float percent) { m_backFrontPercent = percent; m_hadChanged = true; }
    public void SetLeftRightRotationPercent(float percent) { m_leftRightRotationPercent = percent; m_hadChanged = true; }

    public float m_timeBetweenPushToUdp=0.1f;
    public bool m_hadChanged;
    public string m_lastSentCommand;
    public IEnumerator Start() {

        while (true) {

            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenPushToUdp);
            if (m_hadChanged)
            {
                float s = m_generalSensibilityInPercent;
                m_hadChanged = true;
                string cmd = string.Format("rc {0:0.00} {1:0.00} {2:0.00} {3:0.00}", m_leftRightPercent * s, m_backFrontPercent * s, m_leftRightRotationPercent * s, m_downUpPercent * s);
                try
                {
                    DroneUDPToClients.SendUdp(m_ipOfServer, m_portOfServer, cmd);
                }
                catch (Exception) { }
                m_lastSentCommand = cmd;
            }
        }
    
    }
    public void SetGeneralSpeedSensibility(float speedPercent) {

        m_generalSensibilityInPercent = speedPercent;
    }


    public class DroneUDPToClients
    {

        public static int m_defaultPort = 2508;
        public static UDPPusher m_pusher = new UDPPusher("127.0.0.1", 2508);
        public static void SendUdp(string message, params string[] ips)
        {
            foreach (var ip in ips)
            {
                m_pusher.SetWith(ip, m_defaultPort);
                m_pusher.SendMessageWithUDP(message);
            }
        }

        public static void SendUdp(string ip, string message)
        {
            m_pusher.SetWith(ip, m_defaultPort);
            m_pusher.SendMessageWithUDP(message);
        }
        public static void SendUdp(string ip, int port, string message)
        {
            m_pusher.SetWith(ip, port);
            m_pusher.SendMessageWithUDP(message);
        }

        public class UDPPusher
        {
            UDPTargetParams m_target;
            IPEndPoint m_destinationEndPoint;
            Socket m_destinationSock;


            public UDPTargetParams GetCurrentTarget() { return m_target; }

            public UDPPusher()
            {
                m_target = new UDPTargetParams();
                SetWith(m_target);
            }
            public UDPPusher(string adddres, int port)
            {
                m_target = new UDPTargetParams();
                m_target.SetWith(adddres, port);
                SetWith(m_target);
            }


            public void SetWith(in string address, int port)
            {
                m_target.SetWith(address, port);
                SetWith(m_target);

            }
            public void SetWith(UDPTargetParams target)
            {

                m_target = target;
                m_destinationSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serverAddr = IPAddress.Parse(m_target.m_ipAddress);
                m_destinationEndPoint = new IPEndPoint(serverAddr, m_target.m_ipPort);
            }

            public void SendMessageWithUDP(string message)
            {
                m_destinationSock.SendTo(Encoding.Unicode.GetBytes(message), m_destinationEndPoint);
            }

        }
        
        [System.Serializable]
        public class UDPTargetParams {
            public string m_ipAddress;
            public int m_ipPort;
            public void SetWith(string ipAddress, int port) {
                m_ipPort = port;
                m_ipAddress = ipAddress;
            }
        }
    }
}




