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
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al pulsar interruptor")]
    [SerializeField] private AudioSource audioPulsaInterruptor;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioPulsaInterruptor;

    void Start()
    {
        interruptorSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (estadoInterruptor.valorBooleanoEjecucion) 
        {
            activaInterruptor();
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

    public void activaInterruptor() 
    {
        reproduceAudio(audioPulsaInterruptor, velocidadAudioPulsaInterruptor);
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
