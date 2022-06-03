using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorFormularioCuestionario : ManejadorFormulario, ICanvasMenuPrincipal, IPausa
{

    private string nombreEscenaActual;

    private ComponenteGraficoFormularioCuestionario graficos;

    private bool condicionPausa;

    [Header("Nombre de la escena del menu principal")]
    [SerializeField] private ValorString nombreEscenaPrincipal;

    [Header("Evento a ejecutar cuando este en el juego")]
    [SerializeField] private Evento eventoFinalCuestionario;

    public bool CondicionPausa { get => condicionPausa; set => condicionPausa = value; }

    private void OnEnable()
    {
        reproducirAudioAbreVentana();
        reiniciarBotones();
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
        {
            pausarJuego();
        }
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproducirAudioClickCerrar();
        if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
        {
            continuarJuego();
        }
    }

    public void finalizarBoton()
    {
        if (!PulseBoton)
        {
            if (graficos.FieldRespuesta1.text.ToString() != ""
                && graficos.FieldRespuesta2.text.ToString() != ""
                && graficos.FieldRespuesta3.text.ToString() != ""
                && graficos.FieldRespuesta4.text.ToString() != ""
                && graficos.FieldRespuesta5.text.ToString() != ""
                && graficos.FieldRespuesta6.text.ToString() != "")
            {
                Conexion.enviarPrueba(graficos.FieldRespuesta1.text.ToString(),
                    graficos.FieldRespuesta2.text.ToString(),
                    graficos.FieldRespuesta3.text.ToString(),
                    graficos.FieldRespuesta4.text.ToString(),
                    graficos.FieldRespuesta5.text.ToString(),
                    graficos.FieldRespuesta6.text.ToString());
                StartCoroutine(esperarDatosEnvioPrueba());
                bloquearBotones();
            }
            else 
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("¡Debes completar todas las preguntas para que podamos medir tu creatividad!");
            }
        }
    }

    public void cierraCuestionario() 
    {
        if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
        {
            reproducirAudioClickCerrar();
            eventoFinalCuestionario.invocarFunciones();
            cerrarGrafico();
        }
        else
        {
            reproducirAudioClickCerrar();
            iniciarCanvasMenuPrincipal();
            cerrarGrafico();
        }
    }

    private void Start()
    {
        graficos = (ComponenteGraficoFormularioCuestionario) ComponenteGrafico;
    }

    private IEnumerator esperarDatosEnvioPrueba()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == EstadoConexion.iniciandoEnvioPrueba));
        if (Conexion.EstadoActualConexion == EstadoConexion.termineEnvioPrueba)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("¡La prueba se envió correctamente a tu docente a cargo, muchas gracias por tu participación!");
            if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
            {
                continuarJuego();
                yield return new WaitForSeconds(3f);
            }
            else 
            {
                yield return new WaitForSeconds(1f);
            }
            Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            cierraCuestionario();
        }
        else
        {
            if (Conexion.EstadoActualConexion == EstadoConexion.falleEnvioPruebaConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión, comprueba el estado de tu red de internet.");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == EstadoConexion.falleEnvioPruebaeDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("La prueba no pude ser enviada, por favor, inténtelo más tarde.");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = EstadoConexion.ninguno;
                }
            }
        }
        reiniciarBotones();
    }

    public void iniciarCanvasMenuPrincipal()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(graficos.CanvasMenuPrincipal, Vector3.zero, Quaternion.identity);
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

}
