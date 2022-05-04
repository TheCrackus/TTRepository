using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class valorVectorial : ScriptableObject
{
    [Header("Valor inicial del vector")]
    public Vector3 valorVectorialInicial;
    [Header("Valor en ejecucion del juego del vector")]
    public Vector3 valorVectorialEjecucion;

    public void reiniciaValores() 
    {
        valorVectorialEjecucion = valorVectorialInicial;
    }

}
