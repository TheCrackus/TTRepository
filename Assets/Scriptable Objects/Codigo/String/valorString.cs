using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo String", menuName = "Valores/String")]
[System.Serializable]
public class valorString : ScriptableObject
{

    public string valorStringInicial;

    public string valorStringEjecucion;

    public void reiniciaValores()
    {
        valorStringEjecucion = valorStringInicial;
    }

}
