using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Booleano", menuName = "Valores/Booleano")]
[System.Serializable]
public class ValorBooleano : ScriptableObject
{
    public bool valorBooleanoInicial;

    public bool valorBooleanoEjecucion;

    public void reiniciarValores()
    {
        valorBooleanoEjecucion = valorBooleanoInicial;
    }
}
