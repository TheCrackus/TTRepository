using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interruptor : MonoBehaviour
{

    [Header("El manejador de sprites de este objeto")]
    [SerializeField] private SpriteRenderer interruptorSpriteRenderer;
    [Header("Esta activo el interruptor?")]
    [SerializeField] private valorBooleano estadoInterruptor;
    [Header("Sprite para el interruptor pulsado")]
    [SerializeField] private Sprite spriteInterruptorActivo;
    [Header("Objeto puerta a abrir")]
    [SerializeField] private puerta puertaAbrir;

    void Start()
    {
        interruptorSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (estadoInterruptor.valorBooleanoEjecucion) 
        {
            activaInterruptor();
        }
    }

    public void activaInterruptor() 
    {
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
