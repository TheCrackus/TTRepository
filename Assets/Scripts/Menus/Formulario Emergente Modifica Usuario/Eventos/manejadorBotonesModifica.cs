using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBotonesModifica : formulario
{
    private string msjFormulario;

    private bool fechaCorrecta;

    private bool emailCorrecto;

    private bool sobrenombreCorrecto;

    private bool passwordCorrecta;

    private conexionWeb conexion;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailFiled;

    [SerializeField] private InputField passwordFiled;

    [SerializeField] private InputField passwordFieldConf;

    [SerializeField] private InputField sobrenombreFiled;

    [SerializeField] private InputField diaFiled;

    [SerializeField] private InputField mesFiled;

    [SerializeField] private InputField añoFiled;

    void Start()
    {
        msjFormulario = "Favor de verificar la siguiente información:\n\n";
        fechaCorrecta = true;
        emailCorrecto = true;
        passwordCorrecta = true;
        sobrenombreCorrecto = true;
        reiniciaBotones();
        conexion = gameObject.GetComponent<conexionWeb>();
    }

    public void botonModifica() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            string dia = "";
            string mes = "";
            string año = "";
            string nacimiento = "";
            if (mesFiled.text.ToString() != ""
                && diaFiled.text.ToString() != ""
                && añoFiled.text.ToString() != ""
                && emailFiled.text.ToString() != ""
                && passwordFiled.text.ToString() != ""
                && sobrenombreFiled.text.ToString() != "")
            {
                if (mesFiled.text.ToString().Contains("0"))
                {
                    try
                    {
                        if (int.Parse(mesFiled.text.ToString()) <= 0)
                        {
                            fechaCorrecta = false;
                            msjFormulario += "No existe un mes igual o menor a 0.";
                        }
                        else
                        {
                            mes = mesFiled.text.ToString();
                            while (mes.StartsWith("0"))
                            {
                                mes = mes.Remove(0, 1);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                }
                else
                {
                    mes = mesFiled.text.ToString();
                }
                if (fechaCorrecta)
                {
                    if (int.Parse(mes) <= 12)
                    {
                        if (diaFiled.text.ToString().Contains("0"))
                        {
                            try
                            {
                                if (int.Parse(diaFiled.text.ToString()) <= 0)
                                {
                                    fechaCorrecta = false;
                                    msjFormulario += "No existe un día igual o menor a 0.";
                                }
                                else
                                {
                                    dia = diaFiled.text.ToString();
                                    while (dia.StartsWith("0"))
                                    {
                                        dia = dia.Remove(0, 1);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e.Message);
                            }
                        }
                        else
                        {
                            dia = diaFiled.text.ToString();
                        }
                        if (fechaCorrecta)
                        {
                            switch (mes)
                            {
                                case "1":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Enero no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "2":
                                    if (int.Parse(dia) > 28)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Febrero no tiene mas de 28 dias.";
                                    }
                                    break;
                                case "3":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Marzo no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "4":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Abril no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "5":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Mayo no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "6":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Junio no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "7":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Julio no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "8":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Agosto no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "9":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Septiembre no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "10":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Octubre no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "11":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Noviembre no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "12":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Diciembre no tiene mas de 31 dias.";
                                    }
                                    break;
                            }
                            if (fechaCorrecta)
                            {
                                if (añoFiled.text.ToString().Contains("0"))
                                {
                                    try
                                    {
                                        if (int.Parse(añoFiled.text.ToString()) <= 0)
                                        {
                                            fechaCorrecta = false;
                                            msjFormulario += "No existe un año igual o menor a 0.";
                                        }
                                        else
                                        {
                                            año = añoFiled.text.ToString();
                                            while (año.StartsWith("0"))
                                            {
                                                año = año.Remove(0, 1);
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.LogError(e.Message);
                                    }
                                }
                                else
                                {
                                    año = añoFiled.text.ToString();
                                }
                                if (fechaCorrecta)
                                {
                                    int añoDeseadoMax = DateTime.Now.Year - 9;
                                    int añoDeseadoMin = añoDeseadoMax - 4;
                                    if (int.Parse(año) < añoDeseadoMin || int.Parse(año) > añoDeseadoMax)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Necesitamos que te encuentres entre el rango de edad de 10 y 12 años.";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        fechaCorrecta = false;
                        msjFormulario += "No existen más de 12 meses al año.";
                    }
                }
            }
            else
            {
                fechaCorrecta = false;
                msjFormulario += "Termina de ingresar los datos en el formulario.";
            }
            if (fechaCorrecta)
            {
                if (int.Parse(dia) < 10)
                {
                    dia = "0" + dia;
                }
                if (int.Parse(mes) < 10)
                {
                    mes = "0" + mes;
                }
                nacimiento = año + "-" + mes + "-" + dia;
                if (emailFiled.text.ToString().StartsWith("@")
                    || emailFiled.text.ToString().Length < 13)
                {
                    emailCorrecto = false;
                    msjFormulario += "Tu correo electronico nuevo no es válido.";
                }
                else
                {
                    if (emailFiled.text.ToString().EndsWith("@hotmail.com")
                    || emailFiled.text.ToString().EndsWith("@gmail.com")
                    || emailFiled.text.ToString().EndsWith("@outlook.com"))
                    {

                    }
                    else
                    {
                        emailCorrecto = false;
                        msjFormulario += "Tu correo electronico nuevo no es válido.";
                    }
                }
                if (emailCorrecto)
                {
                    if (sobrenombreFiled.text.ToString().Length < 4)
                    {
                        sobrenombreCorrecto = false;
                        msjFormulario += "Tu sobrenombre nuevo no es válido, debe tener al menos 4 caracteres.";
                    }
                    if (sobrenombreCorrecto)
                    {
                        if (passwordFiled.text.ToString().Length < 4)
                        {
                            passwordCorrecta = false;
                            msjFormulario += "Tu contraseña nueva no es válida, debe tener al menos 4 caracteres.";
                        }
                        if (!passwordFieldConf.text.ToString().Equals(conexion.getMiUsuario().datosEjecucion.password)) 
                        {
                            passwordCorrecta = false;
                            msjFormulario += "Tu contraseña actual no es válida, ingresa la correcta.";
                        }
                    }
                }
            }

            if (fechaCorrecta && emailCorrecto && sobrenombreCorrecto && passwordCorrecta)
            {
                conexion.modificaUsuario(emailFiled.text.ToString(), passwordFiled.text.ToString(), sobrenombreFiled.text.ToString(), nacimiento);
                PulseBoton = true;
                StartCoroutine(esperaDatosModificar());
            }
            else
            {
                iniciaVentanaEmergente();
                ManejadorVentanaEmergente.enviaTexto(msjFormulario);
                ManejadorVentanaEmergente.reiniciaTiempo();
                msjFormulario = "Favor de verificar la siguiente información:\n\n";
                fechaCorrecta = true;
                emailCorrecto = true;
                sobrenombreCorrecto = true;
                passwordCorrecta = true;
                reiniciaBotones();
            }
        }
    }

    public void botonRegresar()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickCerrar();
            EventoReiniciaBotones.invocaFunciones();
            PulseBoton = true;
            Destroy(CanvasFormulario);
        }
        else 
        {
            EventoReiniciaBotones.invocaFunciones();
            EventoCierraSesion.invocaFunciones();
            Destroy(CanvasFormulario);
        }
    }

    public void cierraSesion()
    {
        EventoReiniciaBotones.invocaFunciones();
        EventoCierraSesion.invocaFunciones();
        Destroy(CanvasFormulario);
    }

    private IEnumerator esperaDatosModificar()
    {
        iniciaVentanaEmergente();
        ManejadorVentanaEmergente.enviaTexto("Procesando datos...");
        ManejadorVentanaEmergente.reiniciaTiempo();
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoModificacion));
        if (conexion.getEstadoActualConexion() == conexionState.termineModificacion)
        {
            ManejadorVentanaEmergente.enviaTexto("Registro completo...");
            ManejadorVentanaEmergente.reiniciaTiempo();
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            cierraSesion();
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleModificacionConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexión...");
                ManejadorVentanaEmergente.reiniciaTiempo();
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleModificacionDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no pudo ser modificado...");
                    ManejadorVentanaEmergente.reiniciaTiempo();
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        reiniciaBotones();
    }
}
