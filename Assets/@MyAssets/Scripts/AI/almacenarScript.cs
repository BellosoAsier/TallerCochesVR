using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.ReloadAttribute;

public class almacenarScript : MonoBehaviour
{
    private NavMeshAgent robot;
    private RobotAIBehaviour raib;
    private List<packageScript.PurchasePackageItem> ps;
    public GameObject package;
    private bool isPlaced = false;
    private bool isCoroutineRunning = false;
    private packagePickupScript pps;
    // Start is called before the first frame update
    void Start()
    {
        pps = GetComponent<packagePickupScript>();
        robot = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isPlaced)
        {
            isPlaced = false;
            ps.RemoveAt(0);
            pps.destinationEntrega = null;
            raib.changeStateRobot(robotState.Recogida);
            isCoroutineRunning = false;
        }
    }

    public void initilizeMethod(RobotAIBehaviour rr)
    {
        ps = package.GetComponent<packageScript>().listPurchasePackageItem;
        raib = rr;
    }

    public void runAlmacenarScript()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(WaitUntilContainerItemsAreLocated());
        }
    }

    IEnumerator WaitUntilContainerItemsAreLocated()
    {
        isCoroutineRunning = true;
        for (int i = 0; i < ps[0].quantity; i++)
        {
            pps.chosenContainer.GetComponent<containerManager>().numberOfItems++;
            yield return new WaitForSeconds(0.5f);
        }
        isPlaced = true;
    }
}
