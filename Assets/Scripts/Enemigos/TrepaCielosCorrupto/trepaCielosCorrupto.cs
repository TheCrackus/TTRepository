using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorrupto : enemigo
{

    private Rigidbody2D enemigoRigidBody;
    private Transform objetivoPerseguir;
    private Animator enemigoAnimator;
    [Header("Distancia de persecucion")]
    public float radioPersecucion;
    [Header("Distancia de ataque")]
    public float radioAtaque;

    public void setEnemigoRigidBody(Rigidbody2D enemigoRigidBody) 
    {
        this.enemigoRigidBody = enemigoRigidBody;
    }

    public Rigidbody2D getEnemigoRigidBody() 
    {
        return enemigoRigidBody;
    }

    public void setEnemigoAnimator(Animator enemigoAnimator) 
    {
        this.enemigoAnimator = enemigoAnimator;
    }

    public Animator getEnemigoAnimator() 
    {
        return enemigoAnimator;
    }

    public void setObjetivoPerseguir(Transform objetivoPerseguir) 
    {
        this.objetivoPerseguir = objetivoPerseguir;
    }

    public Transform getObjetivoPerseguir() 
    {
        return objetivoPerseguir;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        setEstadoEnemigo(estadoGenerico.durmiendo);
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = GetComponent<Rigidbody2D>();
        enemigoAnimator = GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public override void Start()
    {
        base.Start();
        setEstadoEnemigo(estadoGenerico.ninguno);
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = gameObject.GetComponent<Rigidbody2D>();
        enemigoAnimator = gameObject.GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public virtual void FixedUpdate()
    {
        if (getPuedoMoverme())
        {
            gestionDistancias();
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else 
        {
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public virtual void gestionDistancias()
    {
        if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) >= radioAtaque)
        {
            if (getEstadoEnemigo() == estadoGenerico.caminando 
                || getEstadoEnemigo() == estadoGenerico.durmiendo 
                || getEstadoEnemigo() == estadoGenerico.ninguno)
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, objetivoPerseguir.position, getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                enemigoRigidBody.MovePosition(transform.position + vectorMovimiento * getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                setEstadoEnemigo(estadoGenerico.caminando);
                enemigoAnimator.SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) > radioPersecucion) 
            {
                enemigoAnimator.SetBool("Despertar", false);
                setEstadoEnemigo(estadoGenerico.durmiendo);
            }
        }
    }

    private void enviaAnimacion(Vector2 vector) 
    {
        enemigoAnimator.SetFloat("MoviminetoX", vector.x);
        enemigoAnimator.SetFloat("MovimientoY", vector.y);
    }

    public Vector2 cambiaAnimaciones(Vector2 vectorMovimiento) 
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