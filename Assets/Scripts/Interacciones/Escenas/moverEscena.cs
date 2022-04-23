using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moverEscena : MonoBehaviour
{
    private movimientoCamara movCam;
    private GameObject objetoPanel;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    private GameObject objetoTextoEscena;
    private Text textoEscena;
    private Animator textoEscenaAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    private GameObject nCanvas;
    private GameObject pCanvas;
    [Header("Escena destino")]
    public string escenaCarga;
    [Header("Valores para la posicion del player")]
    public Vector3 nuevaPosicionPlayer;
    public Vector3 nuevaPosicionCamara;
    public valorVectorial posicionPlayer;
    [Header("Estado actual de la escena")]
    public cambioEscena estadoCambioEscena;
    [Header("Panel para animar la transicion")]
    public GameObject fadeInFadeOutCanvas;
    [Header("Valores para mostrar el titulo de los escenarios")]
    public bool debeMostrarTexto;
    public string nombreMostrar;
    [Header("Valores para la posicion del player")]
    public Vector2 direccionPlayer;
    [Header("Tipo de interaccion con el contador")]
    public bool comienzaContador;
    public bool terminaContador;
    public bool pausaContador;
    [Header("Valores para ejecutar una transicion")]
    public string nombreEjecucion;
    public string nombrePropioEjecucion;
    [Header("Valores para limites de la camara")]
    public Vector3 nuevaPosicionCamaraMaxima;
    public Vector3 nuevaPosicionCamaraMinima;
    [Header("Eventos para controlar el ontador")]
    public evento contadorRegresivoInicia;
    public evento contadorRegresivoDeten;
    public evento contadorRegresivoReinicia;

    public void iniciaCanvas()
    {
        if (fadeInFadeOutCanvas != null)
        {
            if (!GameObject.FindGameObjectWithTag("CanvasEscenas"))
            {
                nCanvas = Instantiate(fadeInFadeOutCanvas, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvas = GameObject.FindGameObjectWithTag("CanvasEscenas");
            }
            pCanvas = GameObject.FindGameObjectWithTag("CanvasPlayer");
            objetoPanel = nCanvas.transform.Find("Panel").gameObject;
            panelAnimator = objetoPanel.GetComponent<Animator>();
            objetoTextoEscena = nCanvas.transform.Find("TextoEscenas").gameObject;
            textoEscena = objetoTextoEscena.GetComponent<Text>();
            textoEscenaAnimator = objetoTextoEscena.GetComponent<Animator>();
            foreach (AnimationClip clip in panelAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "FadeOut")
                {
                    fadeOutClip = clip;
                }
                else
                {
                    if (clip.name == "FadeIn")
                    {
                        fadeInClip = clip;
                    }
                }
            }
            foreach (AnimationClip clip in textoEscenaAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "mostrarTexto")
                {
                    mostrarTextoClip = clip;
                }
                else
                {
                    if (clip.name == "ocultarTexto")
                    {
                        ocultarTextoClip = clip;
                    }
                }
            }
        }
    }

    public void Start()
    {
        if (estadoCambioEscena.cambioEjecucion && estadoCambioEscena.nombreEjecutarEjecucion == nombrePropioEjecucion)
        {
            movCam = Camera.main.GetComponent<movimientoCamara>();
            iniciaCanvas();
            StartCoroutine(cambioEscenaIn());
        }
    }

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
            iniciaCanvas();
            movP.setEstadoActualPlayer(PlayerState.interactuando);
            posicionPlayer.valorEjecucion = nuevaPosicionPlayer;
            estadoCambioEscena.cambioEjecucion = true;
            estadoCambioEscena.nombreEjecucion = nombreMostrar;
            estadoCambioEscena.muestraTextoEjecucion = debeMostrarTexto;
            estadoCambioEscena.direccionPlayerEjecucion = direccionPlayer;
            estadoCambioEscena.nombreEjecutarEjecucion = nombreEjecucion;
            estadoCambioEscena.camaraPosicionMaximaEjecucion = nuevaPosicionCamaraMaxima;
            estadoCambioEscena.camaraPosicionMinimaEjecucion = nuevaPosicionCamaraMinima;
            estadoCambioEscena.camaraPosicionEjecucion = nuevaPosicionCamara;
            if (pausaContador)
            {
                contadorRegresivoDeten.invocaFunciones();
                estadoCambioEscena.pausoContadorEjecucion = true;
            }
            if (terminaContador)
            {
                contadorRegresivoReinicia.invocaFunciones();
            }
            StartCoroutine(cambioEscenaOut());
        }
    }

    public IEnumerator cambioEscenaIn()
    {
        if (estadoCambioEscena.pausoContadorEjecucion)
        {
            contadorRegresivoInicia.invocaFunciones();
            estadoCambioEscena.pausoContadorEjecucion = false;
        }
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        pCanvas.SetActive(true);
        objetoPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.ninguno);
        if (estadoCambioEscena.muestraTextoEjecucion)
        {
            objetoTextoEscena.SetActive(true);
            textoEscena.text = estadoCambioEscena.nombreEjecucion;
            textoEscenaAnimator.Play("mostrarTexto");
            estadoCambioEscena.cambioEjecucion = false;
            estadoCambioEscena.nombreEjecucion = "";
            estadoCambioEscena.muestraTextoEjecucion = false;
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoEscenaAnimator.Play("ocultarTexto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoEscena.SetActive(false);
        }
    }

    private IEnumerator cambioEscenaOut()
    {
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
