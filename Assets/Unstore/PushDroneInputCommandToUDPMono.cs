using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PushDroneInputFromJoystickUDP;

public class PushDroneInputCommandToUDPMono : MonoBehaviour
{
    public string m_ipOfServer;
    public int m_portOfServer;

    public string m_lastSentCommand;
    public void SetArmedSwitchState(bool isArmedSwitchOn) {
        try
        {
            string cmd = "as" + (isArmedSwitchOn ? '1' : '0');
            m_lastSentCommand = cmd;
            DroneUDPToClients.SendUdp(m_ipOfServer, m_portOfServer, cmd);
        }
        catch (Exception) { }
    }
    public void SetKillSwitchState(bool isKillSwitchOn) {
        try
        {
            string cmd = "ks" + (isKillSwitchOn ? '1' : '0');
            m_lastSentCommand = cmd;
            DroneUDPToClients.SendUdp(m_ipOfServer, m_portOfServer, cmd);
        }
        catch (Exception) { }
    }
    public void SetServerIP(string ip) { m_ipOfServer = ip; }
    public void SetServerPort(string port) { if (!int.TryParse(port, out m_portOfServer)) m_portOfServer = 2509; }
    public void SetServerPort(int port) { m_portOfServer = port; }

}
