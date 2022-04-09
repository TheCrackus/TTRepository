using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cofre : interactuador
{

    public objeto objetoContenido;
    private bool cofreAbierto;
    private bool cofreVacio;
    public evento muestraObjeto;
    public GameObject objetoContenedorTextoDialogos;
    public Text textoDialogos;
    public inventario inventarioPlayer;
    private Animator cofreAnimator;

    // Start is called before the first frame update
    void Start()
    {
        cofreAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && getPlayerEnRango())
        {
            if (!cofreAbierto && !cofreVacio)
            {
                abreCofre();
            }
            else
            {
                cofreYaAbierto();
            }
        }
    }

    public void abreCofre() 
    {
        textoDialogos.text = objetoContenido.descripcionObjeto;
        objetoContenedorTextoDialogos.SetActive(true);
        inventarioPlayer.agregaObjeto(objetoContenido);
        inventarioPlayer.objetoActual = objetoContenido;
        muestraObjeto.invocaEventosLista();
        simboloActivoDesactivo.invocaEventosLista();
        cofreAbierto = true;
        cofreAnimator.SetBool("Abrir", true);
    }

    public void cofreYaAbierto() {
        if (!cofreVacio)
        {
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
            muestraObjeto.invocaEventosLista();
            simboloActivoDesactivo.invocaEventosLista();
            cofreVacio = true;
        }
        else 
        {
            if (objetoContenedorTextoDialogos.activeInHierarchy)
            {
                objetoContenedorTextoDialogos.SetActive(false);
            }
            else 
            {
                textoDialogos.text = "Un cofre vac�o...";
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
