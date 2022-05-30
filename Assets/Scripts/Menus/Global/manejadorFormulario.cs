using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManejadorFormulario : ManejadorMenuGenerico, ICanvasVentanaEmergente, IReproductorAudio, IConexion, IBotonPulso
{

    private ComponenteGraficoFormulario graficosFormulario;

    private bool pulseBoton;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

    private GameObject nuevoCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private ManejadorVentanaEmergente manejadorVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private conexionWeb conexion;

    public GameObject NuevoCanvasVentanaEmergente { get => nuevoCanvasVentanaEmergente; set => nuevoCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public ManejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public conexionWeb Conexion { get => conexion; set => conexion = value; }

    public void iniciarVentanaEmergente()
    {
        graficosFormulario = (ComponenteGraficoFormulario)ComponenteGrafico;
        if (nuevoCanvasVentanaEmergente == null) 
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nuevoCanvasVentanaEmergente = Instantiate(graficosFormulario.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nuevoCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nuevoCanvasVentanaEmergente);
                nuevoCanvasVentanaEmergente = Instantiate(graficosFormulario.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nuevoCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<ManejadorVentanaEmergente>();
        }
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        }
    }

    public void reiniciarBotones()
    {
        pulseBoton = false;
    }

    public void bloquearBotones() 
    {
        pulseBoton = true;
    }

}
