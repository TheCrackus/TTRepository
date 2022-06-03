using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ManejadorFormulario : ManejadorMenuGenerico, ICanvasVentanaEmergente, IReproductorAudioInterfazGrafica, IConexion, IBotonPulso
{
    
    private ComponenteGraficoFormulario graficosF;

    private bool pulseBoton;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

    private GameObject nuevoCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private ManejadorVentanaEmergente manejadorVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private ConexionWeb conexion;

    public GameObject NuevoCanvasVentanaEmergente { get => nuevoCanvasVentanaEmergente; set => nuevoCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public ManejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public ConexionWeb Conexion { get => conexion; set => conexion = value; }

    public void iniciarVentanaEmergente()
    {
        graficosF = (ComponenteGraficoFormulario)ComponenteGrafico;
        if (nuevoCanvasVentanaEmergente == null) 
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nuevoCanvasVentanaEmergente = Instantiate(graficosF.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nuevoCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nuevoCanvasVentanaEmergente);
                nuevoCanvasVentanaEmergente = Instantiate(graficosF.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nuevoCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<ManejadorVentanaEmergente>();
        }
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioAbrirVentana();
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
