using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorRegistro : manejadorFormulario, pulsoBoton
{

    private bool pulseBoton;

    private string msjFormulario;

    private bool fechaCorrecta;

    private bool emailCorrecto;

    private bool sobrenombreCorrecto;

    private bool passwordCorrecta;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

    void Start()
    {
        msjFormulario = "Favor de verificar la siguiente informaci�n:\n\n";
        fechaCorrecta = true;
        emailCorrecto = true;
        passwordCorrecta = true;
        sobrenombreCorrecto = true;
        reiniciaBotones();
    }

    public void botonRegistrar() 
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            string dia = "";
            string mes = "";
            string a�o = "";
            string nacimiento = "";
            if (((componentesGraficosRegistro)Graficos).MesFiled.text.ToString() != ""
                && ((componentesGraficosRegistro)Graficos).DiaFiled.text.ToString() != ""
                && ((componentesGraficosRegistro)Graficos).A�oFiled.text.ToString() != ""
                && ((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString() != ""
                && ((componentesGraficosRegistro)Graficos).PasswordFiled.text.ToString() != ""
                && ((componentesGraficosRegistro)Graficos).SobrenombreFiled.text.ToString() != "")
            {
                if ( ((componentesGraficosRegistro)Graficos).MesFiled.text.ToString().Contains("0") )
                {
                    try
                    {
                        if (int.Parse(((componentesGraficosRegistro)Graficos).MesFiled.text.ToString()) <= 0)
                        {
                            fechaCorrecta = false;
                            msjFormulario += "No existe un mes igual o menor a 0.";
                        }
                        else 
                        {
                            mes = ((componentesGraficosRegistro)Graficos).MesFiled.text.ToString();
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
                    mes = ((componentesGraficosRegistro)Graficos).MesFiled.text.ToString();
                }
                if (fechaCorrecta) 
                {
                    if (int.Parse(mes) <= 12)
                    {
                        if (((componentesGraficosRegistro)Graficos).DiaFiled.text.ToString().Contains("0"))
                        {
                            try
                            {
                                if (int.Parse(((componentesGraficosRegistro)Graficos).DiaFiled.text.ToString()) <= 0)
                                {
                                    fechaCorrecta = false;
                                    msjFormulario += "No existe un d�a igual o menor a 0.";
                                }else
                                {
                                    dia = ((componentesGraficosRegistro)Graficos).DiaFiled.text.ToString();
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
                            dia = ((componentesGraficosRegistro)Graficos).DiaFiled.text.ToString();
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
                                if (((componentesGraficosRegistro)Graficos).A�oFiled.text.ToString().Contains("0"))
                                {
                                    try
                                    {
                                        if (int.Parse(((componentesGraficosRegistro)Graficos).A�oFiled.text.ToString()) <= 0)
                                        {
                                            fechaCorrecta = false;
                                            msjFormulario += "No existe un a�o igual o menor a 0.";
                                        }
                                        else 
                                        {
                                            a�o = ((componentesGraficosRegistro)Graficos).A�oFiled.text.ToString();
                                            while (a�o.StartsWith("0"))
                                            {
                                                a�o = a�o.Remove(0, 1);
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
                                    a�o = ((componentesGraficosRegistro)Graficos).A�oFiled.text.ToString();
                                }
                                if (fechaCorrecta) 
                                {
                                    int a�oDeseadoMax = DateTime.Now.Year - 9;
                                    int a�oDeseadoMin = a�oDeseadoMax - 4;
                                    if (int.Parse(a�o) < a�oDeseadoMin || int.Parse(a�o) > a�oDeseadoMax)
                                    {
                                        fechaCorrecta = false;
                                        msjFormulario += "Necesitamos que te encuentres entre el rango de edad de 10 y 12 a�os.";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        fechaCorrecta = false;
                        msjFormulario += "No existen m�s de 12 meses al a�o.";
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
                nacimiento = a�o + "-" + mes + "-" + dia;
                if (((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString().StartsWith("@")
                    || ((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString().Length < 13)
                {
                    emailCorrecto = false;
                    msjFormulario += "Tu correo electronico no es v�lido.";
                }
                else 
                {
                    if (((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString().EndsWith("@hotmail.com")
                    || ((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString().EndsWith("@gmail.com")
                    || ((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString().EndsWith("@outlook.com"))
                    {

                    }
                    else 
                    {
                        emailCorrecto = false;
                        msjFormulario += "Tu correo electronico no es v�lido.";
                    }
                }
                if (emailCorrecto)
                {
                    if (((componentesGraficosRegistro)Graficos).SobrenombreFiled.text.ToString().Length < 4) 
                    {
                        sobrenombreCorrecto = false;
                        msjFormulario += "Tu sobrenombre no es v�lido, debe tener al menos 4 caracteres.";
                    }
                    if (sobrenombreCorrecto) 
                    {
                        if (((componentesGraficosRegistro)Graficos).PasswordFiled.text.ToString().Length < 4) 
                        {
                            passwordCorrecta = false;
                            msjFormulario += "Tu contrase�a no es v�lida, debe tener al menos 4 caracteres.";
                        }
                    }
                }
            }

            if (fechaCorrecta && emailCorrecto && sobrenombreCorrecto && passwordCorrecta)
            {
                Conexion.registraUsuario(((componentesGraficosRegistro)Graficos).EmailFiled.text.ToString(),
                    ((componentesGraficosRegistro)Graficos).PasswordFiled.text.ToString(),
                    ((componentesGraficosRegistro)Graficos).SobrenombreFiled.text.ToString(), 
                    nacimiento);
                pulseBoton = true;
                StartCoroutine(esperaDatosRegistro());
            }
            else 
            {
                iniciaVentanaEmergente();
                ManejadorVentanaEmergente.enviaTexto(msjFormulario);
                msjFormulario = "Favor de verificar la siguiente informaci�n:\n\n";
                fechaCorrecta = true;
                emailCorrecto = true;
                sobrenombreCorrecto = true;
                passwordCorrecta = true;
                reiniciaBotones();
            }
        }
    }

    public void iniciaCanvasLogIn()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasLogIn"))
        {
            Instantiate(((componentesGraficosRegistro)Graficos).CanvasLogIn, Vector3.zero, Quaternion.identity);
        }
    }

    public void botonRegresar() 
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            iniciaCanvasLogIn();
            pulseBoton = true;
            cierraFormulario();
        }
    }

    public void cierraFormulario()
    {
        cierraGrafico();
    }

    private IEnumerator esperaDatosRegistro()
    {
        iniciaVentanaEmergente();
        ManejadorVentanaEmergente.enviaTexto("Procesando datos...");
        yield return new WaitWhile(() => Conexion.EstadoActualConexion == estadoConexion.iniciandoRegistro);
        if (Conexion.EstadoActualConexion == estadoConexion.termineRegistro)
        {
            ManejadorVentanaEmergente.enviaTexto("Registro completo...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            cierraFormulario();
            iniciaCanvasLogIn();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleRegistroConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexi�n...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleRegistroDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no pudo ser registrado...");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciaBotones();
    }

    public void reiniciaBotones()
    {
        pulseBoton = false;
    }
}
