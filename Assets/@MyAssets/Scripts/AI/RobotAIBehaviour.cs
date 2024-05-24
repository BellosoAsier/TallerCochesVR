using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum robotState {Idle, Recogida}
public class RobotAIBehaviour : MonoBehaviour
{
    private robotState estadoRobot;
    
    [SerializeField] private packagePickupScript pps;
    [SerializeField] private idleScript ids;
    void Start()
    {
        estadoRobot = robotState.Idle;
    }

    private void Update()
    {
        
        switch (estadoRobot)
        {
            case robotState.Idle:
                //Script IDLE
                ids.runIdle();
                break;

            case robotState.Recogida:
                pps.runPickup();
                break;
        }
    }

    public void changeStateRobot(robotState estado)
    {
        estadoRobot = estado;
        switch (estadoRobot)
        {
            case robotState.Idle:
                //Script IDLE
                ids.initilizeMethod(this);
                break;

            case robotState.Recogida:
                pps.initilizeMethod(this);
                break;
        }
    }

    public void assignPackage(GameObject p)
    {
        pps.package = p;
    }
}
