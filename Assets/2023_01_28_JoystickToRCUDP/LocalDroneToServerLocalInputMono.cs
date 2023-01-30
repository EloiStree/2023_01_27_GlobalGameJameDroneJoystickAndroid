using Eloi;
using LocalDroneInputSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDroneToServerLocalInputMono : MonoBehaviour
{

    public LocalDroneInputUtility m_localDroneInput;
    public float m_leftRight;
    public float m_backFront;
    public float m_downUp;
    public float m_leftRightRotation;

    public PrimitiveUnityEvent_Float m_leftRightEvent;
    public PrimitiveUnityEvent_Float m_backFrontEvent;
    public PrimitiveUnityEvent_Float m_downUpEvent;
    public PrimitiveUnityEvent_Float m_leftRightRotationEvent;

    public void Start()
    {
        m_localDroneInput = new LocalDroneInputUtility();
        m_localDroneInput.Enable();
        m_localDroneInput.LocalDroneInput.Enable();

        m_localDroneInput.LocalDroneInput.LeftRight.performed += (a) => { m_leftRight = a.ReadValue<float>(); m_leftRightEvent.Invoke( m_leftRight); };
        m_localDroneInput.LocalDroneInput.LeftRight.canceled += (a) => { m_leftRight = 0; m_leftRightEvent.Invoke(0); };

        m_localDroneInput.LocalDroneInput.BackForward.performed += (a) => { m_backFront = a.ReadValue<float>(); m_backFrontEvent.Invoke(m_backFront); };
        m_localDroneInput.LocalDroneInput.BackForward.canceled += (a) => { m_backFront = 0; m_backFrontEvent.Invoke(0); };

        m_localDroneInput.LocalDroneInput.DownUp.performed += (a) => { m_downUp = a.ReadValue<float>(); m_downUpEvent.Invoke(m_downUp); };
        m_localDroneInput.LocalDroneInput.DownUp.canceled += (a) => { m_downUp = 0; m_downUpEvent.Invoke(0); };

        m_localDroneInput.LocalDroneInput.RotateLeftRight.performed += (a) => { m_leftRightRotation = a.ReadValue<float>(); m_leftRightRotationEvent.Invoke(m_leftRightRotation); };
        m_localDroneInput.LocalDroneInput.RotateLeftRight.canceled += (a) => { m_leftRightRotation = 0; m_leftRightRotationEvent.Invoke(0); };


        //udp
    }

}
