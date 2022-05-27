using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class espacioInventario : MonoBehaviour
{
    [Header("El texto que describe la cantidad que posee el Player de este item")]
    [SerializeField] private TextMeshProUGUI textoCantidadItem;
    [Header("La imagen que contendra el sprite del item")]
    [SerializeField] private Image imagenItem;
    [Header("El objeto(ScriptableObject) que contiene el item")]
    public inventarioItem item;
    [Header("El manejador de la interfaz grafica del inventario")]
    public manejadorInventario manejadorInventario;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickAbrir;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickAbrir;

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

    public void setUp(inventarioItem nuevoItem, manejadorInventario nuevoManejadorInventario) 
    {
        item = nuevoItem;
        manejadorInventario = nuevoManejadorInventario;
        if (item != null) 
        {
            imagenItem.sprite = item.imagenItem;
            textoCantidadItem.text = "" + item.cantidadItem;

        }
    }

    public void botonItem() 
    {
        if (item) 
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            manejadorInventario.activaBotonEnviaTexto(item.descripcionItem, item.esUsable, item);
        }
    }
}
