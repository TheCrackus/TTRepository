using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaTrepaCielos : sistemaVida
{
    [Header("Evento para cuartos con enemigos (abre puerta)")]
    [SerializeField] private evento estadoEnemigosCuarto;

    public override void quitaVida(float vidaMenos)
    {
        base.quitaVida(vidaMenos);
        if (getVidaActual() <= 0) 
        {
            if (estadoEnemigosCuarto) 
            {
                estadoEnemigosCuarto.invocaFunciones();
            }
            getObjeto().SetActive(false);
            muerteAnimacion();
            procesaLoot();
        }
    }
    
}
