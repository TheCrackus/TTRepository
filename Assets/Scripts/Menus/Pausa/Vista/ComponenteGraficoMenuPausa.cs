using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponenteGraficoMenuPausa : ComponenteGraficoGenerico, IGraficoCanvasMenuInventario, IGraficoCanvasMenuConfiguraciones
{

    [Header("Interfaz grafica que contiene el inventario")]
    [SerializeField] private GameObject canvasMenuInventario;

    [Header("Interfaz grafica que contiene el Menu de configuraciones")]
    [SerializeField] private GameObject canvasMenuConfiguraciones;

    public GameObject CanvasMenuInventario { get => canvasMenuInventario; set => canvasMenuInventario = value; }
    public GameObject CanvasMenuConfiguraciones { get => canvasMenuConfiguraciones; set => canvasMenuConfiguraciones = value; }

}
