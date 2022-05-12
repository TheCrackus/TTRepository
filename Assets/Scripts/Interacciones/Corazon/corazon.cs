using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corazon : incrementoEstadisticas
{
    [Header("Vida del player")]
    [SerializeField] private valorFlotante vidaPlayer;
    [Header("Numero de corazones del player")]
    [SerializeField] private valorFlotante contenedorCorazones;
    [Header("Incremento de vida para el player")]
    [SerializeField] private float incrementoValorEstadistica;

    public override void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        base.OnTriggerEnter2D(colisionDetectada);
        if (colisionDetectada.gameObject.CompareTag("Player") 
            && colisionDetectada.isTrigger)
        {
            vidaPlayer.valorFlotanteEjecucion += incrementoValorEstadistica;
            if (vidaPlayer.valorFlotanteEjecucion > (contenedorCorazones.valorFlotanteEjecucion * 2f))
            {
                vidaPlayer.valorFlotanteEjecucion = contenedorCorazones.valorFlotanteEjecucion * 2f;
            }
            getEventoIncrementoEstadistica().invocaFunciones();
            Destroy(gameObject);
        }
    }
}
