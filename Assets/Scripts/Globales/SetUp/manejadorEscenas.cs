using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class manejadorEscenas : MonoBehaviour
{
    [Header("El nombre de la escena en ejecucion")]
    [SerializeField]private string nombreEscenaActual;
    [Header("La primer cinematica a ejecutar")]
    [SerializeField]private PlayableDirector cinematicaInicial;
    [Header("Posicion del Player en el mapa")]
    public valorVectorial poscicioPlayer;
    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    public cambioEscena estadoCambioEscena;
    [Header("Objeto que contiene las cinematicas")]
    public GameObject[] contenedoresCinematicas;
    [Header("Empezo nueva partida?")]
    public valorBooleano empezoPartida;
    [Header("Los datos guardados localmente de la partida")]
    public datosJuego datos;

    void Awake()
    {
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
        if (empezoPartida.valorBooleanoEjecucion && !estadoCambioEscena.cambieEscenaEjecucion && nombreEscenaActual == "Laberintos")
        {
            //cinematicaInicial.Play();
        }
        estadoCambioEscena.ultimaEscenaGuardadaEjecucion = nombreEscenaActual;
    }

    void OnEnable()
    {
        if (!estadoCambioEscena.cambieEscenaEjecucion && empezoPartida.valorBooleanoEjecucion) 
        {
            if (nombreEscenaActual == "Laberintos")
            {
                //datos.cargaObjetosScriptable();
                poscicioPlayer.valorVectorialEjecucion = new Vector3(2f, -13.5f, 0);
            }
            else
            {
                if (nombreEscenaActual == "Mazmorra")
                {
                    //datos.cargaObjetosScriptable();
                    poscicioPlayer.valorVectorialEjecucion = new Vector3(12.5f, -22.5f, 0);
                }
                else 
                {
                    if (nombreEscenaActual == "Casa1") 
                    {
                        //datos.cargaObjetosScriptable();
                        poscicioPlayer.valorVectorialEjecucion = new Vector3(8f, -9f, 0);
                    }
                }
            }
            empezoPartida.valorBooleanoEjecucion = false;
        }
    }

    private void OnApplicationQuit()
    {
        datos.reiniciaObjetosScriptable();
    }
}
