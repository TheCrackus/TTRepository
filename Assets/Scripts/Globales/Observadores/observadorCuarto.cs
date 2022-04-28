using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observadorCuarto : MonoBehaviour
{
    public enemigo[] enemigos;
    public jarro[] rompibles;

    public void cambiaActivacion(Component componente, bool estadoActivacion) 
    {
        if (componente.gameObject.activeInHierarchy && !estadoActivacion)
        {
            componente.gameObject.SetActive(false);
        }
        else 
        {
            if (!componente.gameObject.activeInHierarchy && estadoActivacion) 
            {
                componente.gameObject.SetActive(true);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada) 
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger) 
        {
            foreach (enemigo enemigo in enemigos) 
            {
                cambiaActivacion(enemigo, true);
            }
            foreach (jarro rompible in rompibles)
            {
                cambiaActivacion(rompible, true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger)
        {
            foreach (enemigo enemigo in enemigos)
            {
                cambiaActivacion(enemigo, false);
            }
            foreach (jarro rompible in rompibles)
            {
                cambiaActivacion(rompible, false);
            }
        }
    }
}
