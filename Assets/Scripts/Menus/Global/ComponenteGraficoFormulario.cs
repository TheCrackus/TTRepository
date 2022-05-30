using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoFormulario : ComponenteGraficoGenerico, IGraficoVentanaEmergente
{

    [Header("Canvas que contiene una ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }

}
