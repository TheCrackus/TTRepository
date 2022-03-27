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
                && getEstadoActualEnemigo() != EnemyState.atacando
                && getEstadoActualEnemigo() != EnemyState.inactivo
                && (getEstadoActualEnemigo() == EnemyState.caminando || getEstadoActualEnemigo() == EnemyState.durmiendo || getEstadoActualEnemigo() == EnemyState.ninguno))
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, objetivoPerseguir.position, velocidadMovimientoEnemigo * Time.deltaTime);
                Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                enemigoRigidBody.MovePosition(transform.position + vectorMovimiento * velocidadMovimientoEnemigo * Time.fixedDeltaTime);
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

    private Vector2 cambiaAnimaciones(Vector2 vectorMovimiento) 
    {
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            if (vectorMovimiento.x > 0)
            {
                enviaAnimacion(Vector2.right);
                return Vector2.right;
            }
            else 
            {
                if (vectorMovimiento.x < 0) 
                {
                    enviaAnimacion(Vector2.left);
                    return Vector2.left;
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
                    return Vector2.up;
                }
                else
                {
                    if (vectorMovimiento.y < 0)
                    {
                        enviaAnimacion(Vector2.down);
                        return Vector2.down;
                    }
                }
            }
        }
        return new Vector2(0,0);
    }
}