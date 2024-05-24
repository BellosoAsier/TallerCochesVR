using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

public class manageCarOrders : MonoBehaviour
{
    [SerializeField] private GameObject repairOrderWindow;
    [SerializeField] private GameObject l;
    private bool leftWasActive;
    [SerializeField] private GameObject r;
    private bool rightWasActive;
    [SerializeField] private GameObject buttonAccept;
    [SerializeField] private GameObject buttonReject;
    [SerializeField] private GameObject buttonCancelar;
    [SerializeField] private GameObject buttonComplete;

    private clientOrders.ClientOrder cco;
    public List<Material> m1;
    public List<Material> m2;
    public List<Material> m3;
    private System.Random rnd = new System.Random();

    private Coroutine mainCoroutine;

    [SerializeField] private playerDataSO playerDataSO;
    [SerializeField] private gameManagerSO gameDataSO;

    private GameObject carObject;

    [SerializeField] private List<List<string>> listaObjetosPreviosACambio = new List<List<string>>(4);

    [SerializeField] private List<TMP_Text> listaTextsNecessaryChanges;
    [SerializeField] private List<TMP_Text> listaTextsExtraChanges;

    [SerializeField] private List<TMP_Text> listaTextsContadores;

    [SerializeField] private int extraMoneyPerExtraChange;

    private bool orderAccepterdBool = false;
    private float tiempoInicio;

    [SerializeField] private GameObject carPlacement;

