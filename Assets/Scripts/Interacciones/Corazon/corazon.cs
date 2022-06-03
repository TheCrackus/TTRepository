using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : IncrementoEstadisticas
{
    [Header("Vida del player")]
    [SerializeField] private ValorFlotante vidaPlayer;

    [Header("Numero de corazones del player")]
    [SerializeField] private ValorFlotante contenedorCorazones;

    [Header("Incremento de vida para el player")]
    [SerializeField] private float incrementoValorEstadistica;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player") 
            && colisionDetectada.isTrigger)
        {
            ManejadorAudioObjetoMapa.reproducirAudioRecojer();
            vidaPlayer.valorFlotanteEjecucion += incrementoValorEstadistica;
            if (vidaPlayer.valorFlotanteEjecucion > (contenedorCorazones.valorFlotanteEjecucion * 2f))
            {
                vidaPlayer.valorFlotanteEjecucion = contenedorCorazones.valorFlotanteEjecucion * 2f;
            }
            EventoIncrementoEstadistica.invocarFunciones();
            Destroy(gameObject);
        }
    }
}
