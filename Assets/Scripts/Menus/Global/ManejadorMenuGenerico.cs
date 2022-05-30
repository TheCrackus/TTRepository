using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuGenerico : MonoBehaviour
{

    [Header("Componentes graficos del menu")]
    [SerializeField] private ComponenteGraficoGenerico componenteGrafico;

    public ComponenteGraficoGenerico ComponenteGrafico { get => componenteGrafico; set => componenteGrafico = value; }

    public void cerrarGrafico()
    {
        if (ComponenteGrafico.ComponenteGraficoPrincipal != null)
        {
            Destroy(ComponenteGrafico.ComponenteGraficoPrincipal);
        }
    }
    public IEnumerator cambiarEscena(string escenaCarga)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}

interface IPausa
{
    public bool CondicionPausa { get; set; }

    public void pausarJuego();

    public void continuarJuego();
}

interface IBotonPulso 
{
    public bool PulseBoton { get; set; }

    public void reiniciarBotones();

    public void bloquearBotones();

}

interface ICanvasVentanaEmergente
{
    public GameObject NuevoCanvasVentanaEmergente { get; set; }
    public GameObject VentanaEmergente { get; set; }
    public ManejadorVentanaEmergente ManejadorVentanaEmergente { get; set; }

    public void iniciarVentanaEmergente();
}

interface IReproductorAudio 
{
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get; set; }

    public void reproducirAudioClickCerrar();

    public void reproducirAudioClickAbrir();

    public void reproducirAudioAbreVentana();
}

interface IConexion 
{
    public conexionWeb Conexion { get; set; }
}

interface ICanvasFormularioRegistro
{
    public void iniciarCanvasRegistrarUsuario();
}

interface ICanvasFormularioLogIn 
{
    public void iniciarCanvasLogIn();
}

interface ICanvasMenuPrincipal 
{
    public void iniciarCanvasMenuPrincipal();
}

interface ICanvasFormularioEliminarUsuario 
{
    public void iniciarCanvasFormularioEliminarUsuario();
}

interface ICanvasFormularioModificarUsuario
{
    public void iniciarCanvasFormularioModificarUsuario();
}

interface ICanvasMenuInventario
{
    public void iniciarCanvasMenuInventario();
}

interface ICanvasMenuConfiguraciones
{
    public void iniciarCanvasMenuConfiguraciones();
}