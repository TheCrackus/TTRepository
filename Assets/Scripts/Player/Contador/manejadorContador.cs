using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class manejadorContador : MonoBehaviour
{
    private bool cuentaTimerRegresivo;
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    private Animator textoCuartoAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    [Header("Duracion del contador")]
    public valorFlotante tiempoContadorRegresivo;
    [Header("Texto donde se mostrara el contador")]
    public GameObject objetoTextoContador;
    public TextMeshProUGUI textoContador;
    [Header("Tipo de transicion")]
    public bool regresaCuarto;
    public bool regresaEscena;
    [Header("Valores para limites de la camara")]
    public Vector3 posicionMaximaCamaraReset;
    public Vector3 posicionMinimaCamaraReset;
    public Vector3 posicionCamaraReset;
    public valorVectorial posicionCamaraMaxima;
    public valorVectorial posicionCamaraMinima;
    public valorVectorial posicionCamara;
    [Header("Valores para la posicion del player")]
    public Vector3 nuevaPoscicionPlayer;
    public Vector2 direccionPlayer;
    public valorVectorial posicionPlayer;
    [Header("Panel para animar la transicion")]
    public GameObject objetoPanel;
    [Header("Valores para mostrar el titulo de los escenarios")]
    public bool debeMostrarTexto;
    public string nombreCuarto;
    public GameObject objetoTextoCuarto;
    public TextMeshProUGUI textoCuarto;
    


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
            if (clip.name == "Mostrar Texto")
            {
                mostrarTextoClip = clip;
            }
            else
            {
                if (clip.name == "Ocultar Texto")
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
            if (tiempoContadorRegresivo.valorFlotanteEjecucion > 0)
            {
                tiempoContadorRegresivo.valorFlotanteEjecucion -= Time.deltaTime;
                muestraTiempo();
            }
            else 
            {
                if (regresaCuarto) 
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    movimientoPlayer movP = player.GetComponent<movimientoPlayer>();
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
        int minutos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion / 60f);
        int segundos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion - minutos * 60f);
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
        tiempoContadorRegresivo.valorFlotanteEjecucion = tiempoContadorRegresivo.valorFlotanteInicial;
    }

    public IEnumerator cambioCuarto(GameObject player)
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        estableceDireccionPlayer(player);
        reiniciaContadorRegresivo();
        player.transform.position = nuevaPoscicionPlayer;
        posicionPlayer.valorVectorialEjecucion = player.transform.position;
        posicionCamaraMaxima.valorVectorialEjecucion = posicionMaximaCamaraReset;
        posicionCamaraMinima.valorVectorialEjecucion = posicionMinimaCamaraReset;
        movCam.gameObject.transform.position = posicionCamaraReset;
        posicionCamara.valorVectorialEjecucion = movCam.gameObject.transform.position;
        yield return new WaitForSeconds(1f);

        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        objetoPanel.SetActive(false);
        player.GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.ninguno);
        if (debeMostrarTexto)
        {

            objetoTextoCuarto.SetActive(true);
            textoCuarto.text = nombreCuarto;
            textoCuartoAnimator.Play("Mostrar Texto");
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoCuartoAnimator.Play("Ocultar Texto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoCuarto.SetActive(false);

        }
    }

    public void estableceDireccionPlayer(GameObject player)
    {
        Animator playerAnimator = player.GetComponent<Animator>();
        if (direccionPlayer.x == 0)
        {
            if (direccionPlayer.y > 0)
            {
                playerAnimator.SetFloat("MovimientoX", 0f);
                playerAnimator.SetFloat("MovimientoY", 1f);
            }
            else
            {
                if (direccionPlayer.y < 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 0f);
                    playerAnimator.SetFloat("MovimientoY", -1f);
                }
            }
        }
        else
        {
            if (direccionPlayer.y == 0)
            {
                if (direccionPlayer.x > 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 1f);
                    playerAnimator.SetFloat("MovimientoY", 0f);
                }
                else
                {
                    if (direccionPlayer.x < 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", -1f);
                        playerAnimator.SetFloat("MovimientoY", 0f);
                    }
                }
            }
        }
    }
}
