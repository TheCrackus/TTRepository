using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manejadorVentanaEmergente : MonoBehaviour
{

    private bool pulseBoton;

    private float contadorTiempoCerrar;

    private bool empiezaContador;

    [Header("El texto que muestra esta ventana ")]
    [SerializeField] private TextMeshProUGUI textoVentanaEmergente;

    [Header("El tiempo en el que esta ventana se cierra (segundos)")]
    [SerializeField] private float tiempoCerrar;

    [Header("El canvas que contiene esta ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfaz manejadorAudioInterfaz;

    void Start()
    {
        contadorTiempoCerrar = tiempoCerrar;
        empiezaContador = true;
        pulseBoton = false;
    }

    void Update()
    {
        if (empiezaContador) 
        {
            contadorTiempoCerrar -= Time.deltaTime;
            if (contadorTiempoCerrar <= 0) 
            {
                contadorTiempoCerrar = tiempoCerrar;
                empiezaContador = false;
                cierraVentanaEmergente();
            }
        }
    }

    public void enviaTexto(string texto) 
    {
        textoVentanaEmergente.text = texto;
    }

    public void botonCierraVentanaEmergente() 
    {
        if (!pulseBoton) 
        {
            manejadorAudioInterfaz.reproduceAudioClickCerrar();
            pulseBoton = true;
            cierraVentanaEmergente();
        }
    }

    public void cierraVentanaEmergente() 
    {
        if (canvasVentanaEmergente != null) 
        {
            Destroy(canvasVentanaEmergente);
        }
    }

    public void reiniciaTiempo() 
    {
        contadorTiempoCerrar = tiempoCerrar;
    }

}
