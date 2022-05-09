using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPausa : MonoBehaviour
{
    private bool estaPausado;
    [Header("Interfaz grafica que contiene el menu de pausa")]
    public GameObject panelPausa;
    [Header("Interfaz grafica que contiene el inventario")]
    public GameObject panelInventario;
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
            if (Input.GetButtonDown("Pausa") && panelInventario.activeInHierarchy)
            {
                abreCierraInventario();
            }
            else 
            {
                if (Input.GetButtonDown("Pausa")) 
                {
                    abreCierraMenuPausa();
                }
            }
        }
        else 
        {
            if (Input.GetButtonDown("Inventario")) 
            {
                abreCierraInventario();
            }
        }
    }

    public void abreCierraMenuPausa() 
    {
        estaPausado = !estaPausado;
        panelPausa.SetActive(estaPausado);
        if (estaPausado)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void abreCierraInventario()
    {
        manejadorBotonesInventario manejadorInventario = panelInventario.GetComponent<manejadorBotonesInventario>();
        manejadorInventario.activaBotonEnviaTexto("", false, null);
        estaPausado = !estaPausado;
        panelInventario.SetActive(estaPausado);
    }

    public void botonRegresar() 
    {
        abreCierraMenuPausa();
    }

    public void botonMenuPrincipal()
    {
        estadoCambioEscena.escenaActualEjecucion = escenaMenuPrincipal;
        StartCoroutine(cargaEscena(escenaMenuPrincipal));
    }

    public void botonInventario() 
    {
        abreCierraMenuPausa();
        abreCierraInventario();
    }

    public void botonReiniciaGuardado() 
    {
        datos.reiniciaObjetosScriptable();
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
