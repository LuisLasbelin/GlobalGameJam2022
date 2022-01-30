using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogos
{
    public string nombre;

    [TextArea(3, 10)]
    public string[] listaDeOraciones;
}

