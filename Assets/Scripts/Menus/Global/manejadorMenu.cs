using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorMenu : MonoBehaviour
{

    [Header("Componentes graficos del menu")]
    [SerializeField] private componentesGraficos graficos;

    public componentesGraficos Graficos { get => graficos; set => graficos = value; }

    public IEnumerator cambioEscena(string escenaCarga)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}

interface ejecutaPausa
{
    public bool Pausa { get; set; }
}

interface pulsoBoton 
{
    public bool PulseBoton { get; set; }

    void reiniciaBotones();
}

interface iniciaVentanaEmergente 
{
    public GameObject NCanvasVentanaEmergente { get; set; }
    public GameObject VentanaEmergente { get; set; }
    public manejadorVentanaEmergente ManejadorVentanaEmergente { get; set; }

    public void iniciaVentanaEmergente();
}

interface reproduceAudio 
{
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get; set; }

    public void reproduceAudioClickCerrar();

    public void reproduceAudioClickAbrir();

    public void reproduceAudioAbreVentana();
}

interface conexion 
{
    public conexionWeb Conexion { get; set; }
}


