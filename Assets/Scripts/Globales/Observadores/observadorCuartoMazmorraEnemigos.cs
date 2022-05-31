using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observadorCuartoMazmorraEnemigos : observadorCuartoMazmorra
{
    public puerta[] puertas;

    public void verificaEnemigosActivos() 
    {
        foreach (enemigo enemigo in Enemigos) 
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
            foreach (enemigo enemigo in Enemigos)
            {
                cambiarActivacion(enemigo, true);
            }
            foreach (Jarro rompible in Rompibles)
            {
                cambiarActivacion(rompible, true);
            }
            cierraPuertas();
            CamaraVirtual.SetActive(true);
            MiniMapa.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger)
        {
            foreach (enemigo enemigo in Enemigos)
            {
                cambiarActivacion(enemigo, false);
            }
            foreach (Jarro rompible in Rompibles)
            {
                cambiarActivacion(rompible, false);
            }
            CamaraVirtual.SetActive(false);
            MiniMapa.SetActive(false);
        }
    }
}
