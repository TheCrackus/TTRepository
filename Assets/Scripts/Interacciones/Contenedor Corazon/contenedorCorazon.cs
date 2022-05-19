using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contenedorCorazon : incrementoEstadisticas
{

    [Header("Numero de corazones que posee el jugador")]
    [SerializeField] private valorFlotante corazonesMaximos;
    [Header("La vida actual del jugador")]
    [SerializeField] private valorFlotante vidaActualPlayer;
    [Header("Estoy obtenido?")]
    [SerializeField] private valorBooleano contenedorCorazonObtenido;

    void Start()
    {
        if (contenedorCorazonObtenido != null) 
        {
            if (contenedorCorazonObtenido.valorBooleanoEjecucion) 
            {
                Destroy(gameObject);
            }
        }
    }

    private  void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            if (contenedorCorazonObtenido) 
            {
                contenedorCorazonObtenido.valorBooleanoEjecucion = true;
            }
            corazonesMaximos.valorFlotanteEjecucion += 1;
            vidaActualPlayer.valorFlotanteEjecucion = corazonesMaximos.valorFlotanteEjecucion * 2;
            getEventoIncrementoEstadistica().invocaFunciones();
            Destroy(gameObject);
        }
    }
}
