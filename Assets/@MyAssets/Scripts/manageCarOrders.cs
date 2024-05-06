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

    private clientOrders.ClientOrder cco;
    public List<Material> m1;
    public List<Material> m2;
    public List<Material> m3;
    private System.Random rnd = new System.Random();

    private Coroutine mainCoroutine;
    [SerializeField] private playerDataSO playerDataSO;
    private GameObject carObject;

    [SerializeField] private List<List<GameObject>> listaObjetosPreviosACambio = new List<List<GameObject>>(4);

    [SerializeField] private List<TMP_Text> listaNecesariChanges;

    private bool orderAccepterdBool;
    private void Awake()
    {
        cco = repairOrderWindow.GetComponent<clientOrders>().currentClientOrder;

        for (int i = 0; i < 4; i++) listaObjetosPreviosACambio.Add(new List<GameObject>());

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

    public void acceptOrder()
    {
        carObject = Instantiate(cco.clientCar);
        carObject.name = carObject.name.Replace("(Clone)", "");
        carObject.transform.position = new Vector3(16.1690006f, 0, -10.4090004f);
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
                            listaObjetosPreviosACambio[counter].Add(hijoTransform);
                            //Debug.Log(hijoTransform);
                        }
                    }
                }

            }
            counter++;
        }

        mainCoroutine = StartCoroutine(lockCarOrders(5f));
        orderAccepterdBool = true;
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
            buttonAccept.SetActive(true);
            buttonReject.SetActive(true);
            l.SetActive(leftWasActive);
            r.SetActive(rightWasActive);
            playerDataSO.canceledOrders++;
            Debug.Log("Espera cancelada.");
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
        buttonCancelar.SetActive(true);

        yield return new WaitForSeconds(time);

        eliminateAndAddNewOrder();
        buttonCancelar.SetActive(false);
        l.SetActive(leftWasActive);
        r.SetActive(rightWasActive);
        buttonAccept.SetActive(true);
        buttonReject.SetActive(true);
        orderAccepterdBool = false;

        foreach(TMP_Text i in listaNecesariChanges)
        {
            i.color = Color.white;
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
                                foreach (GameObject l in listaObjetosPreviosACambio[contador])
                                {
                                    if (!l.name.StartsWith(s))
                                    {
                                        // No se hace nada.
                                    }
                                    else if (l.name.StartsWith(s) && hijoTransform.name != l.name)
                                    {
                                        if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                                        {
                                            //BIEN - text[contador] = Color.Green;
                                            listaNecesariChanges[contador].color = Color.green;
                                            break;
                                        }
                                        else
                                        {
                                            //MAL - text[contador] = Color.Red;
                                            listaNecesariChanges[contador].color = Color.red;
                                            break;
                                        }
                                    }
                                    else if(l.name.StartsWith(s) && hijoTransform.name == l.name)
                                    {
                                        //MAL - text[contador] = Color.Red;
                                        listaNecesariChanges[contador].color = Color.red;
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
                                    listaNecesariChanges[contador].color = Color.green;
                                    break;
                                }
                                else if (hijoTransform.GetComponent<Renderer>().materials[1].name.Contains(cnc.partColor))
                                {
                                    //BIEN - text[contador] = Color.Green;
                                    listaNecesariChanges[contador].color = Color.green;
                                    break;
                                }
                                else
                                {
                                    //MAL - text[contador] = Color.Red;
                                    listaNecesariChanges[contador].color = Color.red;
                                    break;
                                }
                            }
                            else
                            {
                                if (hijoTransform.GetComponent<Renderer>().material.name.Contains(cnc.partColor))
                                {
                                    //BIEN - text[contador] = Color.Green;
                                    listaNecesariChanges[contador].color = Color.green;
                                    break;
                                }
                                else
                                {
                                    //MAL - text[contador] = Color.Red;
                                    listaNecesariChanges[contador].color = Color.red;
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
                                listaNecesariChanges[contador].color = Color.green;
                                break;
                            }
                            else
                            {
                                //MAL - text[contador] = Color.Red;
                                listaNecesariChanges[contador].color = Color.red;
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
                                listaNecesariChanges[contador].color = Color.green;
                                break;
                            }
                            else
                            {
                                //MAL - text[contador] = Color.Red;
                                listaNecesariChanges[contador].color = Color.red;
                                break;
                            }
                        }
                    }
                }
            }



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
                        else if (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().materials.Length <= 1 && (hijoTransform.GetChild(0).gameObject.GetComponent<Renderer>().material.name.Contains(nc.partColor)))
                        {
                            do
                            {
                                rand3 = rnd.Next(m3.Count);
                            } while (rand3 == rand2);
                            Debug.Log("Change: " + hijoTransform.GetChild(0).gameObject.name + ", from:" + m3[rand2] + ", to:" + m3[rand3]);
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
