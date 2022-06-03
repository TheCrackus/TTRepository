using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoBarraMagica : ComponenteGraficoGenerico
{

    [Header("Objeto que representa la barra de magia")]
    [SerializeField] private Slider barraMagica;

    public Slider BarraMagica { get => barraMagica; set => barraMagica = value; }

}
