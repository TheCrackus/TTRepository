using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ComponenteGraficoGenerico : MonoBehaviour
{

    [Header("Objeto que contiene todos los componentes graficos de este componente")]
    [SerializeField] private GameObject componenteGraficoPrincipal;

    public GameObject ComponenteGraficoPrincipal { get => componenteGraficoPrincipal; set => componenteGraficoPrincipal = value; }

}

interface IGraficoVentanaEmergente 
{
    public GameObject CanvasVentanaEmergente { get; set; }
}

interface IGraficoCanvasRegistroUsuario 
{
    public GameObject CanvasFormularioRegistroUsuario { get; set; }
}

interface IGraficoCanvasLogIn 
{
    public GameObject CanvasLogIn { get; set; }
}

interface IGraficoCanvasMenuPrincipal 
{
    public GameObject CanvasMenuPrincipal { get; set; }
}

interface IGraficoCanvasFormularioEliminacionUsuario 
{
    public GameObject CanvasFormularioEliminacionUsuario { get; set; }
}

interface IGraficoCanvasFormularioModificacionUsuario 
{
    public GameObject CanvasFormularioModificacionUsuario { get; set; }
}

interface IGraficoCanvasMenuInventario 
{
    public GameObject CanvasMenuInventario { get; set; }
}

interface IGraficoCanvasMenuConfiguraciones 
{
    public GameObject CanvasMenuConfiguraciones { get; set; }
}

interface IGraficoCanvasFormularioEnlaceProfesor 
{
    public GameObject CanvasFormularioEnlaceProfesor { get; set; }
}

interface IGraficoCanvasFormularioRecuperacion
{
    public GameObject CanvasFormularioRecuperacion { get; set; }
}