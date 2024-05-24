using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.ReloadAttribute;

public class idleScript : MonoBehaviour
{
    private NavMeshAgent robot;
    private RobotAIBehaviour raib;
    private Vector3 initialLocation;
    // Start is called before the first frame update
    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        initialLocation = robot.transform.position;
    }

    public void initilizeMethod(RobotAIBehaviour rr)
    {
        raib = rr;
    }

    public void runIdle()
    {
        if (robot.transform.position != initialLocation)
        {
            robot.SetDestination(initialLocation);
        }
    }
}
