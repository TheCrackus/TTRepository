using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu]
public class datosJuego : ScriptableObject
{
    [Header("Objetos que no deben ser reiniciados")]
    public List<ScriptableObject> objetosPersistentesGeneral = new List<ScriptableObject>();
    public List<valorVectorial> vectores = new List<valorVectorial>();
    public List<valorFlotante> flotantes = new List<valorFlotante>();
    public List<valorBooleano> booleanos = new List<valorBooleano>();
    public List<usuario> usuarios = new List<usuario>();
    public List<cambioEscena> escenas = new List<cambioEscena>();
    public List<inventario> inventarios = new List<inventario>();
    public List<listaInventario> listaInventarios = new List<listaInventario>();
    public List<inventarioItem> items = new List<inventarioItem>();

    private void reiniciaValoresScriptable()
    {
        foreach (valorVectorial vector in vectores)
        {
            vector.reiniciaValores();
        }
        foreach (valorFlotante flotante in flotantes)
        {
            flotante.reiniciaValores();
        }
        foreach (valorBooleano booleano in booleanos)
        {
            booleano.reiniciaValores();
        }
        foreach (usuario usuario in usuarios)
        {
            usuario.reiniciaValores();
        }
        foreach (cambioEscena escena in escenas)
        {
            escena.reiniciaValores();
        }
        foreach (inventario inventario in inventarios)
        {
            inventario.reiniciaValores();
        }
        foreach (inventarioItem item in items)
        {
            item.reiniciaValores();
        }
        foreach (listaInventario listaInventario in listaInventarios) 
        {
            listaInventario.reiniciaValores();
        }
    }

    public void reiniciaObjetosScriptable()
    {
        reiniciaValoresScriptable();
        int numeroObjeto = 0;
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", numeroObjeto)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", numeroObjeto));
            }
            numeroObjeto++;
        }
    }

    public void guardaObjetosScriptable()
    {
        int numeroObjeto = 0;
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            FileStream archivo = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", numeroObjeto));
            BinaryFormatter binario = new BinaryFormatter();
            var json = JsonUtility.ToJson(objeto);
            binario.Serialize(archivo, json);
            archivo.Close();
            numeroObjeto++;
        }
    }

    public void cargaObjetosScriptable()
    {
        int numeroObjeto = 0;
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", numeroObjeto)))
            {
                FileStream archivo = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", numeroObjeto), FileMode.Open);
                BinaryFormatter binario = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binario.Deserialize(archivo), objeto);
                archivo.Close();
            }
            numeroObjeto++;
        }
    }
}
