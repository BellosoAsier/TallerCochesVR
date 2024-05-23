using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class clientOrders : MonoBehaviour
{
    [SerializeField] private int numberOrders;

    private System.Random rnd = new System.Random();
    [SerializeField] private TextAsset t_boyNames;
    private List<string> listaNombresH = new List<string>();
    [SerializeField] private TextAsset t_girlNames;
    private List<string> listaNombresM = new List<string>();
    [SerializeField] private TextAsset t_surnames;
    private List<string> listaApellidos = new List<string>();
    [SerializeField] private TextAsset t_presentacionCliente;
    private List<string> listaPresentaciones = new List<string>();
    //[SerializeField] private TextAsset t_necessaryChanges;
    //private List<string> listaCambiosNecesarios = new List<string>();

    [SerializeField] private gameManagerSO gameDataSO;
    //[SerializeField] private List<Sprite> listAvatarsBoys;
    //[SerializeField] private List<Sprite> listAvatarsGirls;

    [SerializeField] private filesManager fm;

    [SerializeField] private TMP_Text textPresentacion;
    [SerializeField] private Image imgAvatar;
    [SerializeField] private TMP_Text textDuration;
    [SerializeField] private TMP_Text textPrice;

    [SerializeField] private List<GameObject> listCarsGameObjects;

    [System.Serializable]
    public class NecessaryChange
    {
        public string name;
        public List<string> carPart;
        public string partColor;
    }
    [SerializeField] private List<NecessaryChange> listNecessaryChangesCar1;
    [SerializeField] private List<NecessaryChange> listNecessaryChangesCar2;

    [System.Serializable]
    public class OrderExtra
    {
        public string name;
        public string statistic;
        public bool isHigher;
        public int value;
    }
    [SerializeField] private List<OrderExtra> listOrderExtras;

    [System.Serializable]
    public class ClientOrder
    {
        public GameObject clientCar;
        public string clientImg;
        public string clientGender;
        public string clientName;
        public string clientSurname;
        public string clientPresentation;
        public List<NecessaryChange> clientCarChanges;
        public List<OrderExtra> clientOrderExtras;
        public int orderPrice;
        public int orderDuration;
    }

    public List<ClientOrder> listClientOrders;

    public ClientOrder currentClientOrder;
    public int pointerClients = 0;

    [SerializeField] private GameObject initializeMovementArrows;
    [SerializeField] private List<string> listNameColors;
    [SerializeField] private List<string> listNameTints;
    [SerializeField] private List<string> listNameTypeLight;
    [SerializeField] private List<TMP_Text> textListNecessaryChanges;
    [SerializeField] private List<TMP_Text> textListOrderExtras;


    // Start is called before the first frame update
    void Awake()
    {
        pointerClients = PlayerPrefs.GetInt("ppap",0);
        initializeMovementArrows.SetActive(true);
        listaNombresH = fm.ReadTXTFile(t_boyNames, true);
        listaNombresM = fm.ReadTXTFile(t_girlNames, true);
        listaApellidos = fm.ReadTXTFile(t_surnames, true);
        listaPresentaciones = fm.ReadTXTFile(t_presentacionCliente, false);

        foreach (ClientOrder co in fm.LoadOrderListJSON())
        {
            Debug.Log(co.clientCar);
        }

        listClientOrders = fm.LoadOrderListJSON();

        while (listClientOrders.Count < 5)
        {
            listClientOrders.Add(AddNewClientOrder());
        }

        if (listClientOrders[pointerClients].clientGender.Equals("Female"))
        {
            foreach (Sprite s in gameDataSO.femaleAvatarImagesGeneralList)
            {
                if (s.name == listClientOrders[pointerClients].clientImg)
                {
                    imgAvatar.sprite = s;
                }
            }
        }
        else
        {
            foreach (Sprite s in gameDataSO.maleAvatarImagesGeneralList)
            {
                if (s.name == listClientOrders[pointerClients].clientImg)
                {
                    imgAvatar.sprite = s;
                }
            }
        }
        
        textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
        textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
        textDuration.SetText("Duración: " + secondsToMinutesAndSecondsText(listClientOrders[pointerClients].orderDuration));

        for (int i = 0; i < 4; i++)
        {
            textListNecessaryChanges[i].text = "- "+listClientOrders[pointerClients].clientCarChanges[i].name;
            textListOrderExtras[i].text = listClientOrders[pointerClients].clientOrderExtras[i].name;
        }
        //fm.SaveOrderListJSON(listClientOrders);

    }

    // Update is called once per frame
    void Update()
    {
        if (listClientOrders[pointerClients].clientGender.Equals("Female"))
        {
            foreach (Sprite s in gameDataSO.femaleAvatarImagesGeneralList)
            {
                if (s.name == listClientOrders[pointerClients].clientImg)
                {
                    imgAvatar.sprite = s;
                }
            }
        }
        else
        {
            foreach (Sprite s in gameDataSO.maleAvatarImagesGeneralList)
            {
                if (s.name == listClientOrders[pointerClients].clientImg)
                {
                    imgAvatar.sprite = s;
                }
            }
        }
        textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
        textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
        textDuration.SetText("Duración: " + secondsToMinutesAndSecondsText(listClientOrders[pointerClients].orderDuration));

        for (int i = 0; i < 4; i++)
        {
            textListNecessaryChanges[i].text = "- " + listClientOrders[pointerClients].clientCarChanges[i].name;
            textListOrderExtras[i].text = listClientOrders[pointerClients].clientOrderExtras[i].name;
        }

        currentClientOrder = listClientOrders[pointerClients];
        PlayerPrefs.SetInt("ppap",pointerClients);
    }

    private void OnApplicationQuit()
    {
        fm.SaveOrderListJSON(listClientOrders);
    }

    private string secondsToMinutesAndSecondsText(int seconds)
    {
        int minutos = seconds / 60; // Obtenemos los minutos dividiendo por 60
        int segundos = seconds % 60; // Obtenemos los segundos restantes con el operador módulo (%)

        return (minutos + " min " + segundos + " s"); 
    }

    private string choosePresentationPhrase(string name, string surname, string phrase, string car)
    {
        string dayOrAfternoon = (rnd.Next(2) == 0) ? "Buenos días" : "Buenas tardes";
        string newPhrase = phrase;

        string nameLower = name.ToLower();
        string nameUpperFirst = char.ToUpper(nameLower[0]) + nameLower.Substring(1);

        string surnameLower = surname.ToLower();
        string surnameUpperFirst = char.ToUpper(surnameLower[0]) + surnameLower.Substring(1);

        newPhrase = newPhrase.Replace("[x1]", dayOrAfternoon);
        newPhrase = newPhrase.Replace("[x2]", nameUpperFirst);
        newPhrase = newPhrase.Replace("[x3]", surnameUpperFirst);
        newPhrase = newPhrase.Replace("[x4]", car);

        return newPhrase;
    }

    private List<NecessaryChange> chooseNecessaryChanges(List<NecessaryChange> l)
    {
        List<NecessaryChange> copyListN = new List<NecessaryChange>();
        for(int i = 0; i < l.Count; i++)
        {
            string chosenColor = listNameColors[rnd.Next(listNameColors.Count)];
            string chosenTint = listNameTints[rnd.Next(listNameTints.Count)];
            string chosenTypeLight = listNameTypeLight[rnd.Next(listNameTypeLight.Count)];

            List<string> finalNecessaryChanges = l[i].carPart.OrderBy(item => rnd.Next()).Take(UnityEngine.Random.Range(1, l[i].carPart.Count+1)).ToList();

            string phrase = "[";
            foreach(string s in finalNecessaryChanges)
            {
                phrase += s + ", ";
            }
            phrase += "]";

            phrase = phrase.Replace(", ]", "]");

            NecessaryChange nc = new NecessaryChange();

            string nameNecessaryChange = l[i].name;

            if (nameNecessaryChange.Contains("[color]"))
            {
                nc.partColor = chosenColor;
            }
            else if (nameNecessaryChange.Contains("[tinte]"))
            {
                nc.partColor = chosenTint;
            }
            else if (nameNecessaryChange.Contains("[tipo_luz]"))
            {
                nc.partColor = chosenTypeLight;
            }

            nameNecessaryChange = nameNecessaryChange.Replace("[color]", chosenColor);
            nameNecessaryChange = nameNecessaryChange.Replace("[tinte]", chosenTint);
            nameNecessaryChange = nameNecessaryChange.Replace("[tipo_luz]", chosenTypeLight);
            nameNecessaryChange = nameNecessaryChange.Replace("[cuales]", phrase);
            nc.name = nameNecessaryChange;

            List<string> carPartChange = l[i].carPart;
            nc.carPart = carPartChange;

            nc.carPart = finalNecessaryChanges;

            copyListN.Add(nc);
        }
        return copyListN;
    }

    private List<OrderExtra> chooseOrderExtras(List<OrderExtra> l)
    {
        List<OrderExtra> copyListO = new List<OrderExtra>();
        for (int i = 0; i < l.Count; i++)
        {
            int higherOrLower = rnd.Next(2);
            int value = rnd.Next(180, 280);

            OrderExtra oe = new OrderExtra();

            string nameOrderExtra = l[i].name;
            nameOrderExtra = nameOrderExtra.Replace("[value]", value + "");
            nameOrderExtra = nameOrderExtra.Replace("[X]", (higherOrLower == 0) ? ">" : "<");
            oe.name = nameOrderExtra;

            string statisticOrder = l[i].statistic;
            oe.statistic = statisticOrder;

            oe.isHigher = (higherOrLower == 0) ? true : false;

            oe.value = value;

            copyListO.Add(oe);
        }
        return copyListO;
    }

    public ClientOrder AddNewClientOrder()
    {
        ClientOrder newOrder = new ClientOrder();

        int girlOrBoyChance = rnd.Next(2);
        int audiOrFordChance = rnd.Next(2);

        List<NecessaryChange> finalPersonalListNecCar1 = listNecessaryChangesCar1.OrderBy(item => rnd.Next()).Take(4).ToList();
        List<NecessaryChange> finalPersonalListNecCar2 = listNecessaryChangesCar2.OrderBy(item => rnd.Next()).Take(4).ToList();
        List<OrderExtra> finalPersonalExtraList = listOrderExtras.OrderBy(item => rnd.Next()).Take(4).ToList();

        newOrder.clientCar = (audiOrFordChance == 0) ? listCarsGameObjects[0] : listCarsGameObjects[1];
        newOrder.clientGender = (girlOrBoyChance == 0) ? "Female" : "Male";
        newOrder.clientImg = (girlOrBoyChance == 0) ? gameDataSO.femaleAvatarImagesGeneralList[rnd.Next(0, gameDataSO.femaleAvatarImagesGeneralList.Count)].name : gameDataSO.maleAvatarImagesGeneralList[rnd.Next(0, gameDataSO.maleAvatarImagesGeneralList.Count)].name;
        newOrder.clientName = (girlOrBoyChance == 0) ? listaNombresM[rnd.Next(0,30)] : listaNombresH[rnd.Next(0, 30)];
        newOrder.clientSurname = listaApellidos[rnd.Next(0, 30)];
        newOrder.clientPresentation = choosePresentationPhrase(newOrder.clientName, newOrder.clientSurname, listaPresentaciones[rnd.Next(0,listaPresentaciones.Count)], (audiOrFordChance == 0) ? "Audi" : "Ford");
        newOrder.clientCarChanges = (audiOrFordChance == 0) ? chooseNecessaryChanges(finalPersonalListNecCar1) : chooseNecessaryChanges(finalPersonalListNecCar2);
        newOrder.clientOrderExtras = chooseOrderExtras(finalPersonalExtraList);
        newOrder.orderPrice = Mathf.RoundToInt(rnd.Next(1000,7001)/250)*250;
        newOrder.orderDuration = Mathf.RoundToInt(rnd.Next(20, 120) / 25) * 25;

        return newOrder;
    }

}
