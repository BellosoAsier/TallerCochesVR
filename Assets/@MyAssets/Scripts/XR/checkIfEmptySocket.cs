using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class checkIfEmptySocket : MonoBehaviour
{
    public void ccOs()
    {
        this.GetComponent<SphereCollider>().enabled = false;
    }
    public void meterPadre(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.SetParent(transform, false);
        //args.interactableObject.transform.localScale = Vector3.one;
        args.interactableObject.transform.position = transform.position;
        CarScript car = GetComponentInParent<CarScript>();

        string nameSocket = this.transform.name.Replace("Socket", "");
        string[] nombreObjeto = args.interactableObject.transform.name.Split("_");
        args.interactableObject.transform.name = nameSocket + "_" + nombreObjeto[1];
        args.interactableObject.transform.name = args.interactableObject.transform.name.Replace("(Clone)", "");

        var part = args.interactableObject.transform.gameObject.GetComponent<carPart>();
        if (part != null)
        {
            car.scalePart(part);
        }
    }

    public void sacarPapa(SelectExitEventArgs args)
    {
        this.GetComponent<SphereCollider>().enabled = true;
        args.interactableObject.transform.SetParent(null);
    }
}
