using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class manejadorEscenas : MonoBehaviour
{

    private PlayableDirector cinematicaInicial;
    [Header("Posicion del Player en el mapa")]
    public valorVectorial posicionPlayerMapa;
    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    public cambioEscena estadoCambioEscena;
    [Header("Objeto que contiene las cinematicas")]
    public GameObject[] contenedoresCinematicas;
    [Header("Empezo nueva partida?")]
    public valorBooleano empezoPartida;

    void Awake()
    {
        if (estadoCambioEscena.escenaActualEjecucion == "" || estadoCambioEscena.escenaActualEjecucion == null)
        {
            estadoCambioEscena.escenaActualEjecucion = "Principal";
        }
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
        if (empezoPartida.valorBooleanoEjecucion && estadoCambioEscena.escenaActualEjecucion == "Laberintos")
        {
            cinematicaInicial.Play();
            empezoPartida.valorBooleanoEjecucion = false;
        }
        string nombreTemporal = SceneManager.GetActiveScene().name;
        if (nombreTemporal != estadoCambioEscena.escenaActualEjecucion)
        {
            estadoCambioEscena.escenaActualEjecucion = nombreTemporal;
            StartCoroutine(cambioEscena(nombreTemporal));
        }
    }

    private IEnumerator cambioEscena(string nombreEscena)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(nombreEscena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
