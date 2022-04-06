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
    private GameObject objetoPanel;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    public bool debeMostrarTexto;
    private GameObject objetoTextoEscena;
    public string nombreMostrar;
    private Text textoCuarto;
    private Animator textoEscenaAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    private GameObject nCanvas;

    public void Start()
    {
        iniciaCanvas();
        if (estadoCambioEscenas.cambioEjecucion == true)
        {
            StartCoroutine(cambioEscenaIn());
        }
    }

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
            objetoPanel = nCanvas.transform.Find("Panel").gameObject;
            panelAnimator = objetoPanel.GetComponent<Animator>();
            objetoTextoEscena = nCanvas.transform.Find("TextoEscenas").gameObject;
            textoCuarto = objetoTextoEscena.GetComponent<Text>();
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

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger
            && nCanvas != null)
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
                StartCoroutine(cambioEscenaOut());
            }
        }
    }

    private IEnumerator cambioEscenaIn()
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);
        
        objetoPanel.SetActive(false);
        estadoCambioEscenas.cambioEjecucion = false;
        estadoCambioEscenas.nombreEjecucion = "";
    }

    private IEnumerator cambioEscenaOut()
    {
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
