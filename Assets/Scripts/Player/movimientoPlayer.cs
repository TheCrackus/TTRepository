using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    caminando,
    atacando,
    interactuando,
    ninguno,
    estuneado,
    inactivo
}

public class movimientoPlayer : MonoBehaviour
{
    private PlayerState estadoActualPlayer;
    public float velocidad;
    private Rigidbody2D playerRigidBody;
    private Vector3 vectorMovimiento;
    private Animator playerAnimator;
    private AnimationClip atacandoArribaClip;
    public valorFlotante vidaActual;
    public evento eventoVidaJugador;

    // Start is called before the first frame update
    void Start()
    {
        estadoActualPlayer = PlayerState.caminando;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        foreach (AnimationClip clip in playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Atacando Arriba")
            {
                atacandoArribaClip = clip;
            }
        }
        playerAnimator.SetFloat("MovimientoX", 0f);
        playerAnimator.SetFloat("MovimientoY", -1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && estadoActualPlayer != PlayerState.atacando 
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo
            && (estadoActualPlayer == PlayerState.caminando || estadoActualPlayer == PlayerState.ninguno))
        {
            estadoActualPlayer = PlayerState.atacando;
            StartCoroutine(Atacar());
        }
    }

    void FixedUpdate()
    {
        vectorMovimiento = Vector3.zero;
        vectorMovimiento.x = Input.GetAxisRaw("Horizontal");
        vectorMovimiento.y = Input.GetAxisRaw("Vertical");

        if (vectorMovimiento != Vector3.zero)
        {
            if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
            {
                vectorMovimiento.y = 0;
            }
            else
            {
                vectorMovimiento.x = 0;
            }
            if (estadoActualPlayer == PlayerState.caminando
            && estadoActualPlayer != PlayerState.atacando
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.ninguno
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo)
            {
                ActualizarMovimiento();
            }
            else
            {
                if (estadoActualPlayer == PlayerState.ninguno
                    && estadoActualPlayer != PlayerState.atacando
                    && estadoActualPlayer != PlayerState.interactuando
                    && estadoActualPlayer != PlayerState.caminando
                    && estadoActualPlayer != PlayerState.estuneado
                    && estadoActualPlayer != PlayerState.inactivo)
                {
                    estadoActualPlayer = PlayerState.caminando;
                }
                else 
                {
                    if (estadoActualPlayer != PlayerState.caminando
                        && estadoActualPlayer != PlayerState.inactivo
                        && (estadoActualPlayer == PlayerState.atacando || estadoActualPlayer == PlayerState.interactuando
                            || estadoActualPlayer == PlayerState.ninguno || estadoActualPlayer == PlayerState.estuneado))
                    {
                        playerAnimator.SetBool("Movimiento", false);
                    }
                }
            }
        }
        else 
        {
            playerAnimator.SetBool("Movimiento", false);
        }
    }

    private IEnumerator Atacar()
    {
        playerAnimator.SetBool("Atacando", true);
        yield return null;

        playerAnimator.SetBool("Atacando", false);
        yield return new WaitForSeconds(atacandoArribaClip.length);

        estadoActualPlayer = PlayerState.ninguno;
    }

    private void ActualizarMovimiento()
    {
        vectorMovimiento.Normalize();
        playerRigidBody.MovePosition(transform.position + vectorMovimiento * velocidad * Time.fixedDeltaTime);
        playerAnimator.SetFloat("MovimientoX", vectorMovimiento.x);
        playerAnimator.SetFloat("MovimientoY", vectorMovimiento.y);
        playerAnimator.SetBool("Movimiento", true);
    }

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

    public void empuja(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza, float vidaMenos)
    {
        tomaMenosVida(vidaMenos);
        eventoVidaJugador.altaEvento();
        if (estadoActualPlayer != PlayerState.atacando
            && estadoActualPlayer != PlayerState.interactuando
            && estadoActualPlayer != PlayerState.estuneado
            && estadoActualPlayer != PlayerState.inactivo
            && (estadoActualPlayer == PlayerState.caminando
                || estadoActualPlayer == PlayerState.ninguno))
        {
            estadoActualPlayer = PlayerState.estuneado;
            StartCoroutine(empujaPlayer(rigidBodyAfectado, tiempoAplicarFuerza));
        }
    }

    private void tomaMenosVida(float vidaMenos)
    {
        vidaActual.valorEjecucion -= vidaMenos;
        if (vidaActual.valorEjecucion <= 0)
        {
            estadoActualPlayer = PlayerState.inactivo;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator empujaPlayer(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);

            rigidBodyAfectado.velocity = Vector2.zero;
            estadoActualPlayer = PlayerState.ninguno;
        }
    }
}