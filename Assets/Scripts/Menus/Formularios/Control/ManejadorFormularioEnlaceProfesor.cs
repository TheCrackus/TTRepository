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
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Por favor, ingrese una contrase�a grupal perteneciente a alg�n docente o profesor para poder iniciar a jugar.");
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
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("La contrase�a proporcionada es incorrecta.");
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

    private IEnumerator esperarDatosEnlazaUsuario()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == EstadoConexion.iniciandoEnlace));
        if (Conexion.EstadoActualConexion == EstadoConexion.termineEnlace)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Enlace completado con �xito, por favor, inicia sesi�n de nuevo.");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            iniciarCanvasLogIn();
            cerrarGrafico();
        }
        else
        {
            if (Conexion.EstadoActualConexion == EstadoConexion.falleEnlaceConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexi�n, comprueba el estado de tu red a internet.");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == EstadoConexion.falleEnlaceDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El enlace no se completo con �xito, por favor, verifica la contrase�a grupal que has ingresado.");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = EstadoConexion.ninguno;
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
