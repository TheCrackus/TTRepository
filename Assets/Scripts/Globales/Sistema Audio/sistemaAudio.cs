using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sistemaAudio : MonoBehaviour
{

    [Header("Objeto que produce un audio generico")]
    [SerializeField] private GameObject audioEmergente;

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio != null
            && (velocidad > 0 && velocidad <= 3))
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

}
