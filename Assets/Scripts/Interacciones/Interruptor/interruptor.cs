using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interruptor : MonoBehaviour
{

    private bool activo;
    private SpriteRenderer interruptorSpriteRenderer;
    [Header("Esta activo el interruptor?")]
    public valorBooleano estadoInterruptor;
    [Header("Sprite para el interruptor pulsado")]
    public Sprite spriteInterruptorActivo;
    [Header("Objeto puerta a abrir")]
    public puerta puertaAbrir;

    void Start()
    {
        activo = estadoInterruptor.valorEjecucion;
        interruptorSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (activo) 
        {
            activaInterruptor();
        }
    }

    public void activaInterruptor() 
    {
        activo = true;
        estadoInterruptor.valorEjecucion = true;
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
