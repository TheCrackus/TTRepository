using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponenteGraficoVentanaEmergente : ComponenteGraficoGenerico
{

    [Header("El texto que muestra esta ventana ")]
    [SerializeField] private TextMeshProUGUI textoVentanaEmergente;

    public TextMeshProUGUI TextoVentanaEmergente { get => textoVentanaEmergente; set => textoVentanaEmergente = value; }
}
