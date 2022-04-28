using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorDialogos : interactuador
{
    [Header("Objeto contenedor del Texto a mostrar")]
    public GameObject objetoContenedorTextoDialogos;
    [Header("Objeto texto para mostrar los dialogos")]
    public Text textoDialogos;
    [Header("Texto a mostrar")]
    public string dialogo;    

    void Update()
    {
        if (Input.GetButtonDown("Interactuar") && playerEnRango) 
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
    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = false;
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
        }
    }
}
