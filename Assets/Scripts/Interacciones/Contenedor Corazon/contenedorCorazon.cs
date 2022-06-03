using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorCorazon : IncrementoEstadisticas
{

    [Header("Numero de corazones que posee el jugador")]
    [SerializeField] private ValorFlotante corazonesMaximos;

    [Header("La vida actual del jugador")]
    [SerializeField] private ValorFlotante vidaActualPlayer;

    [Header("Estoy obtenido?")]
    [SerializeField] private ValorBooleano contenedorCorazonObtenido;

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
            ManejadorAudioObjetoMapa.reproducirAudioRecojer();
            if (contenedorCorazonObtenido) 
            {
                contenedorCorazonObtenido.valorBooleanoEjecucion = true;
            }
            corazonesMaximos.valorFlotanteEjecucion += 1;
            vidaActualPlayer.valorFlotanteEjecucion = corazonesMaximos.valorFlotanteEjecucion * 2;
            EventoIncrementoEstadistica.invocarFunciones();
            Destroy(gameObject);
        }
    }
}
