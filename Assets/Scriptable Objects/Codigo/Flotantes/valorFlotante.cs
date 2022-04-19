using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class valorFlotante : ScriptableObject, ISerializationCallbackReceiver
{
    
    public float valorInicial;

    public float valorEjecucion;

    public void OnAfterDeserialize()
    {
        valorEjecucion = valorInicial;
    }

    public void OnBeforeSerialize()
    {
        
    }
}