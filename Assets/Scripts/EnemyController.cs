using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed; // она вообще влияет???
    
    Transform ball;  
    Rigidbody2D ballRigidbody2D;
    float ballRadius;

    //Поставить свои значения - нужны ли они?
    //public float topBound = 4.5F;
    //public float bottomBound = -4.5F;
    
    void Start()
    {
        ballRadius = GameObject.FindGameObjectWithTag("Ball").GetComponent<CircleCollider2D>().radius;
        InvokeRepeating("Move", 0.0001f, 0.0001f);
    }

    private void Move()
    {      
        if (ball == null)
        {            
            ball = GameObject.FindGameObjectWithTag("Ball").transform;
        }       
        
        ballRigidbody2D = ball.GetComponent<Rigidbody2D>();

        // Чтобы меньше дергался, если мяч летит почти параллельно - хз, есть ли эффект
        if (Mathf.Abs(Mathf.Abs(ball.position.y) - Mathf.Abs(this.transform.position.y)) < 2 * ballRadius * 0.01f)
        {            
            return;
        }       

        // Проверяем направление меча, x > 0 - значит летит в сторону "врага"
        if (ballRigidbody2D.velocity.x > 0)
        {
            // Смотрим 'y' переменную мяча
            // Если она меньше (ниже) - двигаем ракетку вниз
            /*if (ball.position.y < this.transform.position.y)
            {
                transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);
            }
            // Выше - вверх
            else if (ball.position.y > this.transform.position.y)
            {
                transform.Translate(Vector2.up * enemySpeed * Time.deltaTime);
            }*/

            // Альтернативное управление - должно работать как "скорость" реакции бота          
            if (ball.position.y < this.transform.position.y - 0.0001f)
            {               
                transform.Translate(Vector2.down * enemySpeed * Time.deltaTime);
            }
            // Выше - вверх
            else if (ball.position.y > this.transform.position.y - 0.0001f)
            {                
                transform.Translate(Vector2.up * enemySpeed * Time.deltaTime);
            }
        }

        // С границами вроде как справляются коллайдеры, проверить / переделать
        /*if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }*/
    }
}
