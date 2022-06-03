using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu]
public class DatosJuego : ScriptableObject
{

    [Header("Objetos que no deben ser reiniciados al cambiar la escena")]
    public List<ScriptableObject> objetosPersistentesGeneral = new List<ScriptableObject>();
    public List<ValorVectorial> vectores = new List<ValorVectorial>();
    public List<ValorFlotante> flotantes = new List<ValorFlotante>();
    public List<ValorBooleano> booleanos = new List<ValorBooleano>();
    public Usuario miUsuario;
    public List<CambioEscena> escenas = new List<CambioEscena>();
    public List<ListaInventario> listaInventarios = new List<ListaInventario>();
    public List<InventarioItem> items = new List<InventarioItem>();
    public List<ValorString> strings = new List<ValorString>();

    public void reiniciarScriptable()
    {
        foreach (ValorVectorial vector in vectores)
        {
            vector.reiniciarValores();
        }
        foreach (ValorFlotante flotante in flotantes)
        {
            flotante.reiniciarValores();
        }
        foreach (ValorBooleano booleano in booleanos)
        {
            booleano.reiniciarValores();
        }
        foreach (CambioEscena escena in escenas)
        {
            escena.reiniciarValores();
        }
        foreach (InventarioItem item in items)
        {
            item.reiniciarValores();
        }
        foreach (ListaInventario listaInventario in listaInventarios) 
        {
            listaInventario.reiniciarValores();
        }
        foreach (ValorString lstring in strings) 
        {
            lstring.reiniciarValores();
        }
    }

    public void reiniciarDatos()
    {
        reiniciarScriptable();
        foreach (ScriptableObject objeto in objetosPersistentesGeneral)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", objeto.name));
            }
        }
    }

    public void guardarDatos()
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

    public void cargarDatos()
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
