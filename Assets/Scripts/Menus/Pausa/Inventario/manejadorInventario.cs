
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class manejadorInventario : ManejadorMenuGenerico, IReproductorAudio, IPausa
{

    private bool pausa;

    private inventarioItem itemActual;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("El inventario que contiene los items del Player")]
    [SerializeField] private listaInventario inventarioPlayerItems;

    [Header("Componentes graficos del menu de inventario")]
    [SerializeField] private ComponenteGraficoMenuInventario graficosInventario;

    public bool CondicionPausa { get => pausa; set => pausa = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }

    void OnEnable()
    {
        reproducirAudioAbreVentana();
        pausarJuego();
        limpiaListaInventario();
        limpiaEspaciosInventario();
        creaEspaciosInventario();
        activaBotonEnviaTexto("", false, null);
    }

    void OnDisable()
    {
        if (!gameObject.scene.isLoaded) 
        {
            return;
        } 
        reproducirAudioClickCerrar();
        continuarJuego();
    }

    public void activaBotonEnviaTexto(string descripcion, bool activaBoton, inventarioItem nuevoItem) 
    {
        itemActual = nuevoItem;
        if (graficosInventario != null
            && graficosInventario.TextoDescripcionItem != null
            && graficosInventario.BotonUsarItem != null) 
        {
            graficosInventario.TextoDescripcionItem.text = descripcion;
            graficosInventario.BotonUsarItem.SetActive(activaBoton);
        }
    }

    void creaEspaciosInventario() 
    {
        if (inventarioPlayerItems != null
            && inventarioPlayerItems.inventario != null
            && inventarioPlayerItems.inventario.Count > 0) 
        {
            foreach(inventarioItem item in inventarioPlayerItems.inventario) 
            {
                if (graficosInventario != null
                    && graficosInventario.EspacioInventarioVacio != null
                    && graficosInventario.ContenedorInventario != null)
                {
                    if (item.cantidadItem > 0)
                    {
                        GameObject espacioInventarioTemporal = Instantiate(graficosInventario.EspacioInventarioVacio, graficosInventario.ContenedorInventario.transform.position, Quaternion.identity);
                        espacioInventarioTemporal.transform.SetParent(graficosInventario.ContenedorInventario.transform);
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
        if (graficosInventario != null
            && graficosInventario.ContenedorInventario != null) 
        {
            for (int i = 0; i < graficosInventario.ContenedorInventario.transform.childCount; i++)
            {
                Destroy(graficosInventario.ContenedorInventario.transform.GetChild(i).gameObject);
            }
        }
    }

    void limpiaListaInventario()
    {
        if (inventarioPlayerItems != null
            && inventarioPlayerItems.inventario != null
            && inventarioPlayerItems.inventario.Count > 0)
        {
            foreach (inventarioItem item in inventarioPlayerItems.inventario.ToArray())
            {
                if (item != null) 
                {
                    if (item.cantidadItem <= 0)
                    {
                        inventarioPlayerItems.inventario.Remove(item);
                    }
                }
            }
        }
    }

    public void botonUsa() 
    {
        if (itemActual != null) 
        {
            reproducirAudioClickAbrir();
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

    public void pausarJuego()
    {
        Time.timeScale = 0f;
    }

    public void continuarJuego()
    {
        Time.timeScale = 1f;
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        }
    }   

}
