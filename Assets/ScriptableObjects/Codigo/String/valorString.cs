using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo String", menuName = "Valores/String")]
[System.Serializable]
public class ValorString : ScriptableObject
{

    public string valorStringInicial;

    public string valorStringEjecucion;

    public void reiniciarValores()
    {
        valorStringEjecucion = valorStringInicial;
    }

}
