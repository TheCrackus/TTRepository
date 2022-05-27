using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorGraficosEmergenteInventario : manejadorMenu
{
    void Update()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            iniciaDestruyeCanvas();
        }
        else 
        {
            if (Input.GetButtonDown("Pausa") && GameObject.FindGameObjectWithTag("CanvasInventario"))
            {
                iniciaDestruyeCanvas();
            }
        }
    }

    public void iniciaDestruyeCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasInventario"))
        {
            Instantiate(((componentesGraficosEmergentes)Graficos).Canvas, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("CanvasInventario"));
        }
    }

}
