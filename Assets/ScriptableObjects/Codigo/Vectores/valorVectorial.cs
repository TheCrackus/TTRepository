using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Vector", menuName = "Valores/Vectorial")]
[System.Serializable]
public class ValorVectorial : ScriptableObject
{
    [Header("Valor inicial del vector")]
    public Vector3 valorVectorialInicial;
    [Header("Valor en ejecucion del juego del vector")]
    public Vector3 valorVectorialEjecucion;

    public void reiniciarValores() 
    {
        valorVectorialEjecucion = valorVectorialInicial;
    }

}
