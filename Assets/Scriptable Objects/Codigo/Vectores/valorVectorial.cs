using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class valorVectorial : ScriptableObject, ISerializationCallbackReceiver
{

    public Vector2 valorInicial;

    public Vector2 valorEjecucion;

    public void OnAfterDeserialize()
    {
        valorEjecucion = valorInicial;
    }

    public void OnBeforeSerialize()
    {

    }
}
