using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuador : MonoBehaviour
{

    public evento simboloActivoDesactivo;
    public bool playerEnRango;

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaEventosLista();
            playerEnRango = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaEventosLista();
            playerEnRango = false;
        }
    }
}
