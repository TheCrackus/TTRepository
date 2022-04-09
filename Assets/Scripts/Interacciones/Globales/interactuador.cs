using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactuador : MonoBehaviour
{

    public evento simboloActivoDesactivo;
    private bool playerEnRango;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerEnRango(bool playerEnRango) {
        this.playerEnRango = playerEnRango;
    }

    public bool getPlayerEnRango()
    {
        return playerEnRango;
    }
}
