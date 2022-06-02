using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergenteCuestionario : ManejadorMenuEmergente
{

    private ComponenteGraficoEmergente graficos;

    public override void Awake()
    {
        graficos = (ComponenteGraficoEmergente)ComponenteGrafico;
        TagComponenteGrafico = graficos.CanvasEmergente.tag;
    }

    public override void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag(TagComponenteGrafico))
        {
            Instantiate(graficos.CanvasEmergente, Vector3.zero, Quaternion.identity);
        }
    }

}
