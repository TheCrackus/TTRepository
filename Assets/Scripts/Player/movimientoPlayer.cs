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
        vectorMovimiento = Vector3.zero;
        vectorMovimiento.x = Input.GetAxisRaw("Horizontal");
        vectorMovimiento.y = Input.GetAxisRaw("Vertical");
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            vectorMovimiento.y = 0;
        }
        else 
        {
            vectorMovimiento.x = 0;
        }
<<<<<<< HEAD
        if (estadoActualPlayer == PlayerState.caminando)
=======
        if (permiteMover)
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
=======
        if (permiteMover)
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
=======
        if (permiteMover)
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
=======
        if (Input.GetButtonDown("Fire1") && estadoActualPlayer != PlayerState.atacando && estadoActualPlayer != PlayerState.ninguno)
>>>>>>> parent of 481e338 (Cambios mal echos)
        {
            StartCoroutine(Atacar());
        }
        else 
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            if (estadoActualPlayer == PlayerState.ninguno) 
=======
            if (estadoActualPlayer == PlayerState.caminando && estadoActualPlayer != PlayerState.atacando)
            {
                ActualizarMovimiento();
            }
            else
>>>>>>> parent of 481e338 (Cambios mal echos)
            {
                playerAnimator.SetBool("Movimiento", false);
            }
        }
        
    }

    void FixedUpdate() 
    {
        
    }

    private IEnumerator Atacar() 
    {
        playerAnimator.SetBool("Movimiento", false);
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
=======
            playerAnimator.SetBool("Movimiento", false);
        }
>>>>>>> parent of 177bf19 (Jarron rompible, correciones al movimiento del jugador y ataque de espada basico e interacciones)
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
