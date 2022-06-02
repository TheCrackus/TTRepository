using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{

    private Rigidbody2D rigidBodyPlayer;

    private Vector3 vectorMovimiento;

    private Animator animatorPlayer;

    [Header("Estado actual del Player")]
    [SerializeField] private EstadoObjeto estadoPlayer;

    [Header("Estadisticas Player")]
    [SerializeField] private float velocidad;

    [Header("Posicion del Player en el mapa")]
    [SerializeField] private valorVectorial posicionPlayer;

    [Header("Manejador de Sprite del objeto emergente a mostrar")]
    [SerializeField] private SpriteRenderer spriteObjetoMostrar;

    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;

    [Header("Estado general de la escena")]
    [SerializeField] private cambioEscena estadoCambioEscenas;

    [Header("Proyectil que dispara el arma actual a distancia")]
    [SerializeField] private GameObject proyectil;

    [Header("Evento que decrementa la magia")]
    [SerializeField] private evento decrementaMagia;

    [Header("La cantidad de magia que tiene el Player")]
    [SerializeField] private valorFlotante magiaPlayer;

    [Header("Objeto que representa el arco")]
    [SerializeField] private inventarioItem arco;

    [Header("Objeto que representa la espada")]
    [SerializeField] private inventarioItem espada;

    [Header("Manejador de audio del Player arma mele")]
    [SerializeField] private AudioMelee manejadorAudioMelee;

    [Header("Manejador de audio del Player arma distancia")]
    [SerializeField] private AudioProyectil manejadorAudioProyectil;

    public EstadoObjeto EstadoPlayer { get => estadoPlayer; set => estadoPlayer = value; }

    private void Start()
    {
        rigidBodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        animatorPlayer = gameObject.GetComponent<Animator>();
        if (estadoCambioEscenas.direccionPlayerEjecucion.x == 0)
        {
            if (estadoCambioEscenas.direccionPlayerEjecucion.y > 0)
            {
                animatorPlayer.SetFloat("MovimientoX", 0f);
                animatorPlayer.SetFloat("MovimientoY", 1f);
            }
            else
            {
                if (estadoCambioEscenas.direccionPlayerEjecucion.y < 0)
                {
                    animatorPlayer.SetFloat("MovimientoX", 0f);
                    animatorPlayer.SetFloat("MovimientoY", -1f);
                }
            }
        }
        else 
        {
            if (estadoCambioEscenas.direccionPlayerEjecucion.y == 0)
            {
                if (estadoCambioEscenas.direccionPlayerEjecucion.x > 0)
                {
                    animatorPlayer.SetFloat("MovimientoX", 1f);
                    animatorPlayer.SetFloat("MovimientoY", 0f);
                }
                else
                {
                    if (estadoCambioEscenas.direccionPlayerEjecucion.x < 0)
                    {
                        animatorPlayer.SetFloat("MovimientoX", -1f);
                        animatorPlayer.SetFloat("MovimientoY", 0f);
                    }
                }
            }
        }
        gameObject.transform.position = posicionPlayer.valorVectorialEjecucion;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Atacar")
            && inventariopPlayerItems.objetoEquipado != null
            && (estadoPlayer.Estado == EstadoGenerico.caminando 
                || estadoPlayer.Estado == EstadoGenerico.ninguno))
        {
            estadoPlayer.Estado = EstadoGenerico.atacando;
            StartCoroutine(Atacar());
        }
    }

    private void FixedUpdate()
    {
        if (estadoPlayer.Estado != EstadoGenerico.atacando
            && estadoPlayer.Estado != EstadoGenerico.inactivo
            && estadoPlayer.Estado != EstadoGenerico.transicionando
            && estadoPlayer.Estado != EstadoGenerico.interactuando
            && estadoPlayer.Estado != EstadoGenerico.estuneado)
        {
            vectorMovimiento = Vector3.zero;
            vectorMovimiento.x = Input.GetAxisRaw("Horizontal");
            vectorMovimiento.y = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
            {
                vectorMovimiento.y = 0;
            }
            else
            {
                vectorMovimiento.x = 0;
            }
            if (vectorMovimiento != Vector3.zero)
            {
                estadoPlayer.Estado = EstadoGenerico.caminando;
                ActualizarMovimiento();
            }
            else
            {
                if (estadoPlayer.Estado == EstadoGenerico.caminando
                    || estadoPlayer.Estado == EstadoGenerico.ninguno)
                {
                    estadoPlayer.Estado = EstadoGenerico.ninguno;
                    animatorPlayer.SetBool("Movimiento", false);
                }
            }
        }
        else 
        {
            if (estadoPlayer.Estado == EstadoGenerico.interactuando
                || estadoPlayer.Estado == EstadoGenerico.estuneado
                || estadoPlayer.Estado == EstadoGenerico.inactivo
                || estadoPlayer.Estado == EstadoGenerico.transicionando)
            {
                vectorMovimiento = Vector3.zero;
                animatorPlayer.SetBool("Movimiento", false);
            }
            else 
            {
                if (estadoPlayer.Estado == EstadoGenerico.estuneado) 
                {
                    animatorPlayer.SetBool("Movimiento", false);
                }
            }
        }
    }

    private void ActualizarMovimiento()
    {
        vectorMovimiento.Normalize();
        rigidBodyPlayer.MovePosition(transform.position + vectorMovimiento * velocidad * Time.fixedDeltaTime);
        animatorPlayer.SetFloat("MovimientoX", vectorMovimiento.x);
        animatorPlayer.SetFloat("MovimientoY", vectorMovimiento.y);
        animatorPlayer.SetBool("Movimiento", true);
    }

    private IEnumerator Atacar()
    {
        if (inventariopPlayerItems) 
        {
            if (inventariopPlayerItems.objetoEquipado == espada)
            {
                if (estadoPlayer.Estado == EstadoGenerico.atacando)
                {
                    animatorPlayer.SetBool("Atacando", true);
                    yield return null;

                    manejadorAudioMelee.reproducirAudioMelee();
                    animatorPlayer.SetBool("Atacando", false);
                    yield return new WaitForSeconds(0.6f);

                    estadoPlayer.Estado = EstadoGenerico.ninguno;
                }
            }
            else 
            {
                if (inventariopPlayerItems.objetoEquipado == arco) 
                { 
                    if (estadoPlayer.Estado == EstadoGenerico.atacando)
                    {
                        manejadorAudioProyectil.reproducirAudioProyectil();
                        crearFlecha();
                        yield return new WaitForSeconds(0.5f);

                        estadoPlayer.Estado = EstadoGenerico.ninguno;
                    }
                }
            }
        }
        
        
    }

    private void crearFlecha() 
    {
        if (magiaPlayer.valorFlotanteEjecucion > 0) 
        {
            Vector2 vectorTemporal = new Vector2(animatorPlayer.GetFloat("MovimientoX"), animatorPlayer.GetFloat("MovimientoY"));
            flecha flecha = Instantiate(proyectil, gameObject.transform.position, Quaternion.identity).GetComponent<flecha>();
            flecha.dispara(vectorTemporal, elegirDireccionFlecha());
            magiaPlayer.valorFlotanteEjecucion -= flecha.costoMagia;
            if (magiaPlayer.valorFlotanteEjecucion <= 0) 
            {
                magiaPlayer.valorFlotanteEjecucion = 0;
            }
            decrementaMagia.invocaFunciones();
        }
    }

    private Vector3 elegirDireccionFlecha() 
    {
        float direccionZ = Mathf.Atan2(animatorPlayer.GetFloat("MovimientoY"), animatorPlayer.GetFloat("MovimientoX")) * Mathf.Rad2Deg;
        return new Vector3(0,0, direccionZ);
    }

    public void comenzarEmpujaPlayer(float tiempoAplicarFuerza)
    {
        estadoPlayer.Estado = EstadoGenerico.estuneado;
        StartCoroutine(empujarPlayer(tiempoAplicarFuerza));
    }

    private IEnumerator empujarPlayer(float tiempoAplicarFuerza)
    {
        if (rigidBodyPlayer != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);

            rigidBodyPlayer.velocity = Vector2.zero;
            estadoPlayer.Estado = EstadoGenerico.ninguno;
        }
    }

    public void mostrarObjeto() 
    {
        inventarioItem itemMostrar = null;
        foreach (inventarioItem itemLoop in inventariopPlayerItems.inventario) 
        {
            if (itemLoop.mostrarItem) 
            {
                itemMostrar = itemLoop;
                break;
            }
        }
        if (itemMostrar != null)
        {
            if (estadoPlayer.Estado != EstadoGenerico.interactuando)
            {
                animatorPlayer.SetBool("MostrandoObjeto", true);
                estadoPlayer.Estado = EstadoGenerico.interactuando;
                spriteObjetoMostrar.sprite = itemMostrar.imagenItem;
                itemMostrar.mostrarItem = false;
            }
        }
        else 
        {
            if (estadoPlayer.Estado == EstadoGenerico.interactuando)
            {
                animatorPlayer.SetBool("MostrandoObjeto", false);
                estadoPlayer.Estado = EstadoGenerico.ninguno;
                spriteObjetoMostrar.sprite = null;
            }
        }
    }
}