using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class valorVectorial : ScriptableObject, ISerializationCallbackReceiver
{

    public Vector3 valorInicial;

    public Vector3 valorEjecucion;

    public void OnAfterDeserialize()
    {
        valorEjecucion = valorInicial;
    }

    public void OnBeforeSerialize()
    {

    }
}
