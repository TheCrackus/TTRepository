using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverEscenaPuntoControl : MoverEscena
{

    [Header("Los datos de la partida en curso")]
    [SerializeField] private DatosJuego datos;

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
