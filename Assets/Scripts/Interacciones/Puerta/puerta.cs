using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tipoPuerta 
{
    llave,
    enemigo,
    boton
}

public class puerta : interactuador
{
    [Header("El manejador de sprites de este objeto")]
    [SerializeField] private SpriteRenderer puertaSpriteRenderer;
    [Header("Las colisiones de esta puerta")]
    [SerializeField] private BoxCollider2D[] puertaColliders;
    [Header("Que tipo de puerta soy?")]
    [SerializeField] private tipoPuerta tipoPuerta;
    [Header("Estoy abierta?")]
    [SerializeField] valorBooleano estaAbierta;
    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("Objeto que representa la llave corta")]
    [SerializeField] private inventarioItem llaveCorta;
    

    void Start()
    {
        puertaSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        puertaColliders = gameObject.GetComponents<BoxCollider2D>();
        if (estaAbierta != null) 
        {
            if (estaAbierta.valorBooleanoEjecucion) 
            {
                abrir();
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (playerEnRango && tipoPuerta == tipoPuerta.llave)
            {
                if (inventariopPlayerItems && llaveCorta)
                {
                    if (inventariopPlayerItems.verififcaItem(llaveCorta))
                    {
                        llaveCorta.invocaEventoUsaItem();
                        abrir();
                    }
                }
            }
        }
    }

    public void abrir() 
    {
        if (puertaColliders != null && estaAbierta != null && puertaSpriteRenderer != null) 
        {
            puertaSpriteRenderer.enabled = false;
            estaAbierta.valorBooleanoEjecucion = true;
            foreach (BoxCollider2D colision in puertaColliders)
            {
                colision.enabled = false;
            }
        }
        
    }

    public void cerrar() 
    {
        if (puertaColliders != null && estaAbierta != null && puertaSpriteRenderer != null)
        {
            puertaSpriteRenderer.enabled = true;
            estaAbierta.valorBooleanoEjecucion = false;
            foreach (BoxCollider2D colision in puertaColliders)
            {
                colision.enabled = true;
            }
        }
    }
}
