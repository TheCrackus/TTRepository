using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPausa : MonoBehaviour
{
    private bool estaPausado;
    [Header("El panel que contiene el menu de pausa")]
    public GameObject panelPausa;
    [Header("Nombre de la escena con el menu principal")]
    public string escenaMenuPrincipal;
    public datosJuego datos;
    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    public cambioEscena estadoCambioEscena;

    void Start()
    {
        estaPausado = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausa")) 
        {
            abreCierraMenuPausa();
        }
    }

    public void abreCierraMenuPausa() 
    {
        estaPausado = !estaPausado;
        if (estaPausado)
        {
            panelPausa.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            panelPausa.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void botonRegresar() 
    {
        abreCierraMenuPausa();
    }

    public void botonMenuPrincipal()
    {
        datos.cargaObjetosScriptable();
        estadoCambioEscena.escenaActualEjecucion = escenaMenuPrincipal;
        StartCoroutine(cargaEscena(escenaMenuPrincipal));
    }

    private IEnumerator cargaEscena(string nombreEscena)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(nombreEscena);
        Time.timeScale = 1f;
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
