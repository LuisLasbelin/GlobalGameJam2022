using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public Dialogos dialogos;

    Queue<string> oraciones;

    public GameObject bocadillo;
    public TextMeshProUGUI displayText;

    string fraseActiva;
    public float velocidadDeTexto;

    //AudioSource audio;
    //public AudioClip ruidoHablar;


    // Start is called before the first frame update
    void Start()
    {
        oraciones = new Queue<string>();
        //audio = GetComponent<AudioSource>();
    }

    public void EmpezarDialogo(GameManager manager)
    {

        bocadillo = manager.bocadillo;
        displayText = manager.displayText;
        oraciones.Clear();

        foreach (string oracion in dialogos.listaDeOraciones)
        {
            oraciones.Enqueue(oracion);
        }
        mostrarOracion();
    }

    public void mostrarOracion()
    {
        if (oraciones.Count <= 0)
        {
            displayText.text = fraseActiva;
            return;
        }

        fraseActiva = oraciones.Dequeue();
        displayText.text = fraseActiva;
        Debug.Log(fraseActiva);
    }
}
