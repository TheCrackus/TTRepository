using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorFormularioEnlaceProfesor : ManejadorFormulario, ICanvasFormularioLogIn
{

    private ComponenteGraficoFormularioEnlaceProfesor graficos;

    void Start()
    {
        graficos = (ComponenteGraficoFormularioEnlaceProfesor) ComponenteGrafico;
        reiniciarBotones();
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Por favor, ingrese una contraseña grupal perteneciente a algún docente o profesor para poder iniciar a jugar.");
    }

    public void enlazarAlumnoProfesorBoton() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            bloquearBotones();
            if (graficos.PasswordGrupoFiled.text.ToString() != null && graficos.PasswordGrupoFiled.text.ToString() != "")
            {
                Conexion.enlazarUsuario(graficos.PasswordGrupoFiled.text.ToString());
                StartCoroutine(esperarDatosEnlazaUsuario());
            }
            else
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("La contraseña proporcionada es incorrecta.");
                reiniciarBotones();
            }
        }
    }

    public void regresarBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            iniciarCanvasLogIn();
            bloquearBotones();
            cerrarSesion();
        }
    }
    public void cerrarSesion()
    {
        Conexion.cierraSesion();
        cerrarGrafico();
    }

    public void iniciarCanvasLogIn()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn"))
        {
            Instantiate(graficos.CanvasLogIn, Vector3.zero, Quaternion.identity);
        }
    }

    private IEnumerator esperarDatosEnlazaUsuario()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == estadoConexion.iniciandoEnlace));
        if (Conexion.EstadoActualConexion == estadoConexion.termineEnlace)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Enalce completo...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario fue enlazado, por favor, inicia sesión de nuevo.");
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleEnlaceConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleEnlaceDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario no pudo ser enlazado...");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciarBotones();
    }

}
