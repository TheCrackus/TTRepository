using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class manejadorEscenas : MonoBehaviour
{
    private string nombreEscenaActual;

    private PlayableDirector cinematicaInicial;

    [Header("Posicion del Player en el mapa")]
    [SerializeField] private valorVectorial posicionPlayer;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Objeto que contiene las cinematicas")]
    [SerializeField] private GameObject[] contenedoresCinematicas;

    [Header("Empezo nueva partida?")]
    [SerializeField] private valorBooleano empezoPartida;

    [Header("Los datos guardados localmente de la partida")]
    [SerializeField] private datosJuego datos;

    [Header("La escena actual en ejecucion")]
    [SerializeField] valorString escenaActual;

    [Header("Objeto Singleton")]
    [SerializeField] private GameObject singleton;

    void Awake()
    {
        iniciaSingleton();
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        if (contenedoresCinematicas != null)
        {
            foreach (GameObject contenedorCinematica in contenedoresCinematicas)
            {
                if (contenedorCinematica.gameObject.CompareTag("CinematicaInicial"))
                {
                    cinematicaInicial = contenedorCinematica.gameObject.GetComponent<PlayableDirector>();
                }
            }
        } 
        if (empezoPartida.valorBooleanoEjecucion && !estadoCambioEscena.cambieEscenaEjecucion)
        {
            escenaActual.valorStringEjecucion = nombreEscenaActual;
        }
        if (empezoPartida.valorBooleanoEjecucion && !estadoCambioEscena.cambieEscenaEjecucion && escenaActual.valorStringEjecucion == "Laberintos")
        {
            //cinematicaInicial.Play();
        }
        if (escenaActual.valorStringEjecucion != nombreEscenaActual)
        {
            StartCoroutine(cambiaEscena(escenaActual.valorStringEjecucion));
        }
    }

    void OnEnable()
    {
        if (!estadoCambioEscena.cambieEscenaEjecucion && empezoPartida.valorBooleanoEjecucion)
        {
            if (nombreEscenaActual == "Laberintos")
            {
                posicionPlayer.valorVectorialEjecucion = new Vector3(2f, -13.5f, 0);
            }
            else
            {
                if (nombreEscenaActual == "Mazmorra")
                {
                    posicionPlayer.valorVectorialEjecucion = new Vector3(12.5f, -22.5f, 0);
                }
                else
                {
                    if (nombreEscenaActual == "Casa1")
                    {
                        posicionPlayer.valorVectorialEjecucion = new Vector3(8f, -9f, 0);
                    }
                }
            }
            empezoPartida.valorBooleanoEjecucion = false;
        }
        else 
        {
            if (nombreEscenaActual == "Laberintos" 
                || nombreEscenaActual == "Mazmorra"
                || nombreEscenaActual == "Casa1")
            {
                //datos.cargaObjetosScriptable();
            }
        }
    }

    void Start()
    {
        singletonEventosEscenas.instance.ejecutaEventos();
        singletonEventosEscenas.instance.eliminaEventos();
    }

    private void OnApplicationQuit()
    {
        //datos.cargaObjetosScriptable();
    }

    public void iniciaSingleton() 
    {
        if (!GameObject.FindGameObjectWithTag("Singleton")) 
        {
            Instantiate(singleton, Vector3.zero, Quaternion.identity);
        }
    }

    private IEnumerator cambiaEscena(string escena) 
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
