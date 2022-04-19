using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorEscenas : MonoBehaviour
{

    public valorVectorial posicionPlayerMapa;
    public cambioEscena estadoCambioEscenas;

    public void Awake()
    {
        if (!estadoCambioEscenas.cambioEjecucion) 
        {
            Scene escenaActual = SceneManager.GetActiveScene();
            string nombreEscena = escenaActual.name;
            if (nombreEscena == "Laberintos")
            {
                posicionPlayerMapa.valorEjecucion = new Vector3(2, -13.5f, 0);
                estadoCambioEscenas.direccionPlayerEjecucion = new Vector2(1, 0);
                estadoCambioEscenas.camaraPosicionEjecucion = new Vector3(11, -18, -10);
            }
            else
            {
                if (nombreEscena == "Mazmorra")
                {
                    posicionPlayerMapa.valorEjecucion = new Vector3(12.5f, -23.5f, 0);
                    estadoCambioEscenas.direccionPlayerEjecucion = new Vector2(0, 1);
                    estadoCambioEscenas.camaraPosicionEjecucion = new Vector3(11, -18, -10);

                }
            }
        }
    }
}
