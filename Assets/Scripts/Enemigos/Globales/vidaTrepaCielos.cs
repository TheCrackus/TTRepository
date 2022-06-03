using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaTrepaCielos : SistemaVida
{
    [Header("Evento para cuartos con enemigos (abre puerta)")]
    [SerializeField] private Evento estadoEnemigosCuarto;

    public override void quitarVida(float vidaMenos)
    {
        base.quitarVida(vidaMenos);
        if (VidaActualObjeto <= 0) 
        {
            if (estadoEnemigosCuarto)
            {
                estadoEnemigosCuarto.invocarFunciones();
            }
        }
    }
    
}
