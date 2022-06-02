using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuEmergente : ManejadorMenuGenerico
{

    string tagComponenteGrafico;

    public string TagComponenteGrafico { get => tagComponenteGrafico; set => tagComponenteGrafico = value; }

    public virtual void Awake()
    {
        tagComponenteGrafico = ((ComponenteGraficoEmergente)ComponenteGrafico).CanvasEmergente.tag;
        iniciarDestruirCanvas();
    }

    public virtual void iniciarDestruirCanvas()
    {
        if (!GameObject.FindGameObjectWithTag(tagComponenteGrafico))
        {
            Instantiate(((ComponenteGraficoEmergente)ComponenteGrafico).CanvasEmergente, Vector3.zero, Quaternion.identity);
        }
    }

}
