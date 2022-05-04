using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class manejadorDatosGuardados : MonoBehaviour
{
    public datosJuego datos;    

    void OnEnable()
    {
        datos.cargaObjetosScriptable();
    }

}
