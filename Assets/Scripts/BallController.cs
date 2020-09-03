using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    public readonly float ballSpeed = 30; // сделать скорость возрастающей!!!
    public RoundController RoundController;    
   
    void Start()
    {
        // Задаем начальную скорость мячику
        GetComponent<Rigidbody2D>().velocity = Vector2.right * ballSpeed;        
    }

    // Определяем куда попал мячик и возвращаем:
    // 0 - попал в центр, 1 - попал в верхнюю часть ракетки, -1 - в нижнюю
    private float GetRacketHitLocation(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<AudioSource>().Play();
        float hitLocation = GetRacketHitLocation(transform.position, collision.transform.position, collision.collider.bounds.size.y);

        switch (collision.gameObject.name)
        {
            case "RacketLeft":               
                Vector2 directionRight = new Vector2(1, hitLocation).normalized;
                GetComponent<Rigidbody2D>().velocity = directionRight * ballSpeed;
                break;
            case "RacketRight":                
                Vector2 directionLeft = new Vector2(-1, hitLocation).normalized;
                GetComponent<Rigidbody2D>().velocity = directionLeft * ballSpeed;
                break;
            case "WallLeft":
            case "WallRight":
                RoundController.NewRoundStart(collision.gameObject.name);
                break;
        }        
    }       
}
