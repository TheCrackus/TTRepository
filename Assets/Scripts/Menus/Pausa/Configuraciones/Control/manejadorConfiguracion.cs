using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manejadorConfiguracion : manejadorMenu
{

    [Header("Componentes graficos del menu de configuraciones")]
    [SerializeField] private componentesGraficosConfiguracion graficosConfiguracion;

    public componentesGraficosConfiguracion GraficosConfiguracion { get => graficosConfiguracion; set => graficosConfiguracion = value; }

}
