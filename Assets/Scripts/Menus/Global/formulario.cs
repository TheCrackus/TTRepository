using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class formulario : MonoBehaviour
{

    private bool pulseBoton;

    private GameObject nCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private manejadorVentanaEmergente manejadorVentanaEmergente;

    [Header("Evento que reinicia el click de los botones")]
    [SerializeField] private evento eventoReiniciaBotones;

    [Header("Evento que reinicia cierra una sesion activa")]
    [SerializeField] private evento eventoCierraSesion;

    [Header("Objeto que contiene todos los componentes de este formulario")]
    [SerializeField] private GameObject canvasFormulario;

    [Header("Canvas de la ventana emergente que muestra informacion")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfaz manejadorAudioInterfaz;

    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }
    public audioInterfaz ManejadorAudioInterfaz { get => manejadorAudioInterfaz; set => manejadorAudioInterfaz = value; }
    public GameObject CanvasFormulario { get => canvasFormulario; set => canvasFormulario = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public evento EventoReiniciaBotones { get => eventoReiniciaBotones; set => eventoReiniciaBotones = value; }
    public GameObject NCanvasVentanaEmergente { get => nCanvasVentanaEmergente; set => nCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public manejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public evento EventoCierraSesion { get => eventoCierraSesion; set => eventoCierraSesion = value; }

    public void iniciaVentanaEmergente()
    {
        if (nCanvasVentanaEmergente == null) 
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nCanvasVentanaEmergente = Instantiate(canvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
            }
        }
        if (nCanvasVentanaEmergente != null)
        {
            ventanaEmergente = nCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
        }
        if (ventanaEmergente != null) 
        {
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<manejadorVentanaEmergente>();
        }
    }

    public void reiniciaBotones() 
    {
        pulseBoton = false;
    }
}
