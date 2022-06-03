using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponenteGraficoContador : ComponenteGraficoGenerico
{

    [Header("Contenedor del texto donde se mostrara el contador")]
    [SerializeField] private GameObject objetoTextoContador;

    [Header("Texto donde se mostrara el contador")]
    [SerializeField] private TextMeshProUGUI textoContador;

    public GameObject ObjetoTextoContador { get => objetoTextoContador; set => objetoTextoContador = value; }
    public TextMeshProUGUI TextoContador { get => textoContador; set => textoContador = value; }
}
