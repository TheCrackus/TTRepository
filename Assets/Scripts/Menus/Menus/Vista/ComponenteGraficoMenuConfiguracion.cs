using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoMenuConfiguracion : ComponenteGraficoGenerico, IGraficoCanvasMenuPrincipal
{

    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }

}
