using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorContador : MonoBehaviour
{

    public valorFlotante tiempoContadorRegresivo;
    public GameObject objetoTextoContador;
    public Text textoContador;
    private bool cuentaTimerRegresivo;
    public bool regresaCuarto;
    public bool regresaEscena;
    public Vector2 posicionMaximaCamaraReset;
    public Vector2 posicionMinimaCamaraReset;
    public Vector3 posicionNuevaPlayer;
    public GameObject objetoPanel;
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    public bool debeMostrarTexto;
    public string nombreCuarto;
    public GameObject objetoTextoCuarto;
    public Text textoCuarto;
    private Animator textoCuartoAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;


    public void Start()
    {
        cuentaTimerRegresivo = false;
        movCam = Camera.main.GetComponent<movimientoCamara>();
        panelAnimator = objetoPanel.GetComponent<Animator>();
        textoCuartoAnimator = objetoTextoCuarto.GetComponent<Animator>();
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
        foreach (AnimationClip clip in textoCuartoAnimator.runtimeAnimatorController.animationClips)
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

    public void Update()
    {
        if (cuentaTimerRegresivo) 
        {
            if (tiempoContadorRegresivo.valorEjecucion > 0)
            {
                tiempoContadorRegresivo.valorEjecucion -= Time.deltaTime;
                muestraTiempo();
            }
            else 
            {
                if (regresaCuarto) 
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    movimientoPlayer movP = player.GetComponent<movimientoPlayer>();
                    PlayerState estadoPlayer = movP.getEstadoActualPlayer();
                    movP.setEstadoActualPlayer(PlayerState.interactuando);
                    StartCoroutine(cambioCuarto(player));
                }
                else 
                {
                    if (regresaEscena) 
                    {
                        //Despues
                    }
                }
            }
        }
    }

    public void muestraTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoContadorRegresivo.valorEjecucion / 60f);
        int segundos = Mathf.FloorToInt(tiempoContadorRegresivo.valorEjecucion - minutos * 60f);
        textoContador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void detenContadorRegresivo() 
    {
        cuentaTimerRegresivo = false;
        objetoTextoContador.SetActive(false);
    }

    public void iniciaContadorRegresivo() 
    {
        cuentaTimerRegresivo = true;
        objetoTextoContador.SetActive(true);
    }

    public void reiniciaContadorRegresivo() 
    {
        cuentaTimerRegresivo = false;
        objetoTextoContador.SetActive(false);
        tiempoContadorRegresivo.valorEjecucion = tiempoContadorRegresivo.valorInicial;
    }

    public IEnumerator cambioCuarto(GameObject player)
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        reiniciaContadorRegresivo();
        player.transform.position = posicionNuevaPlayer;
        movCam.posicionMaxima = posicionMaximaCamaraReset;
        movCam.posicionMinima = posicionMinimaCamaraReset;
        yield return new WaitForSeconds(1f);

        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        objetoPanel.SetActive(false);
        player.GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.caminando);
        if (debeMostrarTexto)
        {

            objetoTextoCuarto.SetActive(true);
            textoCuarto.text = nombreCuarto;
            textoCuartoAnimator.Play("mostrarTexto");
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoCuartoAnimator.Play("ocultarTexto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoCuarto.SetActive(false);

        }
    }
}
