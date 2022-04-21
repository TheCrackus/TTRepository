using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorrupto : enemigo
{

    private Rigidbody2D enemigoRigidBody;
    private Transform objetivoPerseguir;
    private GameObject player;
    private Animator enemigoAnimator;
    [Header("Distancia de persecucion")]
    public float radioPersecucion;
    [Header("Distancia de ataque")]
    public float radioAtaque;

    // Start is called before the first frame update
    void Start()
    {
        setEstadoActualEnemigo(EnemyState.durmiendo);
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindWithTag("Player");
        enemigoRigidBody = GetComponent<Rigidbody2D>();
        enemigoAnimator = GetComponent<Animator>();
        getEnemigoAnimator().SetBool("Despertar", true);
    }

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

    public void setPlayer(GameObject player) 
    {
        this.player = player;
    }

    public GameObject getPlayer() 
    {
        return player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        gestionDistancias();
    }

    public virtual void gestionDistancias()
    {
        if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) >= radioAtaque)
        {
            PlayerState estadoPlayer = player.GetComponent<movimientoPlayer>().getEstadoActualPlayer();
            if (getEstadoActualEnemigo() != EnemyState.estuneado
                && getEstadoActualEnemigo() != EnemyState.atacando
                && getEstadoActualEnemigo() != EnemyState.inactivo
                && (getEstadoActualEnemigo() == EnemyState.caminando 
                    || getEstadoActualEnemigo() == EnemyState.durmiendo 
                    || getEstadoActualEnemigo() == EnemyState.ninguno)
                && estadoPlayer != PlayerState.estuneado
                && estadoPlayer != PlayerState.inactivo
                && estadoPlayer != PlayerState.interactuando
                && (estadoPlayer == PlayerState.caminando
                    || estadoPlayer == PlayerState.atacando
                    || estadoPlayer == PlayerState.ninguno))
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, objetivoPerseguir.position, velocidadMovimientoEnemigo * Time.deltaTime);
                Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                enemigoRigidBody.MovePosition(transform.position + vectorMovimiento * velocidadMovimientoEnemigo * Time.fixedDeltaTime);
                setEstadoActualEnemigo(EnemyState.caminando);
                enemigoAnimator.SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) > radioPersecucion) 
            {
                enemigoAnimator.SetBool("Despertar", false);
                setEstadoActualEnemigo(EnemyState.durmiendo);
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