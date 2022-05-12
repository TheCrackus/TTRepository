using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFisico : MonoBehaviour
{
    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("El item a agregar al inventario")]
    [SerializeField] private inventarioItem itemAgrgarInventario;
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al recojer item")]
    [SerializeField] private AudioSource audioRecojer;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRecojer;

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

    void agregaItemInventario() 
    {
        if (inventariopPlayerItems && itemAgrgarInventario) 
        {
            if (inventariopPlayerItems.inventario.Contains(itemAgrgarInventario))
            {
                itemAgrgarInventario.cantidadItem += 1;
            }
            else 
            {
                inventariopPlayerItems.inventario.Add(itemAgrgarInventario);
                itemAgrgarInventario.cantidadItem += 1;
            }
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            reproduceAudio(audioRecojer, velocidadAudioRecojer);
            agregaItemInventario();
            Destroy(gameObject);
        }
    }
}
