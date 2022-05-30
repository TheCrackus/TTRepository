using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergentePausa : ManejadorMenuEmergente
{

    private ComponenteGraficoEmergente graficos;

    private void Start()
    {
        graficos = (ComponenteGraficoEmergente)ComponenteGrafico;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pausa") 
            && !GameObject.FindGameObjectWithTag("CanvasInventario")
            && !GameObject.FindGameObjectWithTag("CanvasConfiguraciones"))
        {
            iniciarDestruirCanvas();
        }
    }

    public override void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPausa"))
        {
            Instantiate(graficos.Canvas, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("CanvasPausa"));
        }
    }

}
