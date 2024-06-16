using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    [SerializeField] private playerDataSO playerData;
    [SerializeField] private GameObject nuevaO;
    [SerializeField] private GameObject continuarO;
    [SerializeField] private GameObject jugarO;
    [SerializeField] private GameObject salirO;
    private int firstTime;
    private void Awake()
    {
        firstTime = PlayerPrefs.GetInt("firstTimePlaying",0);
    }
    private void Update()
    {
        firstTime = PlayerPrefs.GetInt("firstTimePlaying", 0);
    }
    public void hoverEnter()
    {
        this.GetComponent<Image>().color = Color.red;
    }
    public void hoverExit()
    {
        this.GetComponent<Image>().color = Color.white;
    }

    public void jugar()
    {
        StartCoroutine(waitMenu());
    }

    IEnumerator waitMenu()
    {
        yield return new WaitForSeconds(0.5f);
        jugarO.SetActive(false);
        salirO.SetActive(false);
        nuevaO.SetActive(true);
        continuarO.SetActive(true);
        if (firstTime == 0)
        {
            continuarO.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void nueva()
    {
        borrarData();
        initializeStatistics(playerData);
        SceneManager.LoadSceneAsync("SuperTaller");
    }

    public void continuar()
    {
        SceneManager.LoadSceneAsync("SuperTaller");
    }

    public void borrarData()
    {
        DeleteAlmacenItemsList("almacenItemsList.json");
        DeleteAlmacenItemsList("orderList.json");
        PlayerPrefs.DeleteKey("lastSaveDate");
        PlayerPrefs.SetInt("firstTimePlaying", 0);
    }

    private void DeleteAlmacenItemsList(string file)
    {
        string path = Path.Combine(Application.persistentDataPath, file);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File deleted at: " + path);
        }
        else
        {
            Debug.Log("File not found: " + path);
        }
    }


    public void salir()
    {
        Application.Quit();
    }

    private void initializeStatistics(playerDataSO user)
    {
        PlayerPrefs.SetInt("currentMoneyUser1",30000);
        PlayerPrefs.SetInt("completedOrdersUser1", 0);
        PlayerPrefs.SetInt("rejectedOrdersUser1", 0);
        PlayerPrefs.SetInt("cancelledOrdersUser1", 0);
        PlayerPrefs.SetFloat("avgTimePerOrderUser1", 0);
    }
}
