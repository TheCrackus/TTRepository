using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class valorBooleano : ScriptableObject, ISerializationCallbackReceiver
{
    public bool valorInicial;

    public bool valorEjecucion;

    public void OnAfterDeserialize()
    {
        valorEjecucion = valorInicial;
    }

    public void OnBeforeSerialize()
    {

    }
}
