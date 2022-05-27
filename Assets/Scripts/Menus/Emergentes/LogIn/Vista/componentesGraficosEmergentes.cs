using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentesGraficosEmergentes : componentesGraficos
{

    [Header("Canvas que contiene el menu a mostrar")]
    [SerializeField] private GameObject canvas;

    public GameObject Canvas { get => canvas; set => canvas = value; }
}
