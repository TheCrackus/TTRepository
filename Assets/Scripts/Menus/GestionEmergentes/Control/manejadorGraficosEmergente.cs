using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorGraficosEmergente : manejadorMenu
{

    void Start()
    {
        iniciaCanvas();
    }

    public void iniciaCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn")
            || !GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(((componentesGraficosEmergentes)Graficos).Canvas, Vector3.zero, Quaternion.identity);
        }
    }

}
