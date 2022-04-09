using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorDialogos : interactuador
{

    public GameObject objetoContenedorTextoDialogos;
    public Text textoDialogos;
    public string dialogo;    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && getPlayerEnRango()) 
        {
            if (objetoContenedorTextoDialogos.activeInHierarchy)
            {
                objetoContenedorTextoDialogos.SetActive(false);
            }
            else 
            {
                textoDialogos.text = dialogo;
                objetoContenedorTextoDialogos.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaEventosLista();
            setPlayerEnRango(true);
        }
    }

    private void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaEventosLista();
            setPlayerEnRango(false);
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
        }
    }
}
