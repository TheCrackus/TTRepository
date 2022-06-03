using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Nuevo Inventario", menuName = "Inventario/listaItem")]
public class ListaInventario : ScriptableObject
{

    [Header("La lista de objetos tipo item")]
    public List<InventarioItem> inventario = new List<InventarioItem>();
    [Header("Objeto equipado por el player para atacar")]
    public InventarioItem objetoEquipado;

    public void reiniciarValores() 
    {
        inventario.Clear();
        objetoEquipado = null;
    }

    public bool verififcarItem(InventarioItem item) 
    {
        bool verificacion = false;
        foreach (InventarioItem itemLoop in inventario) 
        {
            if (itemLoop == item && itemLoop.cantidadItem > 0)
            {
                verificacion = true;
                break;
            }
            else 
            {
                verificacion = false;
            }
        }
        return verificacion;
    }

}
