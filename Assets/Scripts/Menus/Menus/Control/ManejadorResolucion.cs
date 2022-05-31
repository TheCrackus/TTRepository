using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManejadorResolucion : MonoBehaviour
{

    private Resolution[] resoluciones;

    [Header("Menu desplegable que contiene las configuraciones")]
    [SerializeField] private TMP_Dropdown dropdownResoluciones;

    void Start()
    {
        resoluciones = Screen.resolutions;
        cargaResoluciones();
    }

    public void enviaResolucion(int resolucionIndex) 
    {
        Resolution resolucion = resoluciones[resolucionIndex];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void cargaResoluciones() 
    {
        dropdownResoluciones.ClearOptions();
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
        dropdownResoluciones.AddOptions(opciones);
        dropdownResoluciones.value = indexResolucionActual;
        dropdownResoluciones.RefreshShownValue();
    }

}
