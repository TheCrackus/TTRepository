using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observadorCuartoMazmorraEnemigos : observadorCuartoMazmorra
{
    public puerta[] puertas;

    public void verificaEnemigosActivos() 
    {
        foreach (enemigo enemigo in enemigos) 
        {
            if (enemigo.gameObject.activeInHierarchy) 
            {
                return;
            }
        }
        abrePuertas();
    }

    public void cierraPuertas() 
    {
        foreach (puerta puerta in puertas) 
        {
            puerta.cerrar();
        }   
    }

    public void abrePuertas()
    {
        foreach (puerta puerta in puertas)
        {
            puerta.abrir();
        }
    }

    public override void OnTriggerEnter2D(Collider2D colisionDetectada)
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
            cierraPuertas();
        }
    }

    public override void OnTriggerExit2D(Collider2D colisionDetectada)
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
