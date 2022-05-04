using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class cambioEscena : ScriptableObject
{

    public string nombreTextoCuartoInicial;

    public string nombreTextoCuartoEjecucion;

    public bool cambieEscenaInicial;

    public bool cambieEscenaEjecucion;

    public bool muestraTextoInicial;

    public bool muestraTextoEjecucion;

    public Vector2 direccionPlayerInicial;

    public Vector2 direccionPlayerEjecucion;

    public bool pausoContadorInicial;

    public bool pausoContadorEjecucion;

    public string nombreTansicionDestinoInicial;

    public string nombreTansicionDestinoEjecucion;

    public string escenaActualInicial;

    public string escenaActualEjecucion;

    public void reiniciaValores()
    {
        nombreTextoCuartoEjecucion = nombreTextoCuartoInicial;
        cambieEscenaEjecucion= cambieEscenaInicial;
        muestraTextoEjecucion = muestraTextoInicial;
        direccionPlayerEjecucion = direccionPlayerInicial;
        pausoContadorEjecucion = pausoContadorInicial;
        nombreTansicionDestinoEjecucion = nombreTansicionDestinoInicial;
        escenaActualEjecucion = escenaActualInicial;
    }

}
