using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corazon : incrementoEstadisticas
{
    [Header("Vida del player")]
    public valorFlotante vidaPlayer;
    [Header("Numero de corazones del player")]
    public valorFlotante contenedorCorazones;
    [Header("Incremento de vida para el player")]
    public float incrementoValorEstadistica;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player") 
            && colision.isTrigger)
        {
            vidaPlayer.valorFlotanteEjecucion += incrementoValorEstadistica;
            if (vidaPlayer.valorFlotanteEjecucion > (contenedorCorazones.valorFlotanteEjecucion * 2f))
            {
                vidaPlayer.valorFlotanteEjecucion = contenedorCorazones.valorFlotanteEjecucion * 2f;
            }
            eventoIncrementoEstadistica.invocaFunciones();
            Destroy(gameObject);
        }
    }
}
