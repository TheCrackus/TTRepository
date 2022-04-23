using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorVentanaEmergente : MonoBehaviour
{
    public GameObject fondoEmergente;
    public Text textoVentanaEmergente;

    public void cierraVentanaEmergente()
    {
        textoVentanaEmergente.text = "Advertencias:\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-";
        if (fondoEmergente.activeInHierarchy) 
        {
            fondoEmergente.SetActive(false);
        }
    }

    public void abreVentanaEmergente(string textoMostrar) 
    {
        textoVentanaEmergente.text = textoMostrar;
        if (!fondoEmergente.activeInHierarchy)
        {
            fondoEmergente.SetActive(true);
        }
    }
}
