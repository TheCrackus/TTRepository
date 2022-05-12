using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controladorDialogos : interactuador
{
    [Header("Objeto contenedor del Texto a mostrar")]
    [SerializeField] private GameObject objetoContenedorTextoDialogos;
    [Header("Objeto texto para mostrar los dialogos")]
    [SerializeField] private TextMeshProUGUI textoDialogos;
    [Header("Texto a mostrar")]
    [SerializeField] private string dialogo;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al abrir el dialogo")]
    [SerializeField] private AudioSource audioAbrirDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirDialogo;
    [Header("Audio al cerrar el dialogo")]
    [SerializeField] private AudioSource audioCerrarDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioCerrarDialogo;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Interactuar") && getPlayerEnRango()) 
        {
            if (objetoContenedorTextoDialogos.activeInHierarchy)
            {
                objetoContenedorTextoDialogos.SetActive(false);
                reproduceAudio(audioCerrarDialogo, velocidadAudioCerrarDialogo);
            }
            else 
            {
                textoDialogos.text = dialogo;
                objetoContenedorTextoDialogos.SetActive(true);
                reproduceAudio(audioAbrirDialogo, velocidadAudioAbrirDialogo);
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

    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            getSimboloActivoDesactivo().invocaFunciones();
            setPlayerEnRango(false);
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
        }
    }
}
