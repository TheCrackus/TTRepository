using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorDialogos : MonoBehaviour
{

    public GameObject objetoContenedorTextoDialogos;
    public Text textoDialogos;
    public string dialogo;
    public bool debeMostrarDialogo;
    private bool playerEnRango;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerEnRango) 
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
            && colisionDetectada.isTrigger) 
        {
            playerEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            playerEnRango = false;
            objetoContenedorTextoDialogos.SetActive(false);
        }
    }
}
