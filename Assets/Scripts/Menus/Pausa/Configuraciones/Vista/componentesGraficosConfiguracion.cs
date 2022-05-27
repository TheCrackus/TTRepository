using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class componentesGraficosConfiguracion : componentesGraficos
{

    [Header("Menu desplegable que contiene las configuraciones")]
    [SerializeField] private TMP_Dropdown dropdownResoluciones;

    public TMP_Dropdown DropdownResoluciones { get => dropdownResoluciones; set => dropdownResoluciones = value; }
}
