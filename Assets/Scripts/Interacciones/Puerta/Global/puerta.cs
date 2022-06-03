using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : Interactuador
{

    private SpriteRenderer puertaSpriteRenderer;

    private BoxCollider2D[] puertaColliders;

    [Header("Estoy abierta?")]
    [SerializeField] ValorBooleano estaAbierta;

    [Header("El inventario general del Player")]
    [SerializeField] private ListaInventario inventarioPlayerItems;

    [Header("Manejador de audio de la puesta")]
    [SerializeField] private audioPuerta manejadorAudioPuerta;

    [Header("Manejador de audio de los dialogos")]
    [SerializeField] private AudioDialogos manejadorAudioDialogos;

    public ListaInventario InventarioPlayerItems { get => inventarioPlayerItems; set => inventarioPlayerItems = value; }
    public SpriteRenderer PuertaSpriteRenderer { get => puertaSpriteRenderer; set => puertaSpriteRenderer = value; }
    public BoxCollider2D[] PuertaColliders { get => puertaColliders; set => puertaColliders = value; }
    public ValorBooleano EstaAbierta { get => estaAbierta; set => estaAbierta = value; }
    public audioPuerta ManejadorAudioPuerta { get => manejadorAudioPuerta; set => manejadorAudioPuerta = value; }
    public AudioDialogos ManejadorAudioDialogos { get => manejadorAudioDialogos; set => manejadorAudioDialogos = value; }

    public virtual void Awake() 
    {
        puertaSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        puertaColliders = gameObject.GetComponents<BoxCollider2D>();
    }

    public virtual void Start()
    {
        
        if (estaAbierta != null) 
        {
            if (estaAbierta.valorBooleanoEjecucion) 
            {
                abrir();
            }
        }
    }

    public virtual void abrir() 
    {
        if (puertaColliders != null && puertaSpriteRenderer != null)
        {
            if (estaAbierta != null)
            {
                if (!estaAbierta.valorBooleanoEjecucion)
                {
                    manejadorAudioPuerta.reproduceAudioAbrirPuerta();
                }
            }
            puertaSpriteRenderer.enabled = false;
            if (estaAbierta != null)
            {
                estaAbierta.valorBooleanoEjecucion = true;
            }
            foreach (BoxCollider2D colision in puertaColliders)
            {
                colision.enabled = false;
            }
        }
    }

    public virtual void cerrar() 
    {
        if (puertaColliders != null && puertaSpriteRenderer != null)
        {
            if (estaAbierta != null)
            {
                if (!estaAbierta.valorBooleanoEjecucion)
                {
                    manejadorAudioPuerta.reproduceAudioCerrarPuerta();
                }
            }
            puertaSpriteRenderer.enabled = true;
            if (estaAbierta != null) 
            {
                estaAbierta.valorBooleanoEjecucion = false;
            }
            foreach (BoxCollider2D colision in puertaColliders)
            {
                colision.enabled = true;
            }
        }
    }
}
