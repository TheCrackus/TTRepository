using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioDialogos : sistemaAudio
{

    [Header("Audio al abrir el dialogo")]
    [SerializeField] private AudioSource audioAbrirDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirDialogo;
    [Header("Audio al cerrar el dialogo")]
    [SerializeField] private AudioSource audioCerrarDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioCerrarDialogo;

    public void reproduceAudioAbreDialogo() 
    {
        reproduceAudio(audioAbrirDialogo, velocidadAudioAbrirDialogo);
    }

    public void reproduceAudioCierraDialogo() 
    {
        reproduceAudio(audioCerrarDialogo, velocidadAudioCerrarDialogo);
    }

}
