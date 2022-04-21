using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuador : MonoBehaviour
{
    [Header("Evento que activa un simbolo")]
    public evento simboloActivoDesactivo;
    [Header("Interactua?")]
    public bool playerEnRango;

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = false;
        }
    }
}
