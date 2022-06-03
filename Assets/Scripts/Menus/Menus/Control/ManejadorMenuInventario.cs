
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManejadorMenuInventario : ManejadorMenuGenerico, IReproductorAudioInterfazGrafica, IPausa
{

    private ComponenteGraficoMenuInventario graficos;

    private bool pausa;

    private InventarioItem itemActual;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("El inventario que contiene los items del Player")]
    [SerializeField] private ListaInventario inventarioPlayerItems;

    public bool CondicionPausa { get => pausa; set => pausa = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }

    void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproducirAudioClickCerrar();
        continuarJuego();
    }

    private void Start()
    {
        graficos = (ComponenteGraficoMenuInventario) ComponenteGrafico;
        reproducirAudioAbreVentana();
        pausarJuego();
        limpiarListaInventario();
        limpiarEspaciosInventario();
        crearEspaciosInventario();
        activarBotonEnviaTexto("", false, null);
    }

    public void activarBotonEnviaTexto(string descripcion, bool activaBoton, InventarioItem nuevoItem) 
    {
        itemActual = nuevoItem;
        if (graficos != null
            && graficos.TextoDescripcionItem != null
            && graficos.BotonUsarItem != null) 
        {
            graficos.TextoDescripcionItem.text = descripcion;
            graficos.BotonUsarItem.SetActive(activaBoton);
        }
    }

    void crearEspaciosInventario() 
    {
        if (inventarioPlayerItems != null
            && inventarioPlayerItems.inventario != null
            && inventarioPlayerItems.inventario.Count > 0) 
        {
            foreach (InventarioItem item in inventarioPlayerItems.inventario) 
            {
                if (graficos != null
                    && graficos.EspacioInventarioVacio != null
                    && graficos.ContenedorInventario != null)
                {
                    if (item.cantidadItem > 0)
                    {
                        GameObject espacioInventarioTemporal = Instantiate(graficos.EspacioInventarioVacio, graficos.ContenedorInventario.transform.position, Quaternion.identity);
                        espacioInventarioTemporal.transform.SetParent(graficos.ContenedorInventario.transform);
                        espacioInventarioTemporal.transform.localScale = new Vector3(1, 1, 1);
                        EspacioInventario nuevoEspacioInventario = espacioInventarioTemporal.GetComponent<EspacioInventario>();
                        nuevoEspacioInventario.iniciarEspacioInventario(item, this);
                    }
                }
            }
        }
    }

    public void limpiarEspaciosInventario() 
    {
        if (graficos != null
            && graficos.ContenedorInventario != null) 
        {
            for (int i = 0; i < graficos.ContenedorInventario.transform.childCount; i++)
            {
                Destroy(graficos.ContenedorInventario.transform.GetChild(i).gameObject);
            }
        }
    }

    void limpiarListaInventario()
    {
        if (inventarioPlayerItems != null
            && inventarioPlayerItems.inventario != null
            && inventarioPlayerItems.inventario.Count > 0)
        {
            foreach (InventarioItem item in inventarioPlayerItems.inventario.ToArray())
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

    public void usarBoton() 
    {
        if (itemActual != null) 
        {
            reproducirAudioClickAbrir();
            itemActual.invocarEventoUsaItem();
            limpiarListaInventario();
            limpiarEspaciosInventario();
            crearEspaciosInventario();
            if (itemActual.cantidadItem <= 0) 
            {
                activarBotonEnviaTexto("", false, null);
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
            manejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioAbrirVentana();
        }
    }   

}
