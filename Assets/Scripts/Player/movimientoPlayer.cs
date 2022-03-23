using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState 
{
    caminando,
    atacando,
    interactuando,
    ninguno
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
        if (Input.GetButtonDown("Fire1") && estadoActualPlayer != PlayerState.atacando && estadoActualPlayer != PlayerState.ninguno)
        {
            StartCoroutine(Atacar());
        }
        else 
        {
            if (estadoActualPlayer == PlayerState.caminando && estadoActualPlayer != PlayerState.atacando)
            {
                ActualizarMovimiento();
            }
            else
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
}
