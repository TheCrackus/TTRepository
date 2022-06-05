using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventosEscenas : MonoBehaviour
{

    public static SingletonEventosEscenas instance;

    [Header("Eventos que se ejecutaran al cambiar de escena")]
    [SerializeField] private List<Evento> eventos = new List<Evento>();

    [Header("Los datos guardados localmente del juego")]
    [SerializeField] private DatosJuego datos;

    private void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void agregarEvento(Evento evento) 
    {
        eventos.Add(evento);
    }

    public void eliminarEventos() 
    {
        if (eventos != null
            && eventos.Count > 0)
        {
            eventos.Clear();
        }
    }

    public void ejecutarEventos() 
    {
        if (eventos != null
                && eventos.Count > 0)
        {
            foreach (Evento eventoLoop in eventos)
            {
                eventoLoop.invocarFunciones();
            }
        }
    }

    public void reiniciarScriptable() 
    {
        datos.reiniciarScriptable();
    }

    public void reiniciarDatos() 
    {
        datos.reiniciarDatos();
    }

    public void guardarDatos() 
    {
        datos.guardarDatos();
    }

    public void cargarDatos() 
    {
        datos.cargarDatos();
    }

}
