using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDialogos : SistemaAudio
{

    [Header("Audio al abrir el dialogo")]
    [SerializeField] private AudioSource audioAbrirDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirDialogo;
    [Header("Audio al cerrar el dialogo")]
    [SerializeField] private AudioSource audioCerrarDialogo;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioCerrarDialogo;

    public void reproducirAudioAbreDialogo() 
    {
        reproducirAudio(audioAbrirDialogo, velocidadAudioAbrirDialogo);
    }

    public void reproducirAudioCierraDialogo() 
    {
        reproducirAudio(audioCerrarDialogo, velocidadAudioCerrarDialogo);
    }

}
