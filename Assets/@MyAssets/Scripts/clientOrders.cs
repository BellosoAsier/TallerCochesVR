using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

    [SerializeField] private List<Sprite> listAvatarsBoys;
    [SerializeField] private List<Sprite> listAvatarsGirls;

    [SerializeField] private filesManager fm;

    [SerializeField] private TMP_Text textPresentacion;
    [SerializeField] private Image imgAvatar;
    [SerializeField] private TMP_Text textDuration;
    [SerializeField] private TMP_Text textPrice;

    [System.Serializable]
    public class ClientOrder
    {
        public Sprite clientImg;
        public string clientName;
        public string clientSurname;
        public string clientPresentation;
        public int orderPrice;
        public float orderDuration;
    }

    [SerializeField] private List<ClientOrder> listClientOrders;

    public int pointerClients = 0;

    [SerializeField] private GameObject initializeMovementArrows;

    // Start is called before the first frame update
    void Awake()
    {
        pointerClients = PlayerPrefs.GetInt("ppap",0);
        initializeMovementArrows.SetActive(true);
        listaNombresH = fm.ReadTXTFile(t_boyNames);
        listaNombresM = fm.ReadTXTFile(t_girlNames);
        listaApellidos = fm.ReadTXTFile(t_surnames);
        listaPresentaciones = fm.ReadTXTFile(t_presentacionCliente);

        listClientOrders = fm.LoadOrderListJSON();

        while (listClientOrders.Count < 5)
        {
            listClientOrders.Add(AddNewClientOrder());
        }

        imgAvatar.sprite = listClientOrders[pointerClients].clientImg;
        textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
        textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
        textDuration.SetText("Duración: " + listClientOrders[pointerClients].orderDuration);

        fm.SaveOrderListJSON(listClientOrders);

    }

    // Update is called once per frame
    void Update()
    {
        imgAvatar.sprite = listClientOrders[pointerClients].clientImg;
        textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
        textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
        textDuration.SetText("Duración: " + listClientOrders[pointerClients].orderDuration);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (pointerClients != 4)
            {
                pointerClients++;
                imgAvatar.sprite = listClientOrders[pointerClients].clientImg;
                textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
                textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
                textDuration.SetText("Duración: " + listClientOrders[pointerClients].orderDuration);
            } 
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (pointerClients != 0)
            {
                pointerClients--;
                imgAvatar.sprite = listClientOrders[pointerClients].clientImg;
                textPresentacion.SetText(listClientOrders[pointerClients].clientPresentation);
                textPrice.SetText("Precio: " + listClientOrders[pointerClients].orderPrice);
                textDuration.SetText("Duración: " + listClientOrders[pointerClients].orderDuration);
            }
        }

        PlayerPrefs.SetInt("ppap",pointerClients);
    }

    private string ChoosePresentationPhrase(string name, string surname, string phrase)
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
        newPhrase = newPhrase.Replace("[x4]", "AUDI R8");

        return newPhrase;
    }

    private ClientOrder AddNewClientOrder()
    {
        ClientOrder newOrder = new ClientOrder();

        int girlOrBoyChance = rnd.Next(2);
        bool isBoy = (girlOrBoyChance == 0) ? false : true;

        newOrder.clientImg = (girlOrBoyChance == 0) ? imgAvatar.sprite = listAvatarsGirls[rnd.Next(0, listAvatarsGirls.Count)] : listAvatarsBoys[rnd.Next(0, listAvatarsBoys.Count)];
        newOrder.clientName = (girlOrBoyChance == 0) ? listaNombresM[rnd.Next(0,30)] : listaNombresH[rnd.Next(0, 30)];
        newOrder.clientSurname = listaApellidos[rnd.Next(0, 30)];
        newOrder.clientPresentation = ChoosePresentationPhrase(newOrder.clientName, newOrder.clientSurname, listaPresentaciones[rnd.Next(0,listaPresentaciones.Count)]);
        newOrder.orderPrice = Mathf.RoundToInt(rnd.Next(1000,7001)/250)*250;
        newOrder.orderDuration = Mathf.RoundToInt(rnd.Next(180, 651) / 25) * 25;

        return newOrder;
    }

}
