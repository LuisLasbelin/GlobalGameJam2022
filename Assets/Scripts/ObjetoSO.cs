using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objeto", menuName = "TimeBox/Objeto", order = 1)]
public class ObjetoSO : ScriptableObject
{
    public string nombre;
    public List<Sprite> estados;
    public int estadoActual = 0;
    public tipoObjetoEnum tipoObjeto;
    public objetoEnum objeto;
    public tipoUsoEnum tipoUso;

    public enum tipoObjetoEnum {
        recoger,
        uso,
        dialogo,
        portal
    };

    public enum tipoUsoEnum {
        consumible,
        cambioEstado,
        estatico
    }

    public enum objetoEnum {
        cubo,
        pinzas,
        fuego,
        casillero, 
        palanca,
        tarjeta,
        lector,
        caja,
        dispensador_agua,
        caja_vieja,
        caja_nueva,
        Ninguno
    }

    public bool activable;
    public objetoEnum objetoNecesario;
    public string dialogo;
    public GameObject contenidoSpawn;
    public bool tiene_usos;
    public int numero_de_usos;
}