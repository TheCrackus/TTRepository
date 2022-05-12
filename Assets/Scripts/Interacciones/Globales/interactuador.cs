using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuador : MonoBehaviour
{
    [Header("Evento que activa un simbolo")]
    [SerializeField] private evento simboloActivoDesactivo;
    [Header("Interactua?")]
    [SerializeField] private bool playerEnRango;
    public void setSimboloActivoDesactivo(evento simboloActivoDesactivo) 
    {
        this.simboloActivoDesactivo = simboloActivoDesactivo;
    }

    public evento getSimboloActivoDesactivo() 
    {
        return simboloActivoDesactivo;
    }

    public void setPlayerEnRango(bool playerEnRango) 
    {
        this.playerEnRango = playerEnRango;
    }

    public bool getPlayerEnRango() 
    {
        return playerEnRango;
    }

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
