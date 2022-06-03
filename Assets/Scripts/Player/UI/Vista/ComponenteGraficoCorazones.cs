using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoCorazones : ComponenteGraficoGenerico
{

    [Header("Numero de imagenes maximas")]
    [SerializeField] private Image[] imagenesCorazones;

    [Header("Sprites para la cantidad de vida")]
    [SerializeField] private Sprite corazonLleno;

    [SerializeField] private Sprite corazonMitad;

    [SerializeField] private Sprite corazonVacio;

    public Image[] ImagenesCorazones { get => imagenesCorazones; set => imagenesCorazones = value; }
    public Sprite CorazonLleno { get => corazonLleno; set => corazonLleno = value; }
    public Sprite CorazonMitad { get => corazonMitad; set => corazonMitad = value; }
    public Sprite CorazonVacio { get => corazonVacio; set => corazonVacio = value; }
}
