using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Nuevo Inventario", menuName = "Inventario/listaItem")]
public class listaInventario : ScriptableObject
{

    [Header("La lista de objetos por default del Player")]
    public listaInventarioDefault inventarioDefault;
    [Header("La lista de objetos tipo item")]
    public List<inventarioItem> inventario = new List<inventarioItem>();

    public void reiniciaValores() 
    {
        inventario.Clear();
        foreach (inventarioItem item in inventarioDefault.inventario) 
        {
            inventario.Add(item);
            item.cantidadItem = 1;
        }
    }

    public bool verififcaItem(inventarioItem item) 
    {
        bool verificacion = false;
        foreach (inventarioItem itemLoop in inventario) 
        {
            if (itemLoop == item && itemLoop.cantidadItem > 0)
            {
                verificacion = true;
            }
            else 
            {
                verificacion = false;
            }
        }
        return verificacion;
    }

}
