using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interruptor : MonoBehaviour
{

    private SpriteRenderer interruptorSpriteRenderer;

    [Header("Esta activo el interruptor?")]
    [SerializeField] private valorBooleano estadoInterruptor;

    [Header("Sprite para el interruptor pulsado")]
    [SerializeField] private Sprite spriteInterruptorActivo;

    [Header("Objeto puerta a abrir")]
    [SerializeField] private puertaInterruptor puertaAbrir;

    [Header("Manejador audio de activacion")]
    [SerializeField] private audioActivacion manejadorAudioActivacion;

    void Awake()
    {
        interruptorSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (estadoInterruptor.valorBooleanoEjecucion) 
        {
            activaInterruptor();
        }
    }

    public void activaInterruptor() 
    {
        if (!estadoInterruptor.valorBooleanoEjecucion) 
        {
            manejadorAudioActivacion.reproduceAudioActivacion();
        }
        estadoInterruptor.valorBooleanoEjecucion = true;
        puertaAbrir.abrir();
        interruptorSpriteRenderer.sprite = spriteInterruptorActivo;
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")) 
        {
            activaInterruptor();
        }
    }
}
