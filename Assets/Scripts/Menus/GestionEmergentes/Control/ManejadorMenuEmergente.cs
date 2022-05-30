using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergente : ManejadorMenuGenerico
{

    private void Start()
    {
        iniciarDestruirCanvas();
    }

    public virtual void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn")
            || !GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(((ComponenteGraficoEmergente)ComponenteGrafico).Canvas, Vector3.zero, Quaternion.identity);
        }
    }

}
