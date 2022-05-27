using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorLogInEmergente : manejadorMenu
{

    void Start()
    {
        iniciaCanvas();
    }

    public void iniciaCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn"))
        {
            Instantiate(((componentesGraficosEmergentes)Graficos).Canvas, Vector3.zero, Quaternion.identity);
        }
    }

}
