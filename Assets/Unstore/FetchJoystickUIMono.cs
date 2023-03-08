using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FetchJoystickUIMono : MonoBehaviour
{
    public VariableJoystick joystickLeft;
    public VariableJoystick joystickRight;
    public PushDroneInputFromJoystickUDP m_sender;
    public float m_leftRight;
    public float m_backFront;
    public float m_downUp;
    public float m_leftRightRotation;

    public bool m_inverse;

    public void SetDefaultMove(bool setAsDefaultMode) {

        m_inverse = !setAsDefaultMode;
    }

    void Update()
    {
        if (m_inverse)
        {
            if (joystickLeft.Direction.x != m_leftRight)
            {
                m_leftRight = joystickLeft.Direction.x;
                m_sender.SetLeftRightPercent(m_leftRight);
            }
            if (joystickLeft.Direction.y != m_backFront)
            {
                m_backFront = joystickLeft.Direction.y;
                m_sender.SetBackFrontPercent(m_backFront);
            }
            if (joystickRight.Direction.x != m_leftRightRotation)
            {
                m_leftRightRotation = joystickRight.Direction.x;
                m_sender.SetLeftRightRotationPercent(m_leftRightRotation);
            }
            if (joystickRight.Direction.y != m_downUp)
            {
                m_downUp = joystickRight.Direction.y;
                m_sender.SetDownUpPercent(m_downUp);
            }
        }
        else
        {
            if (joystickLeft.Direction.x != m_leftRight)
            {
                m_leftRight = joystickLeft.Direction.x;
                m_sender. SetLeftRightRotationPercent(m_leftRight);
            }
            if (joystickLeft.Direction.y != m_backFront)
            {
                m_backFront = joystickLeft.Direction.y;
                m_sender. SetDownUpPercent(m_backFront);
            }
            if (joystickRight.Direction.x != m_leftRightRotation)
            {
                m_leftRightRotation = joystickRight.Direction.x;
                m_sender.SetLeftRightPercent(m_leftRightRotation);
            }
            if (joystickRight.Direction.y != m_downUp)
            {
                m_downUp = joystickRight.Direction.y;
                m_sender.SetBackFrontPercent(m_downUp);
            }
        }

    }
}
