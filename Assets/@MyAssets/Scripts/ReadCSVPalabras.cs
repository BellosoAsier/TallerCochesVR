using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ReadCSVPalabras : MonoBehaviour
{
    public List<string> retrieveOptions(TextAsset file)
    {
        string[] data = file.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        List<string> newdata = new List<string>();
        List<string> newdata2 = new List<string>();
        foreach (string arrItem in data)
        {
            newdata.Add(arrItem);
        }
        newdata = Shuffle(newdata);
        string[] opcion = newdata[0].Split(' ');
        foreach (string arrItem in opcion)
        {
            newdata2.Add(arrItem);
        }

        return newdata2;
    }
    public List<string> ReadCSVFile(TextAsset file)
    {
        string[] data = file.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        List<string> newdata = new List<string>();
        foreach (string arrItem in data)
        {
            newdata.Add(arrItem);
        }
        newdata = Shuffle(newdata);
        List<string> listaFinal = new List<string>();
        for(int i = 0; i <newdata.Count; i++)
        {
            listaFinal.Add(newdata[i]);    
        }
        return listaFinal;
    }
    public List<string> Shuffle(List<string> items)
    {
        return items.Distinct().OrderBy(x => System.Guid.NewGuid().ToString()).ToList();
    }
}

