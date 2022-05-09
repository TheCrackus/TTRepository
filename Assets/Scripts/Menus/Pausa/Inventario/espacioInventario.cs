using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class espacioInventario : MonoBehaviour
{
    [Header("El texto que describe la cantidad que posee el Player de este item")]
    [SerializeField] private TextMeshProUGUI textoCantidadItem;
    [Header("La imagen que contendra el sprite del item")]
    [SerializeField] private Image imagenItem;
    [Header("El objeto(ScriptableObject) que contiene el item")]
    public inventarioItem item;
    [Header("El manejador de la interfaz grafica del inventario")]
    public manejadorBotonesInventario manejadorInventario;

    public void setUp(inventarioItem nuevoItem, manejadorBotonesInventario nuevoManejadorInventario) 
    {
        item = nuevoItem;
        manejadorInventario = nuevoManejadorInventario;
        if (item != null) 
        {
            imagenItem.sprite = item.imagenItem;
            textoCantidadItem.text = "" + item.cantidadItem;

        }
    }

    public void botonItem() 
    {
        if (item) 
        {
            manejadorInventario.activaBotonEnviaTexto(item.descripcionItem, item.esUsabe, item);
        }
    }
}
