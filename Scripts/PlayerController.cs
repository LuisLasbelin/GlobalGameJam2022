using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager input;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Movimiento
        Vector3 movimiento = new Vector2(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));

        transform.position += movimiento * Time.deltaTime * speed;
    }
}
