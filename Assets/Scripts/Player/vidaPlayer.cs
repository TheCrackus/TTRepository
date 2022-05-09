using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaPlayer : sistemaVida
{
    [Header("Evento que actualiza la vida en pantalla")]
    [SerializeField] private evento eventoVidaPlayer;

    public override void quitaVida(float vidaMenos)
    {
        base.quitaVida(vidaMenos);
        getVidaMaxima().valorFlotanteEjecucion = getVidaActual();
        eventoVidaPlayer.invocaFunciones();
        if (getVidaActual() <= 0)
        {
            getObjeto().SetActive(false);
        }
    }

}
