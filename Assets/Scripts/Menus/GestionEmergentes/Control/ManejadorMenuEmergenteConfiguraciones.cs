using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergenteConfiguraciones : ManejadorMenuEmergente
{

    private ComponenteGraficoEmergente graficos;

    private void Start()
    {
        graficos = (ComponenteGraficoEmergente)ComponenteGrafico;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausa") && GameObject.FindGameObjectWithTag("CanvasConfiguraciones"))
        {
            iniciarDestruirCanvas();
        }
    }

    public override void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasConfiguraciones"))
        {
            Instantiate(graficos.Canvas, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("CanvasConfiguraciones"));
        }
    }

}
