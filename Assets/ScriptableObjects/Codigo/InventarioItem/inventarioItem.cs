using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class InventarioItem : ScriptableObject
{
    [Header("El nombre del objeto en el inventario")]
    public string nombreItem;
    [Header("La descripcion de este objeto en el inventario")]
    public string descripcionItem;
    [Header("La imagen que mostrara este objeto en el inventario")]
    public Sprite imagenItem;
    [Header("La cantidad de este objeto en el inventario")]
    public int cantidadItem;
    [Header("Este objeto se puede usar?")]
    public bool esUsable;
    [Header("Este objeto es unico?")]
    public bool esUnico;
    [Header("Estoy mostrando este objeto?")]
    public bool mostrarItem;
    [Header("Evento que permite utilizar un item")]
    public UnityEvent eventoUsaItem;

    public void invocarEventoUsaItem() 
    {
        eventoUsaItem.Invoke();
    }

    public void disminuirCantidadItem(int decremento) 
    {
        cantidadItem -= decremento;
        if (cantidadItem <= 0)
        {
            cantidadItem = 0;
        }
    }

    public void reiniciarValores()
    {
        cantidadItem = 0;
    }
}
