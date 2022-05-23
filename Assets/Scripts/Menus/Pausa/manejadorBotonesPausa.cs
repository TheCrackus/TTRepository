using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPausa : MonoBehaviour
{

    [Header("Pulse un boton de la interfaz?")]
    [SerializeField] private bool pulseBoton;

    [Header("Esta pausado el juego?")]
    [SerializeField] private bool estaPausado;

    [Header("Interfaz grafica que contiene el menu de pausa")]
    [SerializeField] private GameObject panelPausa;

    [Header("Interfaz grafica que contiene el inventario")]
    [SerializeField] private GameObject panelInventario;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Los datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfaz manejadorAudioInterfaz;

    void Start()
    {
        estaPausado = false;
        pulseBoton = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausa")) 
        {
            if (Input.GetButtonDown("Pausa") && panelInventario.activeInHierarchy)
            {
                manejadorAudioInterfaz.reproduceAudioClickCerrar();
                abreCierraInventario();
            }
            else 
            {
                if (Input.GetButtonDown("Pausa")) 
                {
                    abreCierraMenuPausa();
                    if (panelPausa.activeInHierarchy)
                    {
                        manejadorAudioInterfaz.reproduceAudioClickAbrir();
                    }
                    else 
                    {
                        manejadorAudioInterfaz.reproduceAudioClickCerrar();
                    }
                }
            }
        }
        else 
        {
            if (Input.GetButtonDown("Inventario")) 
            {
                abreCierraInventario();
                if (panelInventario.activeInHierarchy)
                {
                    manejadorAudioInterfaz.reproduceAudioClickAbrir();
                }
                else
                {
                    manejadorAudioInterfaz.reproduceAudioClickCerrar();
                }
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
        if (pulseBoton)
        {
            pulseBoton = false;
        }
    }

    public void abreCierraInventario()
    {
        manejadorBotonesInventario manejadorInventario = panelInventario.GetComponent<manejadorBotonesInventario>();
        manejadorInventario.activaBotonEnviaTexto("", false, null);
        estaPausado = !estaPausado;
        panelInventario.SetActive(estaPausado);
        if (estaPausado)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (pulseBoton) 
        {
            pulseBoton = false;
        }
    }

    public void botonRegresar() 
    {
        if (!pulseBoton) 
        {
            manejadorAudioInterfaz.reproduceAudioClickCerrar();
            abreCierraMenuPausa();
            pulseBoton = true;
        }
    }

    public void botonMenuPrincipal()
    {
        if (!pulseBoton)
        {
            manejadorAudioInterfaz.reproduceAudioClickCerrar();
            StartCoroutine(cargaEscena(escenaMenuPrincipal.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void botonInventario() 
    {
        if (!pulseBoton)
        {
            manejadorAudioInterfaz.reproduceAudioClickAbrir();
            abreCierraMenuPausa();
            abreCierraInventario();
            pulseBoton = true;
        }
    }

    public void botonReiniciaGuardado() 
    {
        if (!pulseBoton)
        {
            manejadorAudioInterfaz.reproduceAudioClickAbrir();
            //--------------
            datos.reiniciaValoresScriptable();
            //--------------
            StartCoroutine(cargaEscena(escenaMenuPrincipal.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    private IEnumerator cargaEscena(string nombreEscena)
    {
        Time.timeScale = 1f;
        AsyncOperation accion = SceneManager.LoadSceneAsync(nombreEscena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
