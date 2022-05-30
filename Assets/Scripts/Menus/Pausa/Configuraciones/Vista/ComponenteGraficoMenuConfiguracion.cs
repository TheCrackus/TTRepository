using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoMenuConfiguracion : ComponenteGraficoGenerico, IGraficoCanvasMenuPrincipal
{

    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Menu desplegable que contiene las configuraciones")]
    [SerializeField] private TMP_Dropdown dropdownResoluciones;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }

    public TMP_Dropdown DropdownResoluciones { get => dropdownResoluciones; set => dropdownResoluciones = value; }
}
