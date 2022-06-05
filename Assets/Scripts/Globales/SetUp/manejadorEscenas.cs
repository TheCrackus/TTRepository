using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ManejadorEscenas : MonoBehaviour
{
    private string nombreEscenaActual;

    private PlayableDirector cinematicaInicial;

    [Header("Posicion del Player en el mapa")]
    [SerializeField] private ValorVectorial posicionPlayer;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private CambioEscena estadoCambioEscena;

    [Header("Objeto que contiene las cinematicas")]
    [SerializeField] private GameObject[] contenedoresCinematicas;

    [Header("Empezo nueva partida?")]
    [SerializeField] private ValorBooleano empezoPartida;

    [Header("Nombre escena de laberintos")]
    [SerializeField] private ValorString nombreEscenaLaberintos;

    [Header("Nombre escena de mazmorra")]
    [SerializeField] private ValorString nombreEscenaMazmorra;

    [Header("Nombre escena de puzzle")]
    [SerializeField] private ValorString nombreEscenaPuzzle;

    [Header("Nombre escena de jefe final")]
    [SerializeField] private ValorString nombreEscenaJefeFinal;

    [Header("Nombre escena de casa 1")]
    [SerializeField] private ValorString nombreEscenaCasa1;

    [Header("Nombre escena de Log In")]
    [SerializeField] private ValorString nombreEscenaLogIn;

    [Header("Nombre escena principal")]
    [SerializeField] private ValorString nombreEscenaPrincipal;

    [Header("La escena actual en ejecucion en el juago")]
    [SerializeField] ValorString escenaControl;

    [Header("Objeto Singleton")]
    [SerializeField] private GameObject singleton;

    private void Awake()
    {
        iniciarSingleton();
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        if (nombreEscenaActual != nombreEscenaLogIn.valorStringEjecucion
            && nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion
            && escenaControl.valorStringEjecucion != ""
            && nombreEscenaActual != escenaControl.valorStringEjecucion)
        {
            StartCoroutine(cambiarEscena(escenaControl.valorStringEjecucion));
            return;
        }
        if (nombreEscenaActual == nombreEscenaLaberintos.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaMazmorra.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaCasa1.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaPuzzle.valorStringEjecucion
             || nombreEscenaActual == nombreEscenaJefeFinal.valorStringEjecucion)
        {
            escenaControl.valorStringEjecucion = nombreEscenaActual;
        }
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
        
        if (empezoPartida.valorBooleanoEjecucion
            && !estadoCambioEscena.cambieEscenaEjecucion
            && escenaControl.valorStringEjecucion == nombreEscenaLaberintos.valorStringEjecucion)
        {
            //cinematicaInicial.Play();
        }
        if (!estadoCambioEscena.cambieEscenaEjecucion
            && empezoPartida.valorBooleanoEjecucion
            && escenaControl.valorStringEjecucion != "")
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

    private void Start()
    {
        if (SingletonEventosEscenas.instance != null)
        {
            SingletonEventosEscenas.instance.ejecutarEventos();
            SingletonEventosEscenas.instance.eliminarEventos();
        }
    }

    public void iniciarSingleton() 
    {
        if (!GameObject.FindGameObjectWithTag("Singleton")) 
        {
            Instantiate(singleton, Vector3.zero, Quaternion.identity);
        }
    }

    private IEnumerator cambiarEscena(string escena) 
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
