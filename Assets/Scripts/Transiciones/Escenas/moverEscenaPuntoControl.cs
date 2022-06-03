using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverEscenaPuntoControl : MoverEscena
{

    [Header("Los datos de la partida en curso")]
    [SerializeField] private DatosJuego datos;

    [Header("El contador regresivo esta en ejecucion?")]
    [SerializeField] private ValorBooleano cuentaTimerRegresivo;

    public override void iniciarTransicionOut() 
    {
        if (cuentaTimerRegresivo != null) 
        {
            if (cuentaTimerRegresivo.valorBooleanoEjecucion)
            {
                if (EnumAccionContador == accionContador.inicia)
                {
                    if (ContadorRegresivoInicia != null)
                    {
                        ContadorRegresivoInicia.invocarFunciones();
                    }
                    if (ContadorRegresivoDeten != null)
                    {
                        ContadorRegresivoDeten.invocarFunciones();
                    }
                    if (EstadoCambioEscena != null)
                    {
                        EstadoCambioEscena.pausoContadorEjecucion = true;
                    }
                    EscenaActual.valorStringEjecucion = SceneManager.GetActiveScene().name;
                    ManejadorContador manejadorC = GameObject.FindGameObjectWithTag("ManejadorContador").GetComponent<ManejadorContador>();
                    MoverEscena moverE = manejadorC.GetComponent<MoverEscena>();
                    foreach (ValorString nombre in Escenas)
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
                            ContadorRegresivoDeten.invocarFunciones();
                        }
                        if (EstadoCambioEscena != null)
                        {
                            EstadoCambioEscena.pausoContadorEjecucion = true;
                        }
                    }
                    else
                    {
                        if (EnumAccionContador == accionContador.reinicia)
                        {
                            if (ContadorRegresivoReinicia != null)
                            {
                                ContadorRegresivoReinicia.invocarFunciones();
                            }
                        }
                    }
                }
            }
        }
        iniciarCanvas();
        MovimientoPlayer movP = null;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            movP = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
        }
        StartCoroutine(cambiarEscenaOut(movP));
    }

    public override IEnumerator cambiarEscenaOut(MovimientoPlayer movP)
    {
        if (ManejadorAudioTransicion != null
            && movP != null
            && ObjetoPanel != null
            && PanelAnimator != null
            && FadeOutClip != null)
        {
            ManejadorAudioTransicion.reproducirAudioTransicion();
            movP.EstadoPlayer.Estado = EstadoGenerico.transicionando;
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeOut");
            yield return new WaitForSeconds(FadeOutClip.length);
        }

        if (EstadoCambioEscena != null)
        {
            EstadoCambioEscena.cambieEscenaEjecucion = true;
        }

        foreach (ValorString nombre in Escenas)
        {
            if (nombre.valorStringEjecucion == EscenaActual.valorStringEjecucion)
            {
                EscenaCarga = nombre;
                break;
            }
        }
        AsyncOperation accion = SceneManager.LoadSceneAsync(EscenaCarga.valorStringEjecucion);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
