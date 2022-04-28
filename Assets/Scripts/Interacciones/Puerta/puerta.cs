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
    [Header("Variables de la puerta")]
    public tipoPuerta tipoPuerta;
    public bool estaAbierta;
    public inventario inventarioPlayer;
    private SpriteRenderer puertaSprite;
    private BoxCollider2D[] puertaColliders;

    void Start()
    {
        puertaSprite = gameObject.GetComponent<SpriteRenderer>();
        puertaColliders = gameObject.GetComponents<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (playerEnRango)
            {
                if (playerEnRango && tipoPuerta == tipoPuerta.llave)
                {
                    if (inventarioPlayer.numeroLlaves > 0)
                    {
                        inventarioPlayer.numeroLlaves--;
                        abrir();
                    }
                }
            }
        }
    }

    public void abrir() 
    {
        puertaSprite.enabled = false;
        estaAbierta = true;
        foreach (BoxCollider2D colision in puertaColliders) 
        {
            colision.enabled = false;
        }
    }

    public void cerrar() 
    {
        puertaSprite.enabled = true;
        estaAbierta = false;
        foreach (BoxCollider2D colision in puertaColliders)
        {
            colision.enabled = true;
        }
    }
}
