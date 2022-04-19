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

    public string nombreEjecutarInicial;

    public string nombreEjecutarEjecucion;

    public Vector3 camaraPosicionMaximaInicial;

    public Vector3 camaraPosicionMaximaEjecucion;

    public Vector3 camaraPosicionMinimaInicial;

    public Vector3 camaraPosicionMinimaEjecucion;

    public Vector3 camaraPosicionInicial;

    public Vector3 camaraPosicionEjecucion;

    public void OnAfterDeserialize()
    {
        nombreEjecucion = nombreInicial;
        cambioEjecucion = cambioInicial;
        muestraTextoEjecucion = muestraTextoInicial;
        direccionPlayerEjecucion = direccionPlayerInicial;
        pausoContadorEjecucion = pausoContadorInicial;
        nombreEjecutarEjecucion = nombreEjecutarInicial;
        camaraPosicionMaximaEjecucion = camaraPosicionMaximaInicial;
        camaraPosicionMinimaEjecucion = camaraPosicionMinimaInicial;
        camaraPosicionEjecucion = camaraPosicionInicial;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
