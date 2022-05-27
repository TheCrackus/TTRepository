using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class moverCuarto : transicion
{
    [Header("Vector suma a la posicion del player")]
    public Vector3 cambioSumaPoscicionPlayer;
    [Header("Vector suma a la posicion de la camara (maxima y minima)")]
    public Vector3 cambioSumaPosicionCamara;
    [Header("Lugar destino de la transicion")]
    public GameObject moverCuartoRef;

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
                EscenaActual.valorStringEjecucion = SceneManager.GetActiveScene().name;
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
        movP.EstadoPlayer.Estado = estadoGenerico.transicionando;

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
        movP.EstadoPlayer.Estado = estadoGenerico.ninguno;
        if (DebeMostrarTexto)
        {
            if (ObjetoTextoEscena != null)
            {
                ObjetoTextoEscena.SetActive(true);
            }
            if (TextoEscena != null) 
            {
                TextoEscena.text = NombreMostrar;
            }
            if (TextoEscenaAnimator != null) 
            {
                TextoEscenaAnimator.Play("Mostrar Texto");
            }
            if (MostrarTextoClip != null) 
            {
                yield return new WaitForSeconds(MostrarTextoClip.length);
            }
            

            if (TextoEscenaAnimator != null)
            {
                TextoEscenaAnimator.Play("Ocultar Texto");
            }
            if (OcultarTextoClip != null) 
            {
                yield return new WaitForSeconds(OcultarTextoClip.length);
            }

            if (ObjetoTextoEscena != null)
            {
                ObjetoTextoEscena.SetActive(false);
            }

        }
        Destroy(NCanvas);
    }

    public void estableceDireccionPlayer(GameObject player)
    {
        Animator playerAnimator = null;
        if (player != null) 
        {
            playerAnimator = player.GetComponent<Animator>();
        }
        if (NuevaDireccionPlayer != null) 
        {
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
}