    private void Awake()
    {
        
        cco = repairOrderWindow.GetComponent<clientOrders>().currentClientOrder;

        for (int i = 0; i < 4; i++) listaObjetosPreviosACambio.Add(new List<string>());

        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 0)
        {
            l.SetActive(false);
        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 4)
        {
            r.SetActive(false);
        }
    }

    private void Update()
    {
        cco = repairOrderWindow.GetComponent<clientOrders>().currentClientOrder;

        if (orderAccepterdBool)
        {
            checkCorrectChangesMade();
            checkExtraChangesCorrect();
        }
    }

    public void moveRight()
    {
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients != 4)
        {
            repairOrderWindow.GetComponent<clientOrders>().pointerClients++;
            if (!l.activeSelf)
            {
                l.SetActive(true);
            }
        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 4)
        {
            r.SetActive(false);
        }
    }

    public void moveLeft()
    {
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients != 0)
        {
            repairOrderWindow.GetComponent<clientOrders>().pointerClients--;
            if (!r.activeSelf)
            {
                r.SetActive(true);
            }

        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 0)
        {
            l.SetActive(false);
        }
    }

    public void completeTask()
    {
        bool everythingOK = true;

        StopCoroutine(mainCoroutine);

        buttonCancelar.SetActive(false);
        buttonComplete.SetActive(false);
        l.SetActive(leftWasActive);
        r.SetActive(rightWasActive);
        buttonAccept.SetActive(true);
        buttonReject.SetActive(true);
        orderAccepterdBool = false;

        float tiempoTranscurrido = Time.time - tiempoInicio;

        foreach (TMP_Text tmpt in listaTextsNecessaryChanges)
        {
            if (tmpt.color == Color.green)
            {
                tmpt.color = Color.white;
            }
            else
            {
                everythingOK = !everythingOK;
            }

        }
        int multi = 0;
        foreach (TMP_Text tmpt in listaTextsExtraChanges)
        {
            if (tmpt.color == Color.green)
            {
                multi++;
            }
            tmpt.color = Color.white;
        }

        if (everythingOK)
        {
            playerDataSO.completedOrders++;
            playerDataSO.currentMoney += (cco.orderPrice + (extraMoneyPerExtraChange*multi));
            playerDataSO.avgTimePerOrder = (playerDataSO.avgTimePerOrder + tiempoTranscurrido) / playerDataSO.completedOrders;
        }

        eliminateAndAddNewOrder();
    }

    public void acceptOrder()
    {
        tiempoInicio = Time.time;
        foreach (GameObject go in gameDataSO.carModelsGeneralList)
        {
            if (go.name.Contains(cco.clientCar))
            {
                carObject = Instantiate(go);
            }
        }
        carObject.name = carObject.name.Replace("(Clone)", "");
        carObject.transform.position = carPlacement.transform.position + new Vector3(0f, 0.14f, 0f);
        customizeOrderCar(carObject);
        int counter = 0;
        foreach (clientOrders.NecessaryChange cnc in cco.clientCarChanges)
        {
            if (cnc.name.Contains("Cambio"))
            {
                foreach (string s in cnc.carPart)
                {
                    for (int i = 0; i < carObject.transform.childCount; i++)
                    {
                        GameObject hijoTransform = carObject.transform.GetChild(i).GetChild(0).gameObject;
                        if (hijoTransform.name.StartsWith(s))
                        {
                            listaObjetosPreviosACambio[counter].Add(hijoTransform.name);
                            //Debug.Log(hijoTransform);
                        }
                    }
                }
            }
            counter++;
        }
        orderAccepterdBool = true;
        mainCoroutine = StartCoroutine(lockCarOrders(cco.orderDuration));
    }

    public void rejectOrder()
    {
        eliminateAndAddNewOrder();
        playerDataSO.rejectedOrders++;
    }

    public void cancelarOrder()
    {
        if (mainCoroutine != null)
        {
            StopCoroutine(mainCoroutine);
            eliminateAndAddNewOrder();
            buttonCancelar.SetActive(false);
            buttonComplete.SetActive(false);
            buttonAccept.SetActive(true);
            buttonReject.SetActive(true);
            l.SetActive(leftWasActive);
            r.SetActive(rightWasActive);
            playerDataSO.cancelledOrders++;
            orderAccepterdBool = false;
            foreach (TMP_Text i in listaTextsNecessaryChanges)
            {
                i.color = Color.white;
            }
            //Debug.Log("Espera cancelada.");
        }
    }

    IEnumerator lockCarOrders(float time)
    {
        leftWasActive = l.activeSelf;
        rightWasActive = r.activeSelf;
        l.SetActive(false);
        r.SetActive(false);
        buttonAccept.SetActive(false);
        buttonReject.SetActive(false);

        yield return new WaitForSeconds(3f);
        buttonCancelar.SetActive(true);
        buttonComplete.SetActive(true);

        float timeRemaining = time;

        while (timeRemaining > 0)
        {
            // Decrementa el tiempo restante
            timeRemaining -= Time.deltaTime;
            // Asegura que el tiempo restante no sea negativo
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
            }
            // Actualiza el texto del TMP_Text
            UpdateTimerText(timeRemaining);
            yield return null;
            
        }

        completeTask();
    }

    private void UpdateTimerText(float timeRemaining)
    {
        // Convierte el tiempo restante a minutos y segundos
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Actualiza el texto del TMP_Text con el formato MM:SS
        foreach (TMP_Text ttp in listaTextsContadores)
        {
            ttp.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void eliminateAndAddNewOrder()
    {
        repairOrderWindow.GetComponent<clientOrders>().listClientOrders.RemoveAt(repairOrderWindow.GetComponent<clientOrders>().pointerClients);
        repairOrderWindow.GetComponent<clientOrders>().listClientOrders.Add(repairOrderWindow.GetComponent<clientOrders>().AddNewClientOrder());
    }

    private void checkCorrectChangesMade()
    {
        int contador = 0;
        foreach(clientOrders.NecessaryChange cnc in cco.clientCarChanges)
        {
            if (cnc.name.Contains("Cambio"))
            {
                foreach (string s in cnc.carPart)
                {
                    for (int i = 0; i < carObject.transform.childCount; i++)
                    {
                        // Obtener el transform del hijo actual
                        GameObject hijoTransform = carObject.transform.GetChild(i).GetChild(0).gameObject;
                        if (hijoTransform.name.StartsWith(s))
                        {
                            // Inicio del bloque para el bucle foreach interno
                            {
                                foreach (string l in listaObjetosPreviosACambio[contador])
                                {
                                    if (!l.StartsWith(s))
                                    {
                                        // No se hace nada.
                                    }
                                    else if (l.StartsWith(s) && hijoTransform.name != l)
                                    {
                                        if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                                        {
                                            //BIEN - text[contador] = Color.Green;
                                            listaTextsNecessaryChanges[contador].color = Color.green;
                                            break;
                                        }
                                        else
                                        {
                                            //MAL - text[contador] = Color.Red;
                                            listaTextsNecessaryChanges[contador].color = Color.red;
                                            break;
                                        }
                                        
                                    }
                                    else if(l.StartsWith(s) && hijoTransform.name == l)
                                    {
                                        //MAL - text[contador] = Color.Red;
                                        listaTextsNecessaryChanges[contador].color = Color.red;
                                        break;
                                    }
                                }
                            }
                            // Fin del bloque para el bucle foreach interno
                        }
                    }
                }
            }
            else if(cnc.name.Contains("Tinte"))
            {
                foreach (string s in cnc.carPart)
                {
                    for (int i = 0; i < carObject.transform.childCount; i++)
                    {
                        GameObject hijoTransform = carObject.transform.GetChild(i).GetChild(0).gameObject;
                        if (hijoTransform.name.StartsWith(s))
                        {

                            if(hijoTransform.GetComponent<Renderer>().materials.Length > 1)
                            {
                                if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                                {
                                    //BIEN - text[contador] = Color.Green;
                                    listaTextsNecessaryChanges[contador].color = Color.green;
                                    break;
                                }
                                else if (hijoTransform.GetComponent<Renderer>().materials[1].name.Contains(cnc.partColor))
                                {
                                    //BIEN - text[contador] = Color.Green;
                                    listaTextsNecessaryChanges[contador].color = Color.green;
                                    break;
                                }
                                else
                                {
                                    //MAL - text[contador] = Color.Red;
                                    listaTextsNecessaryChanges[contador].color = Color.red;
                                    break;
                                }
                            }
                            else
                            {
                                if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                                {
                                    //BIEN - text[contador] = Color.Green;
                                    listaTextsNecessaryChanges[contador].color = Color.green;
                                    break;
                                }
                                else
                                {
                                    //MAL - text[contador] = Color.Red;
                                    listaTextsNecessaryChanges[contador].color = Color.red;
                                    break;
                                }
                            }
                            
                        }
                    }
                }
            }
            else if (cnc.name.Contains("Luz"))
            {
                foreach (string s in cnc.carPart)
                {
                    for (int i = 0; i < carObject.transform.childCount; i++)
                    {
                        GameObject hijoTransform = carObject.transform.GetChild(i).GetChild(0).gameObject;
                        if (hijoTransform.name.StartsWith(s))
                        {
                            if (hijoTransform.GetComponent<Renderer>().materials[1].name.Contains(cnc.partColor))
                            {
                                //BIEN - text[contador] = Color.Green;
                                listaTextsNecessaryChanges[contador].color = Color.green;
                                break;
                            }
                            else
                            {
                                //MAL - text[contador] = Color.Red;
                                listaTextsNecessaryChanges[contador].color = Color.red;
                                break;
                            }
                        }
                    }
                }
            }
            else if (cnc.name.Contains("Color"))
            {
                foreach (string s in cnc.carPart)
                {
                    for (int i = 0; i < carObject.transform.childCount; i++)
                    {
                        GameObject hijoTransform = carObject.transform.GetChild(i).GetChild(0).gameObject;
                        if (hijoTransform.name.StartsWith(s))
                        {
                            if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                            {
                                //BIEN - text[contador] = Color.Green;
                                listaTextsNecessaryChanges[contador].color = Color.green;
                                break;
                            }
                            else
                            {
                                //MAL - text[contador] = Color.Red;
                                listaTextsNecessaryChanges[contador].color = Color.red;
                                break;
                            }
                        }
                    }
                }
            }
            contador++;
        }
    }

    private void checkExtraChangesCorrect()
    {
        int contador = 0;
        foreach (clientOrders.OrderExtra cooe in cco.clientOrderExtras)
        {
            Color color = Color.red;
            switch (cooe.statistic)
            {
                case "EcoValue":
                    if (((carObject.GetComponent<CarScript>().CarEcoValue > cooe.value) && cooe.isHigher ) || ((carObject.GetComponent<CarScript>().CarEcoValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
                case "Velocity":
                    if (((carObject.GetComponent<CarScript>().CarVelocityValue > cooe.value) && cooe.isHigher) || ((carObject.GetComponent<CarScript>().CarVelocityValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
                case "Manejo":
                    if (((carObject.GetComponent<CarScript>().CarManejoValue > cooe.value) && cooe.isHigher) || ((carObject.GetComponent<CarScript>().CarManejoValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
                case "Weight":
                    if (((carObject.GetComponent<CarScript>().CarWeightValue > cooe.value) && cooe.isHigher) || ((carObject.GetComponent<CarScript>().CarWeightValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
                case "Quality":
                    if (((carObject.GetComponent<CarScript>().CarQualityValue > cooe.value) && cooe.isHigher) || ((carObject.GetComponent<CarScript>().CarQualityValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
                case "Beauty":
                    if (((carObject.GetComponent<CarScript>().CarBeautyValue > cooe.value) && cooe.isHigher) || ((carObject.GetComponent<CarScript>().CarBeautyValue < cooe.value) && !cooe.isHigher))
                    {
                        color = Color.green;
                    }
                    break;
            }
            listaTextsExtraChanges[contador].color = color;
            contador++;
        }
    }

    private void customizeOrderCar(GameObject clientCarToCustomize)
    {
        foreach (Transform hijoTransform in clientCarToCustomize.transform)
        {
            //Transform nietoTransform = hijoTransform.GetChild(0);
            //GameObject parte = nietoTransform.gameObject;
            int rand1 = 0;
            int rand2 = 0;

            switch (hijoTransform.GetChild(0).gameObject.name)
            {
                case "Windshield":
                case "SideWindow":
                case "RearWindow":
                    rand1 = rnd.Next(m3.Count);
                    hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material = m3[rand1];
                    break;
                default:
                    for (int i = 0; i < hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials.Length; i++)
                    {
                        if (i==0)
                        {
                            rand1 = rnd.Next(m1.Count);
                            hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material = m1[rand1];
                        }
                        if (i == 1)
                        {
                            if (hijoTransform.GetChild(0).gameObject.name.Contains("Body"))
                            {
                                rand2 = rnd.Next(m2.Count);
                                Material[] nuevosMateriales = hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                                nuevosMateriales[1] = m2[rand2];
                                hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials = nuevosMateriales;
                            }
                            else
                            {
                                rand2 = rnd.Next(m3.Count);
                                Material[] nuevosMateriales = hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                                nuevosMateriales[1] = m3[rand2];
                                hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials = nuevosMateriales;
                            }
                        }
                    }
                    break;
            }

            foreach (clientOrders.NecessaryChange nc in cco.clientCarChanges)
            {
                int rand3 = 0;
                if (nc.carPart.Contains(hijoTransform.GetChild(0).gameObject.name))
                {
                    if (m1.Find(material => material.name == nc.partColor) != null)
                    {
                        if(hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material.name.Contains(nc.partColor))
                        {
                            do
                            {
                                rand3 = rnd.Next(m1.Count);
                            } while (rand3 == rand1);
                            Debug.Log("Change: "+ hijoTransform.GetChild(0).gameObject.name + ", from:" + m1[rand1] + ", to:" + m1[rand3]);
                            hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material = m1[rand3];
                        }
                        else
                        {
                            Debug.Log("No changes: " + hijoTransform.GetChild(0).gameObject.name);
                        }
                        
                    }
                    else if (m2.Find(material => material.name == nc.partColor) != null)
                    {
                        if (!hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials[1].name.Contains("LuzNula"))
                        {
                            Debug.Log("Change: " + hijoTransform.GetChild(0).gameObject.name + ", from:" + m2[rand2] + ", to:" + m2[2]);
                            Material[] nuevosMateriales = hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                            nuevosMateriales[1] = m2[2]; // Al tratarse de una tarea de arreglar las luces, las luces deben estar fundidas con su correspondiente material.
                            hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials = nuevosMateriales;
                        }
                        else
                        {
                            Debug.Log("No changes: " + hijoTransform.GetChild(0).gameObject.name);
                        }
                    }
                    else if (m3.Find(material => material.name == nc.partColor) != null)
                    {
                        if (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials.Length > 1 && (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials[1].name.Contains(nc.partColor)))
                        {
                            do
                            {
                                rand3 = rnd.Next(m3.Count);
                            } while (rand3 == rand2);
                            Debug.Log("Change: " + hijoTransform.GetChild(0).gameObject.name + ", from:" + m3[rand2] + ", to:" + m3[rand3]);
                            Material[] nuevosMateriales = hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials;
                            nuevosMateriales[1] = m3[rand3];
                            hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials = nuevosMateriales;
                        }
                        else if (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials.Length == 1 && (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material.name.Contains(nc.partColor)))
                        {
                            do
                            {
                                rand3 = rnd.Next(m3.Count);
                            } while (rand3 == rand1);
                            Debug.Log("Change: " + hijoTransform.GetChild(0).gameObject.name + ", from:" + m3[rand1] + ", to:" + m3[rand3]);
                            hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material = m3[rand3];
                        }
                        else
                        {
                            Debug.Log("No changes: " + hijoTransform.GetChild(0).gameObject.name);
                        }
                    }
                }
            }

        }
    }
}
