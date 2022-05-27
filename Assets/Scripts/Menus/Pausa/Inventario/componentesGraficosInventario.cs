using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class componentesGraficosInventario : componentesGraficos
{

    [Header("El espacio fisico en la interfaz que muestra un item en el inventario")]
    [SerializeField] private GameObject espacioInventarioVacio;

    [Header("El espacio fisico en la interfaz que contiene el inventario")]
    [SerializeField] private GameObject contenedorInventario;

    [Header("Texto de la interfaz que muestra la descripcion de un item en el inventario")]
    [SerializeField] private TextMeshProUGUI textoDescripcionItem;

    [Header("Boton de la interfaz que permite usar un item")]
    [SerializeField] private GameObject botonUsarItem;

    public GameObject EspacioInventarioVacio { get => espacioInventarioVacio; set => espacioInventarioVacio = value; }
    public GameObject ContenedorInventario { get => contenedorInventario; set => contenedorInventario = value; }
    public TextMeshProUGUI TextoDescripcionItem { get => textoDescripcionItem; set => textoDescripcionItem = value; }
    public GameObject BotonUsarItem { get => botonUsarItem; set => botonUsarItem = value; }

}
