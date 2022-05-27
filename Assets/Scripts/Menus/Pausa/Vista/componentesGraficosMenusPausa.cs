using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentesGraficosMenusPausa : componentesGraficos
{

    private GameObject nICanvas;

    private GameObject nCCanvas;

    [Header("Interfaz grafica que contiene el menu de pausa")]
    [SerializeField] private GameObject panelPausa;

    [Header("Interfaz grafica que contiene el inventario")]
    [SerializeField] private GameObject panelInventario;

    [Header("Interfaz grafica que contiene el Menu de configuraciones")]
    [SerializeField] private GameObject panelConfiguraciones;

    public GameObject PanelPausa { get => panelPausa; set => panelPausa = value; }
    public GameObject PanelInventario { get => panelInventario; set => panelInventario = value; }
    public GameObject PanelConfiguraciones { get => panelConfiguraciones; set => panelConfiguraciones = value; }

}
