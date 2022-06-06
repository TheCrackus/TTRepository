using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoMenuTutorial : ComponenteGraficoGenerico
{

    [Header("Imagen donde se muestra el tutorial")]
    [SerializeField] private Image imagenTutorial;

    public Image ImagenTutorial { get => imagenTutorial; set => imagenTutorial = value; }
}
