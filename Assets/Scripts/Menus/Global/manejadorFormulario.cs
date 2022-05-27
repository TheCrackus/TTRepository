using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class manejadorFormulario : manejadorMenu, iniciaVentanaEmergente, reproduceAudio, conexion
{

    private GameObject nCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private manejadorVentanaEmergente manejadorVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private conexionWeb conexion;

    public GameObject NCanvasVentanaEmergente { get => nCanvasVentanaEmergente; set => nCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public manejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public conexionWeb Conexion { get => conexion; set => conexion = value; }


    public void iniciaVentanaEmergente()
    {
        if (nCanvasVentanaEmergente == null) 
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nCanvasVentanaEmergente = Instantiate(((componentesGraficosFormulario)Graficos).CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nCanvasVentanaEmergente);
                nCanvasVentanaEmergente = Instantiate(((componentesGraficosFormulario)Graficos).CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<manejadorVentanaEmergente>();
        }
    }

    public void reproduceAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
        }
    }

    public void reproduceAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
        }
    }

    public void reproduceAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        }
    }

}
