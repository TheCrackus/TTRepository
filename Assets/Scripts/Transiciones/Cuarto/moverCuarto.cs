using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class moverCuarto : transicion
{
    private movimientoCamara movCam;
    [Header("Vector suma a la posicion del player")]
    public Vector3 cambioSumaPoscicionPlayer;
    [Header("Vector suma a la posicion de la camara (maxima y minima)")]
    public Vector3 cambioSumaPosicionCamara;
    [Header("Lugar destino de la transicion")]
    public GameObject moverCuartoRef;

    public void Start()
    {
        movCam = Camera.main.GetComponent<movimientoCamara>();
    }

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            if (EnumAccionContador == accionContador.inicia)
            {
                if (ContadorRegresivoInicia != null) 
                {
                    ContadorRegresivoInicia.invocaFunciones();
                }
                manejadorContador manejadorC = GameObject.FindGameObjectWithTag("ManejadorContador").GetComponent<manejadorContador>();
                moverEscena moverE = manejadorC.GetComponent<moverEscena>();
                foreach (valorString nombre in Escenas) 
                {
                    if (nombre.valorStringEjecucion == EscenaActual.valorStringEjecucion) 
                    {
                        moverE.EscenaCarga = nombre;
                        break;
                    }
                }
                moverE.NuevaDireccionPlayer = new Vector2(NuevaDireccionPlayer.x * (-1), NuevaDireccionPlayer.y * (-1));
                moverE.NuevaPosicionPlayer = gameObject.transform.position + new Vector3(NuevaDireccionPlayer.x * (-1), NuevaDireccionPlayer.y * (-1), 0);
                moverE.NuevaPosicionCamaraMinima = PosicionCamaraMinima.valorVectorialEjecucion;
                moverE.NuevaPosicionCamaraMaxima = PosicionCamaraMaxima.valorVectorialEjecucion;
                moverE.NuevaPosicionCamara = PosicionCamara.valorVectorialInicial;
            }
            else 
            {
                if (EnumAccionContador == accionContador.deten)
                {
                    if (ContadorRegresivoDeten != null) 
                    {
                        ContadorRegresivoDeten.invocaFunciones();
                    }
                }
                else 
                {
                    if (EnumAccionContador == accionContador.reinicia)
                    {
                        if (ContadorRegresivoReinicia != null) 
                        {
                            ContadorRegresivoReinicia.invocaFunciones();
                        }
                    }
                }
            }
            iniciaCanvas();
            StartCoroutine(cambioCuarto(colisionDetectada.gameObject));
        }
    }

    public IEnumerator cambioCuarto(GameObject player)
    {
        if (ManejadorAudioTransicion != null)
        {
            ManejadorAudioTransicion.reproduceAudioTransicion();
        }
        movimientoPlayer movP = player.GetComponent<movimientoPlayer>();
        movP.setEstadoPlayer(estadoGenerico.transicionando);

        if (ObjetoPanel != null) 
        {
            ObjetoPanel.SetActive(true);
        }

        if (PanelAnimator != null) 
        {
            PanelAnimator.Play("FadeOut");
        }
        yield return new WaitForSeconds(FadeOutClip.length);

        estableceDireccionPlayer(player);
        player.transform.position = moverCuartoRef.transform.position + cambioSumaPoscicionPlayer;
        PosicionPlayer.valorVectorialEjecucion = player.transform.position;
        PosicionCamaraMaxima.valorVectorialEjecucion = PosicionCamaraMaxima.valorVectorialEjecucion += cambioSumaPosicionCamara;
        PosicionCamaraMinima.valorVectorialEjecucion = PosicionCamaraMinima.valorVectorialEjecucion += cambioSumaPosicionCamara;
        movCam.gameObject.transform.position = new Vector3(
        movCam.gameObject.transform.position.x + cambioSumaPosicionCamara.x,
        movCam.gameObject.transform.position.y + cambioSumaPosicionCamara.y,
        -10);
        PosicionCamara.valorVectorialEjecucion = movCam.gameObject.transform.position;

        if (ManejadorAudioTransicion != null)
        {
            ManejadorAudioTransicion.reproduceAudioTransicion();
        }
        if (PanelAnimator != null)
        {
            PanelAnimator.Play("FadeIn");
        }
        yield return new WaitForSeconds(FadeInClip.length);

        if (ObjetoPanel != null)
        {
            ObjetoPanel.SetActive(false);
        }
        if (EnumAccionContador == accionContador.deten)
        {
            ContadorRegresivoInicia.invocaFunciones();
        }
        player.GetComponent<movimientoPlayer>().setEstadoPlayer(estadoGenerico.ninguno); ;
        if (DebeMostrarTexto)
        {
            ObjetoTextoEscena.SetActive(true);
            TextoEscena.text = NombreMostrar;
            TextoEscenaAnimator.Play("Mostrar Texto");
            yield return new WaitForSeconds(MostrarTextoClip.length);

            TextoEscenaAnimator.Play("Ocultar Texto");
            yield return new WaitForSeconds(OcultarTextoClip.length);
            ObjetoTextoEscena.SetActive(false);
        }
        Destroy(NCanvas);
    }

    public void estableceDireccionPlayer(GameObject player)
    {
        Animator playerAnimator = player.GetComponent<Animator>();
        if (NuevaDireccionPlayer.x == 0)
        {
            if (NuevaDireccionPlayer.y > 0)
            {
                playerAnimator.SetFloat("MovimientoX", 0f);
                playerAnimator.SetFloat("MovimientoY", 1f);
            }
            else
            {
                if (NuevaDireccionPlayer.y < 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 0f);
                    playerAnimator.SetFloat("MovimientoY", -1f);
                }
            }
        }
        else
        {
            if (NuevaDireccionPlayer.y == 0)
            {
                if (NuevaDireccionPlayer.x > 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 1f);
                    playerAnimator.SetFloat("MovimientoY", 0f);
                }
                else
                {
                    if (NuevaDireccionPlayer.x < 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", -1f);
                        playerAnimator.SetFloat("MovimientoY", 0f);
                    }
                }
            }
        }
    }
}