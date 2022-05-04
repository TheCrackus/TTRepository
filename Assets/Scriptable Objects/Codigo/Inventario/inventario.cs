using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class inventario : ScriptableObject
{
    [Header("Objeto actual que posee el Player")]
    public objeto objetoActual;
    [Header("")]
    public List<objeto> objetosEjecucion = new List<objeto>();
    public int numeroLlavesInicial;
    public int numeroLlavesEjecucion;
    public int numeroMonedasInicial;
    public int numeroMonedasEjecucion;
    public float magiaInicial;
    public float magiaEjecucion;

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

    public void decrementaMagia(float costoMagia) 
    {
        magiaEjecucion -= costoMagia;
        verificaMagia();
    }

    public void aumentaMagia(float aumentoMagia)
    {
        magiaEjecucion += aumentoMagia;
        verificaMagia();
    }

    public void verificaMagia() 
    {
        if (magiaEjecucion >= magiaInicial)
        {
            magiaEjecucion = magiaInicial;
        }
        else
        {
            if (magiaEjecucion <= 0)
            {
                magiaEjecucion = 0;
            }
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
        magiaInicial = magiaEjecucion;
        objetosEjecucion.Clear();
    }
}
