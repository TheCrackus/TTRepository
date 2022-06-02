using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoMenuPrincipal : ComponenteGraficoGenerico, IGraficoCanvasFormularioEliminacionUsuario, IGraficoCanvasFormularioModificacionUsuario, IGraficoCanvasMenuConfiguraciones, IGraficoCanvasFormularioEnvioPrueba
{

    [Header("Canvas que contiene el menu de configuraciones")]
    [SerializeField] private GameObject canvasMenuConfiguraciones;

    [Header("Canvas que contiene el formulario de eliminacion")]
    [SerializeField] private GameObject canvasFormularioEliminacionUsuario;

    [Header("Canvas que contiene el formulario de modificacion")]
    [SerializeField] private GameObject canvasFormularioModificacionUsuario;

    [Header("Canvas que contiene el formulario de envio de prueba")]
    [SerializeField] private GameObject canvasFormularioEnvioPrueba;

    [Header("Texto que contiene el nombre del jugador")]
    [SerializeField] private TextMeshProUGUI textoNombreJugador;

    public GameObject CanvasMenuConfiguraciones { get => canvasMenuConfiguraciones; set => canvasMenuConfiguraciones = value; }
    public GameObject CanvasFormularioEliminacionUsuario { get => canvasFormularioEliminacionUsuario; set => canvasFormularioEliminacionUsuario = value; }
    public GameObject CanvasFormularioModificacionUsuario { get => canvasFormularioModificacionUsuario; set => canvasFormularioModificacionUsuario = value; }
    public GameObject CanvasFormularioEnvioPrueba { get => canvasFormularioEnvioPrueba; set => canvasFormularioEnvioPrueba = value; }
    public TextMeshProUGUI TextoNombreJugador { get => textoNombreJugador; set => textoNombreJugador = value; }

}
