using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoMenuPrincipal : ComponenteGraficoGenerico, IGraficoVentanaEmergente, IGraficoCanvasFormularioEliminacionUsuario, IGraficoCanvasFormularioModificacionUsuario
{

    [Header("Canvas que contiene el formulario de eliminacion")]
    [SerializeField] private GameObject canvasEliminacionUsuario;

    [Header("Canvas que contiene el formulario de modificacion")]
    [SerializeField] private GameObject canvasModificacionUsuario;

    [Header("Canvas que contiene una ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    public GameObject CanvasFormularioEliminacionUsuario { get => canvasEliminacionUsuario; set => canvasEliminacionUsuario = value; }
    public GameObject CanvasFormularioModificacionUsuario { get => canvasModificacionUsuario; set => canvasModificacionUsuario = value; }
    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }

}
