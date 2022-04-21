using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manejadorTextoMonedas : MonoBehaviour
{
    [Header("Inventaruio del Player")]
    public inventario inventarioPlayer;
    [Header("Texto para mostrar el numero de monedas")]
    public TextMeshProUGUI textoNumeroMonedas;

    public void actualizaNumeroMonedas() 
    {
        textoNumeroMonedas.text = inventarioPlayer.numeroMonedas.ToString("0000");
    }

}
