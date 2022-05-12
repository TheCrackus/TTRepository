using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incrementoEstadisticas : MonoBehaviour
{
    [Header("Evento que incrementa una estadistica del player")]
    [SerializeField] private evento eventoIncrementoEstadistica;
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al recojer item")]
    [SerializeField] private AudioSource audioRecojer;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRecojer;


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

    public void setEventoIncrementoEstadistica(evento eventoIncrementoEstadistica) 
    {
        this.eventoIncrementoEstadistica = eventoIncrementoEstadistica;
    }

    public evento getEventoIncrementoEstadistica() 
    {
        return eventoIncrementoEstadistica;
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada) 
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            reproduceAudio(audioRecojer, velocidadAudioRecojer);
        }
    }
}
