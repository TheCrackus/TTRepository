using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorMenuTutorial : ManejadorMenuGenerico, IReproductorAudioInterfazGrafica, IPausa, IBotonPulso
{

    private ComponenteGraficoMenuTutorial graficos;

    private bool condicionPausa;

    private bool pulseBoton;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Imagenes a mostrar en el tutorial")]
    [SerializeField] private Sprite[] spritesTutorial;

    public bool CondicionPausa { get => condicionPausa; set => condicionPausa = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

    private void OnEnable()
    {
        reproducirAudioAbreVentana();
        reiniciarBotones();
        pausarJuego();
    }

    private void Start()
    {
        graficos = (ComponenteGraficoMenuTutorial)ComponenteGrafico;
        graficos.ImagenTutorial.sprite = spritesTutorial[0];
        graficos.ImagenTutorial.preserveAspect = true;
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproducirAudioClickCerrar();
        continuarJuego();
    }

    public bool verificarImagenes() 
    {
        if (spritesTutorial != null
                && spritesTutorial.Length > 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void cambiarImagenAdelante() 
    {
        if (verificarImagenes())
        {
            int indexSpriteActual = 0;
            for (int i = 0; i < spritesTutorial.Length; i++)
            {
                if (graficos.ImagenTutorial.sprite == spritesTutorial[i])
                {
                    indexSpriteActual = i;
                    break;
                }
            }
            if (indexSpriteActual < (spritesTutorial.Length - 1))
            {
                graficos.ImagenTutorial.sprite = spritesTutorial[indexSpriteActual + 1];
                graficos.ImagenTutorial.preserveAspect = true;
            }
        }
        reiniciarBotones();
    }

    public void cambiarImagenAtras()
    {
        if (verificarImagenes())
        {
            int indexSpriteActual = 0;
            for (int i = 0; i < spritesTutorial.Length; i++)
            {
                if (graficos.ImagenTutorial.sprite == spritesTutorial[i])
                {
                    indexSpriteActual = i;
                    break;
                }
            }
            if (indexSpriteActual > 0)
            {
                graficos.ImagenTutorial.sprite = spritesTutorial[indexSpriteActual - 1];
                graficos.ImagenTutorial.preserveAspect = true;
            }
        }
        reiniciarBotones();
    }

    public void avanzarImagenBoton() 
    {
        if (!pulseBoton)
        {
            reproducirAudioClickAbrir();
            bloquearBotones();
            cambiarImagenAdelante();
        }
    }

    public void retrocederImagenBoton() 
    {
        if (!pulseBoton)
        {
            reproducirAudioClickAbrir();
            bloquearBotones();
            cambiarImagenAtras();
        }
    }

    public void cerrarBoton()
    {
        if (!pulseBoton)
        {
            reproducirAudioClickCerrar();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void pausarJuego()
    {
        Time.timeScale = 0f;
    }

    public void continuarJuego()
    {
        Time.timeScale = 1f;
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioAbrirVentana();
        }
    }

    public void reiniciarBotones()
    {
        pulseBoton = false;
    }

    public void bloquearBotones()
    {
        pulseBoton = true;
    }

}
