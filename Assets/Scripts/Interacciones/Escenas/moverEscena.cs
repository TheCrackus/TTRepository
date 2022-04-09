using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moverEscena : MonoBehaviour
{

    public string escenaCarga;
    public Vector2 nuevaPosicionPlayer;
    public valorVectorial posicionPlayer;
    public cambioEscena estadoCambioEscenas;
    public GameObject fadeInFadeOutCanvas;
    public bool debeMostrarTexto;
    public string nombreMostrar;
    public Vector2 direccionPlayer;
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

    private void iniciaCanvas()
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
            if (GameObject.FindGameObjectWithTag("CanvasPlayer")) 
            {
                pCanvas = GameObject.FindGameObjectWithTag("CanvasPlayer");
            }
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
        if (estadoCambioEscenas.cambioEjecucion)
        {
            iniciaCanvas();
            StartCoroutine(cambioEscenaIn());
        }
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
            PlayerState estadoPlayer = movP.getEstadoActualPlayer();
            if (estadoPlayer != PlayerState.interactuando
                && estadoPlayer != PlayerState.atacando
                && estadoPlayer != PlayerState.ninguno
                && (estadoPlayer == PlayerState.caminando || estadoPlayer == PlayerState.estuneado))
            {
                iniciaCanvas();
                movP.setEstadoActualPlayer(PlayerState.interactuando);
                posicionPlayer.valorEjecucion = nuevaPosicionPlayer;
                estadoCambioEscenas.cambioEjecucion = true;
                estadoCambioEscenas.nombreEjecucion = nombreMostrar;
                estadoCambioEscenas.muestraTextoEjecucion = debeMostrarTexto;
                estadoCambioEscenas.direccionPlayerEjecucion = direccionPlayer;
                StartCoroutine(cambioEscenaOut());
            }
        }
    }

    private IEnumerator cambioEscenaIn()
    {
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        pCanvas.SetActive(true);
        objetoPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.caminando);
        if (estadoCambioEscenas.muestraTextoEjecucion)
        {
            objetoTextoEscena.SetActive(true);
            textoEscena.text = estadoCambioEscenas.nombreEjecucion;
            textoEscenaAnimator.Play("mostrarTexto");
            estadoCambioEscenas.cambioEjecucion = false;
            estadoCambioEscenas.nombreEjecucion = "";
            estadoCambioEscenas.muestraTextoEjecucion = false;
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
