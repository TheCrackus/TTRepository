using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorResolucion : manejadorConfiguracion
{

    private Resolution[] resoluciones;

    void Start()
    {
        cargaResoluciones();
    }

    public void enviaResolucion(int resolucionIndex) 
    {
        Resolution resolucion = resoluciones[resolucionIndex];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void cargaResoluciones() 
    {
        if (GraficosConfiguracion != null)
        {
            resoluciones = Screen.resolutions;
            GraficosConfiguracion.DropdownResoluciones.ClearOptions();
            List<string> opciones = new List<string>();
            Resolution resolucionActual = Screen.currentResolution;
            string textoResolucionActual = resolucionActual.width + " x " + resolucionActual.height;
            int indexResolucionActual = 0;
            int index = 0;
            foreach (Resolution resolucion in resoluciones)
            {
                string opcion = resolucion.width + " x " + resolucion.height;
                opciones.Add(opcion);
                if (textoResolucionActual == opcion)
                {
                    indexResolucionActual = index;
                }
                index++;
            }
            GraficosConfiguracion.DropdownResoluciones.AddOptions(opciones);
            GraficosConfiguracion.DropdownResoluciones.value = indexResolucionActual;
            GraficosConfiguracion.DropdownResoluciones.RefreshShownValue();
        }
    }

}
