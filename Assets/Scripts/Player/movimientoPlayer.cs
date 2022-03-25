using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    caminando,
    atacando,
    interactuando,
    ninguno,
    estuneado
}

public class movimientoPlayer : MonoBehaviour
{
    private PlayerState estadoActualPlayer;
    public float velocidad;
    private Rigidbody2D playerRigidBody;
    private Vector3 vectorMovimiento;
    private Animator playerAnimator;
    private AnimationClip atacandoArribaClip;


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
        if (Input.GetButtonDown("Fire1") && estadoActualPlayer != PlayerState.atacando && estadoActualPlayer != PlayerState.ninguno)
        {
            StartCoroutine(Atacar());
        }
    }

    void FixedUpdate()
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
        if (estadoActualPlayer == PlayerState.caminando)
        {
            ActualizarMovimiento();
        }
        else
        {
            if (estadoActualPlayer == PlayerState.ninguno)
            {
                playerAnimator.SetBool("Movimiento", false);
            }
        }
    }

    public void activarPlayerEstuneado() 
    {
        estadoActualPlayer = PlayerState.estuneado;
    }

    private IEnumerator Atacar()
    {
        estadoActualPlayer = PlayerState.atacando;
        playerAnimator.SetBool("Atacando", true);
        yield return null;

        playerAnimator.SetBool("Atacando", false);
        yield return new WaitForSeconds(atacandoArribaClip.length);

        estadoActualPlayer = PlayerState.caminando;
    }

    private void ActualizarMovimiento()
    {
        if (vectorMovimiento != Vector3.zero)
        {
            Movimiento();
            playerAnimator.SetFloat("MovimientoX", vectorMovimiento.x);
            playerAnimator.SetFloat("MovimientoY", vectorMovimiento.y);
            playerAnimator.SetBool("Movimiento", true);
        }
        else
        {
            playerAnimator.SetBool("Movimiento", false);
        }
    }

    private void Movimiento()
    {
        vectorMovimiento.Normalize();
        playerRigidBody.MovePosition(transform.position + vectorMovimiento * velocidad * Time.fixedDeltaTime);
    }

    public void cambiaPermiteMovimientoPositivo()
    {
        estadoActualPlayer = PlayerState.caminando;
    }

    public void cambiaPermiteMovimientoNegativo()
    {
        estadoActualPlayer = PlayerState.ninguno;
    }

    public void empuja(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        StartCoroutine(empujaPlayer(rigidBodyAfectado, tiempoAplicarFuerza));
    }

    private IEnumerator empujaPlayer(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);

            rigidBodyAfectado.velocity = Vector2.zero;
            estadoActualPlayer = PlayerState.caminando;
        }
    }
}