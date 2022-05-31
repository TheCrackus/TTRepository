using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class sistemaDaño : MonoBehaviour
{
    [Header("El daño que inflinge este objeto")]
    [SerializeField] private float daño;
    [Header("El objeto al que se le aplica el daño")]
    [SerializeField] private string colisionDetectadaTag;

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag(colisionDetectadaTag)
            && colisionDetectada.isTrigger) 
        {
            SistemaVida sistemaVidaTemporal = colisionDetectada.GetComponent<SistemaVida>();
            if (sistemaVidaTemporal != null) 
            {
                sistemaVidaTemporal.quitarVida(daño);
            }
        }
    }

}
