using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverEscenaPuntoControl : MoverEscena
{

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

        if (EstadoCambioEscena != null
            && NombreTransicionDestino != null)
        {
            EstadoCambioEscena.cambieEscenaEjecucion = true;
            EstadoCambioEscena.nombreTansicionDestinoEjecucion = NombreTransicionDestino.valorStringEjecucion;
        }

        AsyncOperation accion = SceneManager.LoadSceneAsync(EscenaCarga.valorStringEjecucion);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
