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
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al abrir puerta")]
    [SerializeField] private AudioSource audioAbrirPuerta;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirPuerta;
    [Header("Audio al cerrar puerta")]
    [SerializeField] private AudioSource audioCerrarPuerta;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioCerrarPuerta;
    [Header("Audio al romper puerta")]
    [SerializeField] private AudioSource audioRomperPuerta;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomperPuerta;


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
            if (getPlayerEnRango() && tipoPuerta == tipoPuerta.llave)
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

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

    public void abrir() 
    {
        if (puertaColliders != null && puertaSpriteRenderer != null)
        {
            if (tipoPuerta == tipoPuerta.llave
                || tipoPuerta == tipoPuerta.boton)
            {
                reproduceAudio(audioAbrirPuerta, velocidadAudioAbrirPuerta);
            }
            else 
            {
                if (tipoPuerta == tipoPuerta.enemigo) 
                {
                    reproduceAudio(audioRomperPuerta, velocidadAudioRomperPuerta);
                }
            }
            puertaSpriteRenderer.enabled = false;
            if(estaAbierta != null) 
            {
                estaAbierta.valorBooleanoEjecucion = true;
            }
            foreach (BoxCollider2D colision in puertaColliders)
            {
                colision.enabled = false;
            }
        } 
    }

    public void cerrar() 
    {
        if (puertaColliders != null && puertaSpriteRenderer != null)
        {
            if (tipoPuerta == tipoPuerta.llave
                || tipoPuerta == tipoPuerta.boton)
            {
                reproduceAudio(audioCerrarPuerta, velocidadAudioCerrarPuerta);
            }
            else
            {
                if (tipoPuerta == tipoPuerta.enemigo)
                {
                    reproduceAudio(audioRomperPuerta, velocidadAudioRomperPuerta);
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
