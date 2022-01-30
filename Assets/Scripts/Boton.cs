using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public ScenesLoader scenesLoader;
    private void OnMouseDown() {
        scenesLoader.VolverMenu();
    }
}
