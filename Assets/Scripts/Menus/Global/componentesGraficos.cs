using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class componentesGraficos : MonoBehaviour
{

    [Header("Objeto que contiene todos los componentes graficos de este componente")]
    [SerializeField] private GameObject componenteGraficoPrincipal;

    public GameObject ComponenteGraficoPrincipal { get => componenteGraficoPrincipal; set => componenteGraficoPrincipal = value; }

    public void cierraFormulario()
    {
        Destroy(ComponenteGraficoPrincipal);
    }

}

interface ventanaEmergente 
{
    public GameObject CanvasVentanaEmergente { get; set; }
}

interface ordenInputs 
{
    public EventSystem Sistema { get; set; }

    public Selectable PrimerInput { get ; set; }

}