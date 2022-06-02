using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorFormularioRecuperaContraseña : ManejadorFormulario, ICanvasFormularioLogIn
{

    private ComponenteGraficoFormularioRecuperaContraseña graficos;

    private void Start()
    {
        graficos = (ComponenteGraficoFormularioRecuperaContraseña) ComponenteGrafico;
    }

    public void recuperarContraseñaBoton() 
    {
        if (!PulseBoton) 
        {
            Conexion.recuperarContraseña(graficos.EmailField.text.ToString());
            StartCoroutine(esperarDatosRecuperaContraseña());
            bloquearBotones();
        }
    }

    public void regresarBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            iniciarCanvasLogIn();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    private IEnumerator esperarDatosRecuperaContraseña()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => Conexion.EstadoActualConexion == estadoConexion.iniciandoSesion);
        if (Conexion.EstadoActualConexion == estadoConexion.termineRecuperacionContraseña)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("¡Todo listo!, Revisa tu correo electrónico para cambiar tu contraseña.");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            iniciarCanvasLogIn();
            cerrarGrafico();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleRecuperacionContraseñaConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión, comprueba el estado de tu red a internet.");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleRecuperacionContraseñaDatos)
                {
                    if (Conexion.RespuestaServidor == "Error")
                    {
                        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El correo electrónico ingresado no está registrado en el sistema.");
                    }
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciarBotones();
    }

    public void iniciarCanvasLogIn()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn"))
        {
            Instantiate(graficos.CanvasLogIn, Vector3.zero, Quaternion.identity);
        }
    }

}