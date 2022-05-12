
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class manejadorBotonesInventario : MonoBehaviour
{

    [Header("El inventario que contiene los items del Player")]
    public listaInventario inventariopPlayerItems;
    [Header("El espacio fisico en la interfaz que muestra un item en el inventario")]
    [SerializeField] private GameObject espacioInventarioVacio;
    [Header("El espacio fisico en la interfaz que contiene el inventario")]
    [SerializeField] private GameObject contenedorInventario;
    [Header("Texto de la interfaz que muestra la descripcion de un item en el inventario")]
    [SerializeField] private TextMeshProUGUI textoDescripcionItem;
    [Header("Boton de la interfaz que permite usar un item")]
    [SerializeField] private GameObject botonUsarItem;
    [Header("El item que actualmente se muestra en la interfaz")]
    [SerializeField] private inventarioItem itemActual;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickAbrir;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickAbrir;

    void OnEnable()
    {
        limpiaListaInventario();
        limpiaEspaciosInventario();
        creaEspaciosInventario();
        activaBotonEnviaTexto("", false, null);
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

    public void activaBotonEnviaTexto(string descripcion, bool activaBoton, inventarioItem nuevoItem) 
    {
        itemActual = nuevoItem;
        textoDescripcionItem.text = descripcion;
        botonUsarItem.SetActive(activaBoton);

    }

    void creaEspaciosInventario() 
    {
        if (inventariopPlayerItems != null) 
        {
            foreach(inventarioItem item in inventariopPlayerItems.inventario) 
            {
                if (espacioInventarioVacio != null) 
                {
                    if (item.cantidadItem > 0)
                    {
                        GameObject espacioInventarioTemporal = Instantiate(espacioInventarioVacio, contenedorInventario.transform.position, Quaternion.identity);
                        espacioInventarioTemporal.transform.SetParent(contenedorInventario.transform);
                        espacioInventarioTemporal.transform.localScale = new Vector3(1, 1, 1);
                        espacioInventario nuevoEspacioInventario = espacioInventarioTemporal.GetComponent<espacioInventario>();
                        nuevoEspacioInventario.setUp(item, this);
                    }
                }
            }
        }
    }

    public void limpiaEspaciosInventario() 
    {
        for (int i = 0; i < contenedorInventario.transform.childCount; i++) 
        {
            Destroy(contenedorInventario.transform.GetChild(i).gameObject);
        }
    }

    void limpiaListaInventario()
    {
        if (inventariopPlayerItems != null)
        {
            foreach (inventarioItem item in inventariopPlayerItems.inventario.ToArray())
            {
                if (item.cantidadItem <= 0)
                {
                    inventariopPlayerItems.inventario.Remove(item);
                }
            }
        }
    }

    public void botonUsa() 
    {
        if (itemActual) 
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            itemActual.invocaEventoUsaItem();
            limpiaListaInventario();
            limpiaEspaciosInventario();
            creaEspaciosInventario();
            if (itemActual.cantidadItem <= 0) 
            {
                activaBotonEnviaTexto("", false, null);
            }
        }
    }
}
