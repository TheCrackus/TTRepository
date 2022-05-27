using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorGraficosEmergentePausa : manejadorMenu
{

    void Update()
    {
        if (Input.GetButtonDown("Pausa") && !GameObject.FindGameObjectWithTag("CanvasInventario"))
        {
            iniciaDestruyeCanvas();
        }
    }

    public void iniciaDestruyeCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPausa"))
        {
            Instantiate(((componentesGraficosEmergentes)Graficos).Canvas, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("CanvasPausa"));
        }
    }

}
