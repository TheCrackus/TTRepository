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
    public List<listaInventario> listaInventarios = new List<listaInventario>();
    public List<inventarioItem> items = new List<inventarioItem>();
    public List<valorString> strings = new List<valorString>();

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
        foreach (inventarioItem item in items)
        {
            item.reiniciaValores();
        }
        foreach (listaInventario listaInventario in listaInventarios) 
        {
            listaInventario.reiniciaValores();
        }
        foreach (valorString lstring in strings) 
        {
            lstring.reiniciaValores();
        }
    }

    public void reiniciaObjetosScriptable()
    {
        reiniciaValoresScriptable();
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name));
            }
        }
    }

    public void guardaObjetosScriptable()
    {
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            FileStream archivo = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name));
            BinaryFormatter binario = new BinaryFormatter();
            var json = JsonUtility.ToJson(objeto);
            binario.Serialize(archivo, json);
            archivo.Close();
        }
    }

    public void cargaObjetosScriptable()
    {
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name)))
            {
                FileStream archivo = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name), FileMode.Open);
                BinaryFormatter binario = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binario.Deserialize(archivo), objeto);
                archivo.Close();
            }
        }
    }
}
