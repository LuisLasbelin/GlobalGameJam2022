using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objeto", menuName = "TimeBox/Objeto", order = 1)]
public class ObjetoSO : ScriptableObject
{
    public string nombre;
    /* Estados
    0: default futuro
    1: default pasado
    2: activado futuro
    3: activado pasado
    */
    public List<Sprite> estados;

    public tipoObjetoEnum tipoObjeto = tipoObjetoEnum.recoger;

    public enum tipoObjetoEnum {
        recoger,
        activar,
        dialogo
    };

    public bool activable;
    public string objetoNecesario;
}