using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class componentesGraficosVentanaEmergente : componentesGraficos
{

    [Header("El texto que muestra esta ventana ")]
    [SerializeField] private TextMeshProUGUI textoVentanaEmergente;

    public TextMeshProUGUI TextoVentanaEmergente { get => textoVentanaEmergente; set => textoVentanaEmergente = value; }
}
