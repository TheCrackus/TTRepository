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

    // Start is called before the first frame update
    void Start()
    {
        activarEnemigoNinguno();
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = GetComponent<Rigidbody2D>();
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
        if (Vector3.Distance(objetivoPerseguir.position, transform.position) <= radioPersecucion
            && Vector3.Distance(objetivoPerseguir.position, transform.position) >= radioAtaque
            && (getEstadoActualEnemigo() == EnemyState.ninguno || getEstadoActualEnemigo() == EnemyState.caminando)
            && getEstadoActualEnemigo() != EnemyState.estuneado)
        {
            Vector3 vectorTemporal = transform.position = Vector3.MoveTowards(transform.position, objetivoPerseguir.position, velocidadMovimientoEnemigo * Time.deltaTime);
            enemigoRigidBody.MovePosition(vectorTemporal);
            cambiarEstado(EnemyState.caminando);
        }
    }
}