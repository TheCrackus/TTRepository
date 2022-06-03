using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaAudio : MonoBehaviour
{

    [Header("Objeto que produce un audio generico")]
    [SerializeField] private GameObject audioEmergente;

    public void reproducirAudio(AudioSource audio, float velocidad)
    {
        if (audio != null
            && (velocidad > 0 && velocidad <= 3))
        {
            AudioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<AudioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproducirAudio();
        }
    }

}
