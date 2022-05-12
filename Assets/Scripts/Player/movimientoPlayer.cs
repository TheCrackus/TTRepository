using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPlayer : MonoBehaviour
{

    [Header("El manejador de fisicas de este objeto")]
    [SerializeField] private Rigidbody2D rigidBodyPlayer;
    [Header("Vector a donde se mueve este objeto")]
    [SerializeField] private Vector3 vectorMovimiento;
    [Header("El manejador de animaciones de este objeto")]
    [SerializeField] private Animator animatorPlayer;
    [Header("Estado actual del Player")]
    [SerializeField] private estadoObjeto estadoPlayer;
    [Header("Estadisticas Player")]
    [SerializeField] private float velocidad;
    [Header("Evento para animar la pantalla al recivir golpe")]
    [SerializeField] private evento reciveGolpePlayer;
    [Header("Variables globales del player")]
    [SerializeField] private valorVectorial posicionPlayer;
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
    [Header("Manejador de la vida del Player")]
    [SerializeField] private vidaPlayer vidaPlayer;
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio atacar con espada")]
    [SerializeField] private AudioSource audioAtaqueEspada;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAtaqueEspada;
    [Header("Audio arroja proyectil")]
    [SerializeField] private AudioSource audioArrojaProyectil;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioArrojaProyectil;

    public void setEstadoPlayer(estadoGenerico estado)
    {
        estadoPlayer.setEstadoActualObjeto(estado);
    }

    public estadoGenerico getEstadoPlayer()
    {
        return estadoPlayer.getEstadoActualObjeto();
    }

    void Start()
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

    void Update()
    {
        if (Input.GetButtonDown("Atacar")
            && inventariopPlayerItems.objetoEquipado != null
            && (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.caminando 
                || estadoPlayer.getEstadoActualObjeto() == estadoGenerico.ninguno))
        {
            estadoPlayer.setEstadoActualObjeto(estadoGenerico.atacando);
            StartCoroutine(Atacar());
        }
    }

    void FixedUpdate()
    {
        if (estadoPlayer.getEstadoActualObjeto() != estadoGenerico.atacando
            && estadoPlayer.getEstadoActualObjeto() != estadoGenerico.inactivo
            && estadoPlayer.getEstadoActualObjeto() != estadoGenerico.transicionando
            && estadoPlayer.getEstadoActualObjeto() != estadoGenerico.interactuando
            && estadoPlayer.getEstadoActualObjeto() != estadoGenerico.estuneado)
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
                estadoPlayer.setEstadoActualObjeto(estadoGenerico.caminando);
                ActualizarMovimiento();
            }
            else
            {
                if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.caminando
                    || estadoPlayer.getEstadoActualObjeto() == estadoGenerico.ninguno)
                {
                    estadoPlayer.setEstadoActualObjeto(estadoGenerico.ninguno);
                    animatorPlayer.SetBool("Movimiento", false);
                }
            }
        }
        else 
        {
            if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.interactuando
                || estadoPlayer.getEstadoActualObjeto() == estadoGenerico.estuneado
                || estadoPlayer.getEstadoActualObjeto() == estadoGenerico.inactivo
                || estadoPlayer.getEstadoActualObjeto() == estadoGenerico.transicionando)
            {
                vectorMovimiento = Vector3.zero;
                animatorPlayer.SetBool("Movimiento", false);
            }
            else 
            {
                if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.estuneado) 
                {
                    animatorPlayer.SetBool("Movimiento", false);
                }
            }
        }
    }

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
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
                if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.atacando)
                {
                    animatorPlayer.SetBool("Atacando", true);
                    yield return null;

                    reproduceAudio(audioAtaqueEspada, velocidadAudioAtaqueEspada);
                    animatorPlayer.SetBool("Atacando", false);
                    yield return new WaitForSeconds(0.6f);

                    estadoPlayer.setEstadoActualObjeto(estadoGenerico.ninguno);
                }
            }
            else 
            {
                if (inventariopPlayerItems.objetoEquipado == arco) 
                { 
                    if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.atacando)
                    {
                        reproduceAudio(audioArrojaProyectil, velocidadAudioArrojaProyectil);
                        creaFlecha();
                        yield return new WaitForSeconds(0.5f);

                        estadoPlayer.setEstadoActualObjeto(estadoGenerico.ninguno);
                    }
                }
            }
        }
        
        
    }

    private void creaFlecha() 
    {
        if (magiaPlayer.valorFlotanteEjecucion > 0) 
        {
            Vector2 vectorTemporal = new Vector2(animatorPlayer.GetFloat("MovimientoX"), animatorPlayer.GetFloat("MovimientoY"));
            flecha flecha = Instantiate(proyectil, gameObject.transform.position, Quaternion.identity).GetComponent<flecha>();
            flecha.dispara(vectorTemporal, eligeDireccionFlecha());
            magiaPlayer.valorFlotanteEjecucion -= flecha.costoMagia;
            if (magiaPlayer.valorFlotanteEjecucion <= 0) 
            {
                magiaPlayer.valorFlotanteEjecucion = 0;
            }
            decrementaMagia.invocaFunciones();
        }
    }

    private Vector3 eligeDireccionFlecha() 
    {
        float direccionZ = Mathf.Atan2(animatorPlayer.GetFloat("MovimientoY"), animatorPlayer.GetFloat("MovimientoX")) * Mathf.Rad2Deg;
        return new Vector3(0,0, direccionZ);
    }

    public void comienzaEmpujaPlayer(float tiempoAplicarFuerza)
    {
        estadoPlayer.setEstadoActualObjeto(estadoGenerico.estuneado);
        StartCoroutine(empujaPlayer(tiempoAplicarFuerza));
    }

    private IEnumerator empujaPlayer(float tiempoAplicarFuerza)
    {
        if (rigidBodyPlayer != null)
        {
            reciveGolpePlayer.invocaFunciones();
            StartCoroutine(vidaPlayer.flash());
            yield return new WaitForSeconds(tiempoAplicarFuerza);

            rigidBodyPlayer.velocity = Vector2.zero;
            estadoPlayer.setEstadoActualObjeto(estadoGenerico.ninguno);
        }
    }

    public void muestrObjeto() 
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
            if (estadoPlayer.getEstadoActualObjeto() != estadoGenerico.interactuando)
            {
                animatorPlayer.SetBool("MostrandoObjeto", true);
                estadoPlayer.setEstadoActualObjeto(estadoGenerico.interactuando);
                spriteObjetoMostrar.sprite = itemMostrar.imagenItem;
                itemMostrar.mostrarItem = false;
            }
        }
        else 
        {
            if (estadoPlayer.getEstadoActualObjeto() == estadoGenerico.interactuando)
            {
                animatorPlayer.SetBool("MostrandoObjeto", false);
                estadoPlayer.setEstadoActualObjeto(estadoGenerico.ninguno);
                spriteObjetoMostrar.sprite = null;
            }
        }
    }
}