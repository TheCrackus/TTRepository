using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incrementoEstadisticas : MonoBehaviour
{

    public evento eventoIncrementoEstadistica;
    public valorFlotante vidaPlayer;
    public valorFlotante contenedorCorazones;
    public float incrementoValorEstadistica;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D colision) 
    {
        if (colision.gameObject.CompareTag("Player") && colision.isTrigger) 
        {
            vidaPlayer.valorEjecucion += incrementoValorEstadistica;
            if (vidaPlayer.valorEjecucion > (contenedorCorazones.valorEjecucion * 2f)) 
            {
                vidaPlayer.valorEjecucion = contenedorCorazones.valorEjecucion * 2f;
            }
            eventoIncrementoEstadistica.invocaEventosLista();
            Destroy(this.gameObject);
        }
    }
}
