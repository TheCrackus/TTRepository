using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorResolucion : ManejadorMenuGenerico
{

    private ComponenteGraficoMenuConfiguracion graficos;

    private Resolution[] resoluciones;

    void Start()
    {
        graficos = (ComponenteGraficoMenuConfiguracion) ComponenteGrafico;
        cargaResoluciones();
    }

    public void enviaResolucion(int resolucionIndex) 
    {
        Resolution resolucion = resoluciones[resolucionIndex];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void cargaResoluciones() 
    {
        if (graficos != null)
        {
            resoluciones = Screen.resolutions;
            graficos.DropdownResoluciones.ClearOptions();
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
            graficos.DropdownResoluciones.AddOptions(opciones);
            graficos.DropdownResoluciones.value = indexResolucionActual;
            graficos.DropdownResoluciones.RefreshShownValue();
        }
    }

}
