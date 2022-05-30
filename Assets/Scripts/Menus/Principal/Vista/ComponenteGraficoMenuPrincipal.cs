using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoMenuPrincipal : ComponenteGraficoGenerico, IGraficoVentanaEmergente, IGraficoCanvasFormularioEliminacionUsuario, IGraficoCanvasFormularioModificacionUsuario, IGraficoCanvasMenuConfiguraciones
{

    [Header("Canvas que contiene el menu de configuraciones")]
    [SerializeField] private GameObject canvasMenuConfiguraciones;

    [Header("Canvas que contiene el formulario de eliminacion")]
    [SerializeField] private GameObject canvasEliminacionUsuario;

    [Header("Canvas que contiene el formulario de modificacion")]
    [SerializeField] private GameObject canvasModificacionUsuario;

    [Header("Canvas que contiene una ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    public GameObject CanvasFormularioEliminacionUsuario { get => canvasEliminacionUsuario; set => canvasEliminacionUsuario = value; }
    public GameObject CanvasFormularioModificacionUsuario { get => canvasModificacionUsuario; set => canvasModificacionUsuario = value; }
    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }
    public GameObject CanvasMenuConfiguraciones { get => canvasMenuConfiguraciones; set => canvasMenuConfiguraciones = value; }
}
