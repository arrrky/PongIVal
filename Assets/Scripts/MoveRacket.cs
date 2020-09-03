using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRacket : MonoBehaviour
{  
    float racketSpeed = 30F;
    [SerializeField]
    string axis = "Vertical";   

    private void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, vertical) * racketSpeed;
    }
}
