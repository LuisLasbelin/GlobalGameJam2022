using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string inventario = "";
    public GameObject personaje;

    public void recogerObjeto(GameObject objeto)
    {
        if(inventario == "")
        {
            inventario = objeto.name;
            
            objeto.GetComponent<SpriteRenderer>().enabled = false;
            objeto.GetComponent<ObjetoInteraccion>().enabled = false;

        }
    }

    public void soltarObjeto()
    {
        if (inventario != null)
        {
            GameObject objeto =  GameObject.Find(inventario);
            objeto.transform.position = personaje.transform.position;
            objeto.GetComponent<SpriteRenderer>().enabled = true;
            objeto.GetComponent<ObjetoInteraccion>().enabled = true;
         
            inventario = "";
        }
    }
}
