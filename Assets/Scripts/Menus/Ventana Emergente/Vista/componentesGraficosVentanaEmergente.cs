using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class componentesGraficosVentanaEmergente : MonoBehaviour
{

    [Header("El canvas que contiene esta ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    [Header("El texto que muestra esta ventana ")]
    [SerializeField] private TextMeshProUGUI textoVentanaEmergente;

    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }
    public TextMeshProUGUI TextoVentanaEmergente { get => textoVentanaEmergente; set => textoVentanaEmergente = value; }
}
