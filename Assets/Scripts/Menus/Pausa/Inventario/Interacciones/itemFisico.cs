using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFisico : MonoBehaviour
{

    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("El item a agregar al inventario")]
    [SerializeField] private inventarioItem itemAgrgarInventario;

    void agregaItemInventario() 
    {
        if (inventariopPlayerItems && itemAgrgarInventario) 
        {
            if (inventariopPlayerItems.inventario.Contains(itemAgrgarInventario))
            {
                itemAgrgarInventario.cantidadItem += 1;
            }
            else 
            {
                inventariopPlayerItems.inventario.Add(itemAgrgarInventario);
                itemAgrgarInventario.cantidadItem += 1;
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            agregaItemInventario();
            Destroy(gameObject);
        }
    }
}
