using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponenteGraficoEmergente : ComponenteGraficoGenerico
{

    [Header("Canvas que contiene el menu a mostrar")]
    [SerializeField] private GameObject canvasEmergente;

    public GameObject CanvasEmergente { get => canvasEmergente; set => canvasEmergente = value; }

}
