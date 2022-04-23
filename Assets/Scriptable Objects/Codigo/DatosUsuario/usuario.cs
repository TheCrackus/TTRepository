using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class usuario : ScriptableObject, ISerializationCallbackReceiver
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
        public int id_jugador;
        public string Sobrenombre;
        public string nacimiento;
        public string mail;
        public string password;
        public int Puntos;
        public int Enemigos;
        public int NivelesT;
        public configuracion Configuracion;
        public partida Partida;
        public logro[] Logros;
    }

    [Header("Datos usuario iniciales")]
    public datosUsuario datosIniciales;
    [Header("Datos usuario en ejecucion")]
    public datosUsuario datosEjecucuion;
    [Header("Datos usuario para cerrar sesion")]
    public datosUsuario datosReset;

    public void OnAfterDeserialize()
    {
        datosEjecucuion = datosIniciales;
    }

    public void OnBeforeSerialize()
    {

    }
}
