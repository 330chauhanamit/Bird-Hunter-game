using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 
    public float speed = 3.0f;
    public static float totalkilledenemies=0;
    public float changetime = 3.0f;
    public int maxHealth = 100;
    int currentHealth;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction= 1;
    // Start is called before the first frame update
    void Start()
    {
       
        

        currentHealth = maxHealth;
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changetime;
    }

    public void takedamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        totalkilledenemies++;
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <0)
        {
            direction = -direction;
            timer = changetime;
        }
    }
    void FixedUpdate()
    {
         Vector2 position = rigidbody2D.position;
     

            transform.localScale = new Vector3(direction, 1, 1);
            position.x = position.x + Time.deltaTime * speed* direction;
            rigidbody2D.MovePosition(position);
    }

 
}
