using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergenteConfiguraciones : ManejadorMenuEmergente
{

    private ComponenteGraficoEmergente graficos;

    public override void Awake()
    {
        graficos = (ComponenteGraficoEmergente)ComponenteGrafico;
        TagComponenteGrafico = graficos.CanvasEmergente.tag;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pausa") && GameObject.FindGameObjectWithTag(TagComponenteGrafico))
        {
            iniciarDestruirCanvas();
        }
    }

    public override void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag(TagComponenteGrafico))
        {
            Instantiate(graficos.CanvasEmergente, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag(TagComponenteGrafico));
        }
    }

}
