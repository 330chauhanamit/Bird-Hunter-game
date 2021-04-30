using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Control : MonoBehaviour
{
    AudioSource audioSource;  
       
    public AudioClip Attackclip;                    // Public Variable for Accessing AuduiClip
    public AudioClip Footsteps;
    public AudioClip jumps;
    public CharacterController2D controller;        //Public Variable for Accessing CharatorController2D
    public Animator animator;
    public float speed = 30f;
    float horizontal = 0f;
    bool jump ;                                     //bool variable for jump and crouch
    bool crouch = false;

    public int healthmax = 100;
    public int healthnow;
    public HealthBar healthbar;                     
    public LayerMask enemyLayers;                   //For activities control of same layer
    public Transform attackPoint;                   //For Accessing position from outside
    public float attackRange = 0.5f;

    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        healthnow = healthmax;
        healthbar.setmaxhealth(healthmax);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed; //Input From Keyboard
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if(Input.GetButtonDown("Jump"))
        {
            PlaySound(jumps);
            jump = true;
            animator.SetBool("Jumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if(Input.GetButtonDown("Crouch"))
        {
            crouch = false;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
                 isInvincible = false; 
        }
    }
    void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, crouch, jump);  
    }

    void Attack()
    {

        PlaySound(Attackclip);
        animator.SetTrigger("Attack");
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach( Collider2D enemy in hitenemies)
        {
            Debug.Log("Total Killed - " + Enemy.totalkilledenemies.ToString() + "/20");

            enemy.GetComponent<Enemy>().takedamage(20);
        }
    } 

    public void Onlanding()
    {
        jump = false;
        animator.SetBool("Jumping", false);
    }

    public void healthchange (int amount)
    {
        if (Enemy.totalkilledenemies%20==0 && Enemy.totalkilledenemies!=0)   //The Gate for  Level 2 is closed upto 20 enemies killed
        {
            healthnow = healthmax;                                       //Getting Heal
        }
        if (healthnow ==0)
        {
            Enemy.totalkilledenemies =0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);  //Game Scene Restarts
        }


        if (amount < 0)
        {
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        healthnow = Mathf.Clamp(healthnow + amount, 0, healthmax);
        Debug.Log("Current Health - " +healthnow + "/" + healthmax);
        healthbar.setHealth(healthnow); 
    }

    void OnDrawGizmosSelected()                             // Creating a visual shape for attackpoint
    {
        if(attackPoint == null)
        { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);                      //playing the sound
    }
}
