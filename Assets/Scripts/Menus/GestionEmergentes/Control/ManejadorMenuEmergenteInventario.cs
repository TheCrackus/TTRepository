using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergenteInventario : ManejadorMenuEmergente
{

    private ComponenteGraficoEmergente graficos;

    private void Start()
    {
        graficos = (ComponenteGraficoEmergente) ComponenteGrafico;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            iniciarDestruirCanvas();
        }
        else 
        {
            if (Input.GetButtonDown("Pausa") && GameObject.FindGameObjectWithTag("CanvasInventario"))
            {
                iniciarDestruirCanvas();
            }
        }
    }

    public override void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasInventario"))
        {
            Instantiate(graficos.Canvas, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("CanvasInventario"));
        }
    }

}
