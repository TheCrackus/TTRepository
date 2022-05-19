using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class usuario : ScriptableObject
{
    [System.Serializable]
    public struct logro
    {
        public int nombre;
        public string descripcion;
    }

    [System.Serializable]
    public struct datosUsuario
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
    }

    [Header("Datos usuario iniciales")]
    public datosUsuario datosIniciales;
    [Header("Datos usuario en ejecucion del juego")]
    public datosUsuario datosEjecucion;


    public void reiniciaValores()
    {
        datosEjecucion = datosIniciales;
    }
}
