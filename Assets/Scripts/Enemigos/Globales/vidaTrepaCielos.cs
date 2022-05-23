using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaTrepaCielos : sistemaVida
{
    [Header("Evento para cuartos con enemigos (abre puerta)")]
    [SerializeField] private evento estadoEnemigosCuarto;

    [Header("Audio al recibir daņo")]
    [SerializeField] private AudioSource audioDaņo;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioDaņo;

    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;

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

    public override void quitaVida(float vidaMenos)
    {
        reproduceAudio(audioDaņo, velocidadAudioDaņo);
        base.quitaVida(vidaMenos);
        if (getVidaActual() <= 0) 
        {
            getObjeto().SetActive(false);
            muerteAnimacion();
            procesaLoot();
            if (estadoEnemigosCuarto)
            {
                estadoEnemigosCuarto.invocaFunciones();
            }
        }
    }
    
}
