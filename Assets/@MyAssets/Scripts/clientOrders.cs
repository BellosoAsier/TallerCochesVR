using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class clientOrders : MonoBehaviour
{
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

    [SerializeField] private ReadCSVPalabras rcp;

    [SerializeField] private TMP_Text textPresentacion;
    [SerializeField] private Image imgAvatar;

    private bool isBoy = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        int girlOrBoyChance = rnd.Next(2);
        isBoy = (girlOrBoyChance == 0) ? false : true;
        listaNombresH = rcp.ReadCSVFile(t_boyNames);
        listaNombresM = rcp.ReadCSVFile(t_girlNames);
        listaApellidos = rcp.ReadCSVFile(t_surnames);
        listaPresentaciones = rcp.ReadCSVFile(t_presentacionCliente);

        if (isBoy)
        {
            imgAvatar.sprite = listAvatarsBoys[rnd.Next(0, listAvatarsBoys.Count)];
            textPresentacion.SetText(choosePresentationPhrase(listaNombresH[0], listaApellidos[0], listaPresentaciones[0]));
        }
        else
        {
            imgAvatar.sprite = listAvatarsBoys[rnd.Next(0, listAvatarsGirls.Count)];
            textPresentacion.SetText(choosePresentationPhrase(listaNombresM[0], listaApellidos[0], listaPresentaciones[0]));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string choosePresentationPhrase(string name, string surname, string phrase)
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

}
