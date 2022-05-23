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

    [Header("Manejador de conexiones")]
    [SerializeField] private conexionWeb conexion;

    [Header("Componentes graficos del formulario")]
    [SerializeField] private componentesGraficosFormulario graficos;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfaz manejadorAudioInterfaz;

    [Header("Evento que reinicia el click de los botones")]
    [SerializeField] private evento eventoReiniciaBotones;

    [Header("Evento que cierra una sesion activa")]
    [SerializeField] private evento eventoCierraSesion; 

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public GameObject NCanvasVentanaEmergente { get => nCanvasVentanaEmergente; set => nCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public manejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public conexionWeb Conexion { get => conexion; set => conexion = value; }
    public componentesGraficosFormulario Graficos { get => graficos; set => graficos = value; }
    public audioInterfaz ManejadorAudioInterfaz { get => manejadorAudioInterfaz; set => manejadorAudioInterfaz = value; }
    public evento EventoReiniciaBotones { get => eventoReiniciaBotones; set => eventoReiniciaBotones = value; }
    public evento EventoCierraSesion { get => eventoCierraSesion; set => eventoCierraSesion = value; }

    public void iniciaVentanaEmergente()
    {
        if (nCanvasVentanaEmergente == null) 
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nCanvasVentanaEmergente = Instantiate(graficos.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nCanvasVentanaEmergente);
                nCanvasVentanaEmergente = Instantiate(graficos.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<manejadorVentanaEmergente>();
        }
    }

    public void reiniciaBotones() 
    {
        pulseBoton = false;
    }
}
