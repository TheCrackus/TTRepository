using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manejadorTextoMonedas : MonoBehaviour
{

    public inventario inventarioPlayer;
    public TextMeshProUGUI textoNumeroMonedas;

    public void actualizaNumeroMonedas() 
    {
        textoNumeroMonedas.text = inventarioPlayer.numeroMonedas.ToString("0000");
    }

}
