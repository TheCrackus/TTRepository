using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorVentanaEmergente : MonoBehaviour
{
    private float contadorTiempoCerrar;
    private bool empiezaContador;
    public Text textoVentanaEmergente;
    public float tiempoCerrar;

    void Awake()
    {
        empiezaContador = false;
    }

    void Start()
    {
        contadorTiempoCerrar = tiempoCerrar;    
    }

    void Update()
    {
        if (empiezaContador) 
        {
            contadorTiempoCerrar -= Time.deltaTime;
            if (contadorTiempoCerrar <= 0) 
            {
                cierraVentanaEmergente();
                contadorTiempoCerrar = tiempoCerrar;
                empiezaContador = false;
            }
        }
    }

    public void cierraVentanaEmergente()
    {
        if (gameObject.activeInHierarchy) 
        {
            textoVentanaEmergente.text = "Advertencias:\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-";
            gameObject.SetActive(false);
        }
    }

    public void abreVentanaEmergente(string textoMostrar, bool debeEmpezarContador) 
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            textoVentanaEmergente.text = textoMostrar;
            empiezaContador = debeEmpezarContador;
        }
        else 
        {
            textoVentanaEmergente.text = textoMostrar;
            empiezaContador = debeEmpezarContador;
        }
    }
}
