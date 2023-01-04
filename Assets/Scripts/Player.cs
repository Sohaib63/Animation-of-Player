using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public HealthbarController healthbar;
    float health = 5f;
    float horizontalMovement=0f;
    float playerSpeed=15f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2d;
    Animator animator;
    public GameObject globallight;
    Light2D ourlight;
    public Color one;
    public Color two;
    bool inAir=false;
    public Joystick joystick;
    Vector3 forward;
    Vector3 backward;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        forward=new Vector3(0,0,0);
        backward=new Vector3(0,180,0);
        ourlight=globallight.GetComponent<Light2D>();
    }
    int time=0;
    // Update is called once per frame
    void Update()
    {
        if (time%100==0){
            ourlight.intensity += 0.1f;
            ourlight.color=Color.Lerp(one,two,0.5f);
        }
        time++;

        horizontalMovement=joystick.Horizontal*playerSpeed*Time.deltaTime;
        print(horizontalMovement);
        

        //animator.SetFloat("Speed",Mathf.Abs(horizontalMovement));
        float speed=playerSpeed*Time.deltaTime;

        if (joystick.Horizontal>=0.2f){
            horizontalMovement=speed;
            healthbar.SetHealth(health);
            health -= 0.001f;
        }
        else if (joystick.Horizontal<=-0.2f){
            horizontalMovement=-speed;
            healthbar.SetHealth(health);
            health -= 0.001f;
        }
        else{
            horizontalMovement=0f;
        }

        transform.position +=new Vector3(horizontalMovement,0,0);

        if (joystick.Vertical>0.5f && !inAir){
            rigidBody2d.AddForce(Vector3.up*320);
            animator.SetBool("isJump",true);
            inAir=true;
        }

        /*if (horizontalMovement>0){
            transform.eulerAngles=forward;
            healthbar.SetHealth(health);
            health -= 0.001f;
        }
        else if(horizontalMovement<0){
            transform.eulerAngles=backward;
            healthbar.SetHealth(health);
            health -= 0.001f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inAir){
            rigidBody2d.AddForce(Vector3.up * 250);
            animator.SetBool("isJump",true);
            inAir=true;
        }*/ 

        if (Input.GetButtonDown("Fire1")){
            animator.SetBool("isShoot",true);
        }

        if (Input.GetButtonUp("Fire1")){
            animator.SetBool("isShoot",false);
        }

        if (Input.touchCount>1){
            Touch touch=Input.GetTouch(1);
            if (touch.phase==TouchPhase.Moved){
                Debug.Log("Moving");
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            animator.SetBool("isJump", false);
            inAir=false;
        }
    }
}
