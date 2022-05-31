using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ManejadorMixer : MonoBehaviour
{

    [Header("Mixer a controlar")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Nombre del volumen a modificar")]
    [SerializeField] private string nombreMasterVolume;

    [Header("Barra de volumen")]
    [SerializeField] private Slider barraVolumen;

    public void Start()
    {
        float valor;
        bool resultado = masterMixer.GetFloat(nombreMasterVolume, out valor);
        if (resultado) 
        {
            valor = Mathf.Pow(10.0f, valor / 20.0f);
            barraVolumen.value = valor;
        }
    }

    public void enviaVolumen(float volumen) 
    {
        masterMixer.SetFloat(nombreMasterVolume, Mathf.Log10(volumen) * 20);
    }

}
