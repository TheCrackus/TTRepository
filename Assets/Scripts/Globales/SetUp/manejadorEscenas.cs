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

    [Header("")]
    [SerializeField] private valorString nombreEscenaLaberintos;

    [Header("")]
    [SerializeField] private valorString nombreEscenaMazmorra;

    [Header("")]
    [SerializeField] private valorString nombreEscenaPuzzle;

    [Header("")]
    [SerializeField] private valorString nombreEscenaJefeFinal;

    [Header("")]
    [SerializeField] private valorString nombreEscenaCasa1;

    [Header("La escena actual en ejecucion")]
    [SerializeField] valorString escenaActual;

    [Header("Objeto Singleton")]
    [SerializeField] private GameObject singleton;

    void Awake()
    {
        iniciaSingleton();
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        if (contenedoresCinematicas != null )
        {
            foreach (GameObject contenedorCinematica in contenedoresCinematicas)
            {
                if (contenedorCinematica != null) 
                {
                    if (contenedorCinematica.gameObject.CompareTag("CinematicaInicial"))
                    {
                        cinematicaInicial = contenedorCinematica.gameObject.GetComponent<PlayableDirector>();
                    }
                }
            }
        } 
        if (nombreEscenaActual == nombreEscenaLaberintos.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaMazmorra.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaCasa1.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaPuzzle.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaJefeFinal.valorStringEjecucion)
        {
            escenaActual.valorStringEjecucion = nombreEscenaActual;
        }
        else
        {
            escenaActual.valorStringEjecucion = "";
        }
        if (empezoPartida.valorBooleanoEjecucion 
            && !estadoCambioEscena.cambieEscenaEjecucion 
            && escenaActual.valorStringEjecucion == nombreEscenaLaberintos.valorStringEjecucion)
        {
            cinematicaInicial.Play();
        }
        if (escenaActual.valorStringEjecucion != nombreEscenaActual
            && escenaActual.valorStringEjecucion != "")
        {
            StartCoroutine(cambiaEscena(escenaActual.valorStringEjecucion));
        }
    }

    void OnEnable()
    {
        if (!estadoCambioEscena.cambieEscenaEjecucion 
            && empezoPartida.valorBooleanoEjecucion
            && escenaActual.valorStringEjecucion != "")
        {
            if (nombreEscenaActual == nombreEscenaLaberintos.valorStringEjecucion)
            {
                posicionPlayer.valorVectorialEjecucion = new Vector3(2f, -13.5f, 0);
            }
            else
            {
                if (nombreEscenaActual == nombreEscenaMazmorra.valorStringEjecucion
                    || nombreEscenaActual == nombreEscenaJefeFinal.valorStringEjecucion)
                {
                    posicionPlayer.valorVectorialEjecucion = new Vector3(12.5f, -22.5f, 0);
                }
                else
                {
                    if (nombreEscenaActual == nombreEscenaCasa1.valorStringEjecucion)
                    {
                        posicionPlayer.valorVectorialEjecucion = new Vector3(8f, -9f, 0);
                    }
                }
            }
            empezoPartida.valorBooleanoEjecucion = false;
        }
        else 
        {
            if (nombreEscenaActual == nombreEscenaLaberintos.valorStringEjecucion
                || nombreEscenaActual == nombreEscenaMazmorra.valorStringEjecucion
                || nombreEscenaActual == nombreEscenaCasa1.valorStringEjecucion
                || nombreEscenaActual == nombreEscenaPuzzle.valorStringEjecucion
                || nombreEscenaActual == nombreEscenaJefeFinal.valorStringEjecucion)
            {
                //Carga la partida del usuario en log in
            }
        }
    }

    void Start()
    {
        if (singletonEventosEscenas.instance != null) 
        {
            singletonEventosEscenas.instance.ejecutaEventos();
            singletonEventosEscenas.instance.eliminaEventos();
        }
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
