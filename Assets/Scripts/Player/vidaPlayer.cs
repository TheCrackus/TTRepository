using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vidaPlayer : sistemaVida
{
    [Header("Evento que actualiza la vida en pantalla")]
    [SerializeField] private evento eventoVidaPlayer;

    [Header("Manejador para las escenas")]
    [SerializeField] private moverEscenaPuntoControl manejadorEscenaPuntoControl;

    public override void quitaVida(float vidaMenos)
    {
        base.quitaVida(vidaMenos);
        if (VidaObjeto != null)
        {
            VidaObjeto.valorFlotanteEjecucion = VidaActualObjeto;
        }
        eventoVidaPlayer.invocaFunciones();
        if (VidaActualObjeto <= 0)
        {
            manejadorEscenaPuntoControl.iniciaTransicionOut();
        }
    }

}
