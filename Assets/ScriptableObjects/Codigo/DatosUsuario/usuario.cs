using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Usuario : ScriptableObject
{
    [System.Serializable]
    public struct logro
    {
        public int nombre;
        public string descripcion;
    }

    [System.Serializable]
    public struct DatosUsuario
    {
        public int idJugador;
        public string sobrenombre;
        public string nacimiento;
        public string mail;
        public string password;
        public string puntos;
        public int enemigos;
        public int nivelesT;
        public logro[] logros;
        public string verificado;
    }

    [Header("Datos usuario iniciales")]
    [SerializeField]  private DatosUsuario datosIniciales;

    [Header("Datos usuario en ejecucion del juego")]
    [SerializeField] private DatosUsuario datosEjecucion;

    public DatosUsuario DatosIniciales { get => datosIniciales; set => datosIniciales = value; }
    public DatosUsuario DatosEjecucion { get => datosEjecucion; set => datosEjecucion = value; }

    public void reiniciarScriptable()
    {
        datosEjecucion = datosIniciales;
    }

}
