using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionEquipable : MonoBehaviour
{

    [Header("El inventario general del Player")]
    [SerializeField] private ListaInventario inventariopPlayerItems;
    [Header("Objeto que se equipara")]
    [SerializeField] private InventarioItem objetoEquipar;

    public void equipar() 
    {
        if (inventariopPlayerItems && objetoEquipar) 
        {
            inventariopPlayerItems.objetoEquipado = objetoEquipar;
        }
    }

}
