using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class cambioEscena : ScriptableObject, ISerializationCallbackReceiver
{

    public string nombreInicial;

    public string nombreEjecucion;

    public bool cambioInicial;

    public bool cambioEjecucion;

    public void OnAfterDeserialize()
    {
        nombreEjecucion = nombreInicial;
        cambioInicial = cambioEjecucion;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
