using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class usuario : ScriptableObject
{
    [System.Serializable]
    public struct configuracion
    {
        public int volumen;
        public string resolucion;
    }

    [System.Serializable]
    public struct partida
    {
        public int id_partida;
        public int Vida;
        public int Puntos;
        public int Enemigos;
        public int id_punto_control;
        public string[] Inventario;
    }

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
        public int puntos;
        public int enemigos;
        public int nivelesT;
        public partida partida;
        public logro[] logros;
        public configuracion Configuracion;
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
