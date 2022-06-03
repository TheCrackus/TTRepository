using UnityEngine;

[System.Serializable]
public class Loot
{
    public IncrementoEstadisticas miLoot;
    public int posibilidadLoot;
}

[CreateAssetMenu]
public class TablaLoot : ScriptableObject
{
    public Loot[] loots;

    public IncrementoEstadisticas generarLootIncrementoEstadisticas() 
    {
        int probabilidadAcumulada = 0;
        int probabilidadActual = Random.Range(0, 100);
        foreach (Loot loot in loots) 
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
