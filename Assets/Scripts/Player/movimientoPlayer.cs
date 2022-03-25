using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPlayer : MonoBehaviour
{

    public float velocidad;
    private Rigidbody2D playerRigidBody;
    private Vector3 vectorMovimiento;
    private Animator playerAnimator;
    private bool permiteMover;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        permiteMover = true;
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
<<<<<<< HEAD
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            vectorMovimiento.y = 0;
        }
        else
        {
            vectorMovimiento.x = 0;
        }
        if (estadoActualPlayer == PlayerState.caminando)
=======
        if (permiteMover)
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
        {
            ActualizarMovimiento();
        }
        else 
        {
<<<<<<< HEAD
            if (estadoActualPlayer == PlayerState.ninguno) 
            {
                playerAnimator.SetBool("Movimiento", false);
            }
        }
    }

    private IEnumerator Atacar() 
    {
        estadoActualPlayer = PlayerState.atacando;
        playerAnimator.SetBool("Atacando", true);
        yield return null;

        playerAnimator.SetBool("Atacando", false);
        yield return new WaitForSeconds(atacandoArribaClip.length);

        estadoActualPlayer = PlayerState.caminando;
=======
            playerAnimator.SetBool("Movimiento", false);
        }
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
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
        playerRigidBody.MovePosition(transform.position + vectorMovimiento * velocidad * Time.fixedDeltaTime);
    }

    public void cambiaPermiteMovimientoPositivo() 
    {
        permiteMover = true;
    }

    public void cambiaPermiteMovimientoNegativo()
    {
        permiteMover = false;
    }
}
