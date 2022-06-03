using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFisico : MonoBehaviour
{
    [Header("El inventario general del Player")]
    [SerializeField] private ListaInventario inventariopPlayerItems;

    [Header("El item a agregar al inventario")]
    [SerializeField] private InventarioItem itemAgrgarInventario;

    [Header("Manejador de audio del objeto")]
    [SerializeField] private AudioObjetoMapa manejadorAudioObjetoMapa;

    public AudioObjetoMapa ManejadorAudioObjetoMapa { get => manejadorAudioObjetoMapa; set => manejadorAudioObjetoMapa = value; }

    void agregarItemInventario() 
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
            manejadorAudioObjetoMapa.reproducirAudioRecojer();
            agregarItemInventario();
            Destroy(gameObject);
        }
    }
}
