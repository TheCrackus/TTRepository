using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    caminando,
    atacando,
    interactuando,
    transicionando,
    ninguno,
    estuneado,
    inactivo
}

public class movimientoPlayer : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    private Vector3 vectorMovimiento;
    private Animator playerAnimator;
    private AnimationClip atacandoClip;
    public PlayerState estadoActualPlayer;
    [Header("Estadisticas Player")]
    public float velocidad;
    [Header("Evento para animar la pantalla al recivir golpe")]
    public evento reciveGolpePlayer;
    [Header("Variables globales del player")]
    public valorVectorial posicionPlayer;
    public SpriteRenderer spriteObjetoMostrar;
    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("Estado general de la escena")]
    public cambioEscena estadoCambioEscenas;
    [Header("Proyectil que dispara el arma actual a distancia")]
    public GameObject proyectil;
    [Header("Evento que decrementa la magia")]
    public evento decrementaMagia;
    [Header("La cantidad de magia que tiene el Player")]
    public valorFlotante magiaPlayer;
    [Header("Objeto que representa el arco")]
    public inventarioItem arco;
    public Color colorFlash;
    public Color colorNormal;
    public float tiempoFlash;
    public int numeroFlash;
    public Collider2D colisionTrigger;
    public SpriteRenderer spritePlayer;

    public void setEstadoActualPlayer(PlayerState nuevoEstado)
    {
        if (estadoActualPlayer != nuevoEstado)
        {
            estadoActualPlayer = nuevoEstado;
        }
    }

    public PlayerState getEstadoActualPlayer()
    {
        return estadoActualPlayer;
    }

    void Start()
    {
        if (estadoCambioEscenas.cambieEscenaEjecucion)
        {
            estadoActualPlayer = PlayerState.transicionando;
        }
        else 
        {
            estadoActualPlayer = PlayerState.ninguno;
        }
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        foreach (AnimationClip clip in playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Atacando Arriba")
            {
                atacandoClip = clip;
            }
        }
        if (estadoCambioEscenas.direccionPlayerEjecucion.x == 0)
        {
            if (estadoCambioEscenas.direccionPlayerEjecucion.y > 0)
            {
                playerAnimator.SetFloat("MovimientoX", 0f);
                playerAnimator.SetFloat("MovimientoY", 1f);
            }
            else
            {
                if (estadoCambioEscenas.direccionPlayerEjecucion.y < 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 0f);
                    playerAnimator.SetFloat("MovimientoY", -1f);
                }
            }
        }
        else 
        {
            if (estadoCambioEscenas.direccionPlayerEjecucion.y == 0)
            {
                if (estadoCambioEscenas.direccionPlayerEjecucion.x > 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 1f);
                    playerAnimator.SetFloat("MovimientoY", 0f);
                }
                else
                {
                    if (estadoCambioEscenas.direccionPlayerEjecucion.x < 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", -1f);
                        playerAnimator.SetFloat("MovimientoY", 0f);
                    }
                }
            }
        }
        gameObject.transform.position = posicionPlayer.valorVectorialEjecucion;
    }

    void Update()
    {
        if (Input.GetButtonDown("Atacar")
            && estadoActualPlayer != PlayerState.atacando
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.transicionando
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo
            && (estadoActualPlayer == PlayerState.caminando 
                || estadoActualPlayer == PlayerState.ninguno))
        {
            estadoActualPlayer = PlayerState.atacando;
            StartCoroutine(Atacar());
        }
        else 
        {
            if (Input.GetButtonDown("Atacar 2")
                && estadoActualPlayer != PlayerState.atacando
                && estadoActualPlayer != PlayerState.interactuando
                && estadoActualPlayer != PlayerState.transicionando
                && estadoActualPlayer != PlayerState.estuneado
                && estadoActualPlayer != PlayerState.inactivo
                && (estadoActualPlayer == PlayerState.caminando 
                    || estadoActualPlayer == PlayerState.ninguno)
                && inventariopPlayerItems.verififcaItem(arco)
                && magiaPlayer.valorFlotanteEjecucion > 0)
            {
                estadoActualPlayer = PlayerState.atacando;
                StartCoroutine(Atacar2());
            }
        }
    }

    void FixedUpdate()
    {
        if (playerRigidBody.velocity != Vector2.zero
            && estadoActualPlayer != PlayerState.caminando
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.atacando
            && (estadoActualPlayer == PlayerState.ninguno
                || estadoActualPlayer == PlayerState.interactuando
                || estadoActualPlayer == PlayerState.transicionando
                || estadoActualPlayer == PlayerState.inactivo)
            )
        {
            playerAnimator.SetBool("Movimiento", false);
            playerRigidBody.velocity = Vector2.zero;
        }
        else 
        {
            vectorMovimiento = Vector3.zero;
            vectorMovimiento.x = Input.GetAxisRaw("Horizontal");
            vectorMovimiento.y = Input.GetAxisRaw("Vertical");
            if (vectorMovimiento != Vector3.zero)
            {
                if (estadoActualPlayer != PlayerState.caminando
                    && estadoActualPlayer != PlayerState.ninguno
                    && (estadoActualPlayer == PlayerState.atacando
                        || estadoActualPlayer == PlayerState.inactivo
                        || estadoActualPlayer == PlayerState.interactuando
                        || estadoActualPlayer == PlayerState.transicionando
                        || estadoActualPlayer == PlayerState.estuneado))
                {
                    playerAnimator.SetBool("Movimiento", false);
                }
                else 
                {
                    if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
                    {
                        vectorMovimiento.y = 0;
                    }
                    else
                    {
                        vectorMovimiento.x = 0;
                    }
                    if (estadoActualPlayer == PlayerState.ninguno
                        && estadoActualPlayer != PlayerState.atacando
                        && estadoActualPlayer != PlayerState.interactuando
                        && estadoActualPlayer != PlayerState.transicionando
                        && estadoActualPlayer != PlayerState.caminando
                        && estadoActualPlayer != PlayerState.estuneado
                        && estadoActualPlayer != PlayerState.inactivo)
                    {
                        estadoActualPlayer = PlayerState.caminando;
                    }
                    else
                    {
                        if (estadoActualPlayer == PlayerState.caminando
                            && estadoActualPlayer != PlayerState.atacando
                            && estadoActualPlayer != PlayerState.interactuando
                            && estadoActualPlayer != PlayerState.transicionando
                            && estadoActualPlayer != PlayerState.ninguno
                            && estadoActualPlayer != PlayerState.estuneado
                            && estadoActualPlayer != PlayerState.inactivo)
                        {
                            ActualizarMovimiento();
                        }
                    }
                }
            }
            else 
            {
                if (estadoActualPlayer != PlayerState.atacando
                    && estadoActualPlayer != PlayerState.inactivo
                    && estadoActualPlayer != PlayerState.interactuando
                    && estadoActualPlayer != PlayerState.transicionando
                    && estadoActualPlayer != PlayerState.estuneado
                    && (estadoActualPlayer == PlayerState.caminando
                        || estadoActualPlayer == PlayerState.ninguno))
                {
                    playerAnimator.SetBool("Movimiento", false);
                    estadoActualPlayer = PlayerState.ninguno;
                }
            }
        }
    }

    private void ActualizarMovimiento()
    {
        vectorMovimiento.Normalize();
        playerRigidBody.MovePosition(transform.position + vectorMovimiento * velocidad * Time.fixedDeltaTime);
        playerAnimator.SetFloat("MovimientoX", vectorMovimiento.x);
        playerAnimator.SetFloat("MovimientoY", vectorMovimiento.y);
        playerAnimator.SetBool("Movimiento", true);
    }

    private IEnumerator Atacar()
    {
        playerAnimator.SetBool("Atacando", true);
        yield return null;

        playerAnimator.SetBool("Atacando", false);
        yield return new WaitForSeconds(atacandoClip.length);

        if (estadoActualPlayer != PlayerState.caminando
            && estadoActualPlayer == PlayerState.atacando
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.transicionando
            && estadoActualPlayer != PlayerState.ninguno
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo)
        {
            estadoActualPlayer = PlayerState.ninguno;
        }
    }

    private IEnumerator Atacar2()
    {
        creaFlecha();
        yield return new WaitForSeconds(0.5f);
        if (estadoActualPlayer != PlayerState.caminando
            && estadoActualPlayer == PlayerState.atacando
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.transicionando
            && estadoActualPlayer != PlayerState.ninguno
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo)
        {
            estadoActualPlayer = PlayerState.ninguno;
        }
    }

    private void creaFlecha() 
    {
        if (magiaPlayer.valorFlotanteEjecucion > 0) 
        {
            Vector2 vectorTemporal = new Vector2(playerAnimator.GetFloat("MovimientoX"), playerAnimator.GetFloat("MovimientoY"));
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
        float direccionZ = Mathf.Atan2(playerAnimator.GetFloat("MovimientoY"), playerAnimator.GetFloat("MovimientoX")) * Mathf.Rad2Deg;
        return new Vector3(0,0, direccionZ);
    }

    public void comienzaEmpujaPlayer(float tiempoAplicarFuerza)
    {
        estadoActualPlayer = PlayerState.estuneado;
        StartCoroutine(empujaPlayer(tiempoAplicarFuerza));
    }

    private IEnumerator empujaPlayer(float tiempoAplicarFuerza)
    {
        if (playerRigidBody != null)
        {
            reciveGolpePlayer.invocaFunciones();
            StartCoroutine(flash());
            yield return new WaitForSeconds(tiempoAplicarFuerza);

            playerRigidBody.velocity = Vector2.zero;
            estadoActualPlayer = PlayerState.ninguno;
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
            if (estadoActualPlayer != PlayerState.interactuando)
            {
                playerAnimator.SetBool("MostrandoObjeto", true);
                estadoActualPlayer = PlayerState.interactuando;
                spriteObjetoMostrar.sprite = itemMostrar.imagenItem;
                itemMostrar.mostrarItem = false;
            }
        }
        else 
        {
            if (estadoActualPlayer == PlayerState.interactuando)
            {
                playerAnimator.SetBool("MostrandoObjeto", false);
                estadoActualPlayer = PlayerState.ninguno;
                spriteObjetoMostrar.sprite = null;
            }
        }
    }

    private IEnumerator flash() 
    {
        int numeroFlashTemporal = 0;
        colisionTrigger.enabled = false;
        while (numeroFlashTemporal < numeroFlash) 
        {
            spritePlayer.color = colorFlash;
            yield return new WaitForSeconds(tiempoFlash);
            spritePlayer.color = colorNormal;
            yield return new WaitForSeconds(tiempoFlash);
            numeroFlashTemporal++;
        }
        colisionTrigger.enabled = true;
    }
}