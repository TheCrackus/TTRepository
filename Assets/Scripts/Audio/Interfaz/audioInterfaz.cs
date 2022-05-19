using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioInterfaz : sistemaAudio
{

    [Header("Audio al cerrar una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickCerrar;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickCerrar;

    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickAbrir;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickAbrir;

    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioAbrirVentana;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAbrirVentana;

    public void reproduceAudioClickCerrar() 
    {
        reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar);
    }

    public void reproduceAudioClickAbrir()
    {
        reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
    }

    public void reproduceAudioAbrirVentana() 
    {
        reproduceAudio(audioAbrirVentana, velocidadAudioAbrirVentana);
    }

}
