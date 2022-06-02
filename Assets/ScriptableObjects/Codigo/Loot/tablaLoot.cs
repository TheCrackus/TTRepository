using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class loot 
{
    public incrementoEstadisticas miLoot;
    public int posibilidadLoot;
}

[CreateAssetMenu]
public class tablaLoot : ScriptableObject
{
    public loot[] loots;

    public incrementoEstadisticas lootIncrementoEstadisticas() 
    {
        int probabilidadAcumulada = 0;
        int probabilidadActual = Random.Range(0, 100);
        foreach (loot loot in loots) 
        {
            probabilidadAcumulada += loot.posibilidadLoot;
            if (probabilidadActual <= probabilidadAcumulada) 
            {
                return loot.miLoot;
            }
        }
        return null;
    }
}
