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

    public bool muestraTextoInicial;

    public bool muestraTextoEjecucion;

    public Vector2 direccionPlayerInicial;

    public Vector2 direccionPlayerEjecucion;

    public bool pausoContadorInicial;

    public bool pausoContadorEjecucion;

    public void OnAfterDeserialize()
    {
        nombreEjecucion = nombreInicial;
        cambioEjecucion = cambioInicial;
        muestraTextoEjecucion = muestraTextoInicial;
        direccionPlayerEjecucion = direccionPlayerInicial;
        pausoContadorEjecucion = pausoContadorInicial;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
