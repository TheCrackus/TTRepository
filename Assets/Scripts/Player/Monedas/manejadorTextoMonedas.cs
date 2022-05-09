using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manejadorTextoMonedas : MonoBehaviour
{
    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("El item que representa las monedas")]
    [SerializeField] private inventarioItem moneda;
    [Header("Texto para mostrar el numero de monedas")]
    public TextMeshProUGUI textoNumeroMonedas;

    void Start()
    {
        actualizaNumeroMonedas();
    }

    public void actualizaNumeroMonedas() 
    {
        if (inventariopPlayerItems.verififcaItem(moneda))
        {
            textoNumeroMonedas.text = moneda.cantidadItem.ToString("0000");
        }
        else 
        {
            textoNumeroMonedas.text = "0000";
        }
    }

}
