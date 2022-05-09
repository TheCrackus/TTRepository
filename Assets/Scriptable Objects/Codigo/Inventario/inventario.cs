using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class inventario : ScriptableObject
{
    [Header("Objeto actual que posee el Player")]
    public objeto objetoActual;
    public List<objeto> objetosEjecucion = new List<objeto>();
    public int numeroLlavesInicial;
    public int numeroLlavesEjecucion;
    public int numeroMonedasInicial;
    public int numeroMonedasEjecucion;

    public bool verificaObjeto(objeto objeto) 
    {
        if (objetosEjecucion.Contains(objeto))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void agregaObjeto(objeto objetoAgrega) 
    {
        if (objetoAgrega.esLlave)
        {
            numeroLlavesEjecucion++;
        }
        else 
        {
            if (!objetosEjecucion.Contains(objetoAgrega)) 
            {
                objetosEjecucion.Add(objetoAgrega);
            }
        }
    }

    public void reiniciaValores()
    {
        numeroLlavesEjecucion = numeroLlavesInicial;
        numeroMonedasInicial = numeroLlavesInicial;
        objetosEjecucion.Clear();
    }
}
