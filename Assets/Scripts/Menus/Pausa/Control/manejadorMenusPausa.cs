using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorMenusPausa : manejadorMenu, ejecutaPausa
{

    private bool pausa;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Componentes graficos del menu de pausa")]
    [SerializeField] private componentesGraficosMenusPausa graficosPausa;

    public bool Pausa { get => pausa; set => pausa = value; }

    void Update()
    {
        //if (Input.GetButtonDown("Pausa")) 
        //{
        //    if (verificaComponentegraficoActivo())
        //    {
        //        reproduceAudioClickCerrar();
        //        abreCierraInventario();
        //    }
        //    else 
        //    {
        //        if (verificaComponentegraficoActivo())
        //        {
        //            reproduceAudioClickCerrar();
        //            abreCierraConfiguraciones();
        //        }
        //        else 
        //        {
        //            abreCierraMenuPausa();
        //            if (verificaComponentegraficoActivo(graficosPausa.PanelPausa))
        //            {
        //                reproduceAudioClickCerrar();
        //            }
        //            else 
        //            {
        //                reproduceAudioClickAbrir();
        //            }
        //        }
        //    }
        //}
        //else 
        //{
        //    if (Input.GetButtonDown("Inventario")) 
        //    {
        //        abreCierraInventario();
        //        if (verificaComponentegraficoActivo())
        //        {
        //            reproduceAudioClickCerrar();
        //        }
        //        else
        //        {
        //            reproduceAudioClickAbrir();
        //        }
        //    }
        //}
    }

    public void abreCierraMenuPausa() 
    {
        pausa = !pausa;
        if (graficosPausa != null) 
        {
            graficosPausa.PanelPausa.SetActive(pausa);
        }
        if (pausa)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void abreCierraInventario()
    {
        pausa = !pausa;
        if (graficosPausa != null)
        {
            if (graficosPausa.PanelInventario != null) 
            {
            
            }
        }
        if (pausa)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void abreCierraConfiguraciones() 
    {
        pausa = !pausa;
        if (graficosPausa != null)
        {
            if (graficosPausa.PanelConfiguraciones != null) 
            {
                
            }
        }
        if (pausa)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private IEnumerator cargaEscena(string nombreEscena)
    {
        Time.timeScale = 1f;
        AsyncOperation accion = SceneManager.LoadSceneAsync(nombreEscena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

    public bool verificaComponentegraficoActivo(GameObject componenteGrafico)
    {
        if (componenteGrafico != null)
        {
            return componenteGrafico.activeInHierarchy;
        }
        else 
        {
            return false;
        }
    }

}
