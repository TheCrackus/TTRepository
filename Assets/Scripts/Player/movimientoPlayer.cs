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
        if (permiteMover)
        {
            ActualizarMovimiento();
        }
        else 
        {
            playerAnimator.SetBool("Movimiento", false);
        }
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
