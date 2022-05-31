using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManejadorFormularioRegistrarUsuario : ManejadorFormulario, ICanvasFormularioLogIn
{

    private ComponenteGraficoRegistrarUsuario graficos;

    private string mensajeFormulario;

    private bool fechaCorrecta;

    private bool emailCorrecto;

    private bool sobrenombreCorrecto;

    private bool passwordCorrecta;

    void Start()
    {
        graficos = (ComponenteGraficoRegistrarUsuario)ComponenteGrafico;
        mensajeFormulario = "Favor de verificar la siguiente información:\n\n";
        fechaCorrecta = true;
        emailCorrecto = true;
        passwordCorrecta = true;
        sobrenombreCorrecto = true;
        reiniciarBotones();
    }

    public void registrarBoton() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            string dia = "";
            string mes = "";
            string año = "";
            string nacimiento = "";
            if (graficos.MesFiled.text.ToString() != ""
                && graficos.DiaFiled.text.ToString() != ""
                && graficos.AñoFiled.text.ToString() != ""
                && graficos.EmailFiled.text.ToString() != ""
                && graficos.PasswordFiled.text.ToString() != ""
                && graficos.SobrenombreFiled.text.ToString() != "")
            {
                if (graficos.MesFiled.text.ToString().Contains("0") )
                {
                    try
                    {
                        if (int.Parse(graficos.MesFiled.text.ToString()) <= 0)
                        {
                            fechaCorrecta = false;
                            mensajeFormulario += "No existe un mes igual o menor a 0.";
                        }
                        else 
                        {
                            mes = graficos.MesFiled.text.ToString();
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
                    mes = graficos.MesFiled.text.ToString();
                }
                if (fechaCorrecta) 
                {
                    if (int.Parse(mes) <= 12)
                    {
                        if (graficos.DiaFiled.text.ToString().Contains("0"))
                        {
                            try
                            {
                                if (int.Parse(graficos.DiaFiled.text.ToString()) <= 0)
                                {
                                    fechaCorrecta = false;
                                    mensajeFormulario += "No existe un día igual o menor a 0.";
                                }else
                                {
                                    dia = graficos.DiaFiled.text.ToString();
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
                            dia = graficos.DiaFiled.text.ToString();
                        }
                        if (fechaCorrecta) 
                        {
                            switch (mes)
                            {
                                case "1":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Enero no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "2":
                                    if (int.Parse(dia) > 28)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Febrero no tiene mas de 28 dias.";
                                    }
                                    break;
                                case "3":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Marzo no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "4":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Abril no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "5":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Mayo no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "6":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Junio no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "7":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Julio no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "8":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Agosto no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "9":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Septiembre no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "10":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Octubre no tiene mas de 31 dias.";
                                    }
                                    break;
                                case "11":
                                    if (int.Parse(dia) > 30)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Noviembre no tiene mas de 30 dias.";
                                    }
                                    break;
                                case "12":
                                    if (int.Parse(dia) > 31)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Diciembre no tiene mas de 31 dias.";
                                    }
                                    break;
                            }
                            if (fechaCorrecta) 
                            {
                                if (graficos.AñoFiled.text.ToString().Contains("0"))
                                {
                                    try
                                    {
                                        if (int.Parse(graficos.AñoFiled.text.ToString()) <= 0)
                                        {
                                            fechaCorrecta = false;
                                            mensajeFormulario += "No existe un año igual o menor a 0.";
                                        }
                                        else 
                                        {
                                            año = graficos.AñoFiled.text.ToString();
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
                                    año = graficos.AñoFiled.text.ToString();
                                }
                                if (fechaCorrecta) 
                                {
                                    int añoDeseadoMax = DateTime.Now.Year - 9;
                                    int añoDeseadoMin = añoDeseadoMax - 4;
                                    if (int.Parse(año) < añoDeseadoMin || int.Parse(año) > añoDeseadoMax)
                                    {
                                        fechaCorrecta = false;
                                        mensajeFormulario += "Necesitamos que te encuentres entre el rango de edad de 10 y 12 años.";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        fechaCorrecta = false;
                        mensajeFormulario += "No existen más de 12 meses al año.";
                    }
                }
            }
            else 
            {
                fechaCorrecta = false;
                mensajeFormulario += "Termina de ingresar los datos en el formulario.";
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
                if (graficos.EmailFiled.text.ToString().StartsWith("@")
                    || graficos.EmailFiled.text.ToString().Length < 13)
                {
                    emailCorrecto = false;
                    mensajeFormulario += "Tu correo electronico no es válido.";
                }
                else 
                {
                    if (graficos.EmailFiled.text.ToString().EndsWith("@hotmail.com")
                    || graficos.EmailFiled.text.ToString().EndsWith("@gmail.com")
                    || graficos.EmailFiled.text.ToString().EndsWith("@outlook.com"))
                    {

                    }
                    else 
                    {
                        emailCorrecto = false;
                        mensajeFormulario += "Tu correo electronico no es válido.";
                    }
                }
                if (emailCorrecto)
                {
                    if (graficos.SobrenombreFiled.text.ToString().Length < 4) 
                    {
                        sobrenombreCorrecto = false;
                        mensajeFormulario += "Tu sobrenombre no es válido, debe tener al menos 4 caracteres.";
                    }
                    if (sobrenombreCorrecto) 
                    {
                        if (graficos.PasswordFiled.text.ToString().Length < 4) 
                        {
                            passwordCorrecta = false;
                            mensajeFormulario += "Tu contraseña no es válida, debe tener al menos 4 caracteres.";
                        }
                    }
                }
            }

            if (fechaCorrecta && emailCorrecto && sobrenombreCorrecto && passwordCorrecta)
            {
                Conexion.registrarUsuario(graficos.EmailFiled.text.ToString(),
                    graficos.PasswordFiled.text.ToString(),
                    graficos.SobrenombreFiled.text.ToString(), 
                    nacimiento);
                bloquearBotones();
                StartCoroutine(esperarDatosRegistro());
            }
            else 
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente(mensajeFormulario);
                mensajeFormulario = "Favor de verificar la siguiente información:\n\n";
                fechaCorrecta = true;
                emailCorrecto = true;
                sobrenombreCorrecto = true;
                passwordCorrecta = true;
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
            cerrarGrafico();
        }
    }

    private IEnumerator esperarDatosRegistro()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => Conexion.EstadoActualConexion == estadoConexion.iniciandoRegistro);
        if (Conexion.EstadoActualConexion == estadoConexion.termineRegistro)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Registro completo...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            cerrarGrafico();
            iniciarCanvasLogIn();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleRegistroConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleRegistroDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario no pudo ser registrado...");
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
