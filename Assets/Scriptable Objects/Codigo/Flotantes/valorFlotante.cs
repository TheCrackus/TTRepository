using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class valorFlotante : ScriptableObject
{
    
    public float valorFlotanteInicial;

    public float valorFlotanteEjecucion;

    public void reiniciaValores()
    {
        valorFlotanteEjecucion = valorFlotanteInicial;
    }

}
