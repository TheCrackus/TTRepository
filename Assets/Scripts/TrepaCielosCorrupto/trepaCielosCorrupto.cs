using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorrupto : enemigo
{

    private Rigidbody2D enemigoRigidBody;
    private Transform objetivoPerseguir;
    public float radioPersecucion;
    public float radioAtaque;
    public Transform posicionOriginal;
    private Animator trepaCielosAnimator;

    // Start is called before the first frame update
    void Start()
    {
        setEstadoActualEnemigo(EnemyState.durmiendo);
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = GetComponent<Rigidbody2D>();
        trepaCielosAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        gestionDistancias();
    }

    private void gestionDistancias()
    {
        if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) >= radioAtaque)
        {
            if (getEstadoActualEnemigo() != EnemyState.estuneado
                && (getEstadoActualEnemigo() == EnemyState.caminando || getEstadoActualEnemigo() == EnemyState.durmiendo || getEstadoActualEnemigo() == EnemyState.ninguno)
                && getEstadoActualEnemigo() != EnemyState.atacando)
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, objetivoPerseguir.position, velocidadMovimientoEnemigo * Time.deltaTime);
                Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                cambiaAnimaciones(refAnimacion);
                enemigoRigidBody.MovePosition(vectorTemporal);
                setEstadoActualEnemigo(EnemyState.caminando);
                trepaCielosAnimator.SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) > radioPersecucion) 
            {
                trepaCielosAnimator.SetBool("Despertar", false);
                setEstadoActualEnemigo(EnemyState.durmiendo);
            }
        }
    }

    private void enviaAnimacion(Vector2 vector) 
    {
        trepaCielosAnimator.SetFloat("MoviminetoX", vector.x);
        trepaCielosAnimator.SetFloat("MovimientoY", vector.y);
    }

    private void cambiaAnimaciones(Vector2 vectorMovimiento) 
    {
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            if (vectorMovimiento.x > 0)
            {
                enviaAnimacion(Vector2.right);
            }
            else 
            {
                if (vectorMovimiento.x < 0) 
                {
                    enviaAnimacion(Vector2.left);
                }
            }
        }
        else
        {
            if (Mathf.Abs(vectorMovimiento.x) < Mathf.Abs(vectorMovimiento.y))
            {
                if (vectorMovimiento.y > 0)
                {
                    enviaAnimacion(Vector2.up);
                }
                else
                {
                    if (vectorMovimiento.y < 0)
                    {
                        enviaAnimacion(Vector2.down);
                    }
                }
            }
        }
    }
}