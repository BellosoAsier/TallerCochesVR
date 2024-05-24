using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class packagePickupScript : MonoBehaviour
{
    private NavMeshAgent robot;
    [SerializeField] private GameObject x;
    public GameObject package;
    [SerializeField] private List<containerManager> listaContainerManager;
    private bool hasPackage = false;
    private Transform destinationEntrega;
    private Transform pointContainer;
    private List<packageScript.PurchasePackageItem> ps;
    private RobotAIBehaviour raib;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        x.transform.position = robot.transform.position + new Vector3(-0.5f,-0.2f,0f);
        if (x.transform.childCount > 0)
        {
            x.transform.rotation = robot.transform.rotation;
        }
    }

    public void runPickup()
    {
        if (!hasPackage)
        {
            robot.SetDestination(package.transform.position);
            if (Vector3.Distance(robot.transform.position, package.transform.position) < 1)
            {
                package.transform.parent = x.transform;
                package.transform.position = x.transform.position;
                hasPackage = true;
            }
        }
        else
        {
            if (destinationEntrega == null)
            {
                setDestinationOfPackage();
            }
            else
            {
                if (Vector3.Distance(robot.transform.position, destinationEntrega.transform.position) < 1)
                {
                    for (int i = 0; i < ps[0].quantity; i++)
                    {
                        GameObject o = Instantiate(ps[0].item);
                        o.transform.position = pointContainer.position;
                        o.AddComponent<Rigidbody>();
                    }
                    ps.RemoveAt(0);
                    destinationEntrega = null;
                }
            }
        }

    }
    public void initilizeMethod(RobotAIBehaviour rr)
    {
        raib = rr;
        destinationEntrega = null;
        hasPackage = false;
    }

    private void setDestinationOfPackage()
    {
        ps = package.GetComponent<packageScript>().listPurchasePackageItem;
        if (ps.Count == 0)
        {
            Destroy(package);
            raib.changeStateRobot(robotState.Idle);
        }
        else
        {
            string[] palabras = ps[0].item.name.Split(' ');

            //Debug.Log(palabras);
        
            foreach (containerManager cm in listaContainerManager)
            {
                if (cm.gameObject.name.Contains(palabras[0]))
                {
                    int numero = int.Parse(palabras[1]);
                    destinationEntrega = cm.getContainerPosition(numero - 1)[2];
                    pointContainer = cm.getContainerPosition(numero - 1)[1];
                    robot.SetDestination(destinationEntrega.position);
                }
            }
        }
    }
}
