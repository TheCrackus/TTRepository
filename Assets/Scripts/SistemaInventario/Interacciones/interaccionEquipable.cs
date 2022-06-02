using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionEquipable : MonoBehaviour
{

    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("Objeto que se equipara")]
    [SerializeField] private inventarioItem objetoEquipar;

    public void equipa() 
    {
        if (inventariopPlayerItems && objetoEquipar) 
        {
            inventariopPlayerItems.objetoEquipado = objetoEquipar;
        }
    }

}
