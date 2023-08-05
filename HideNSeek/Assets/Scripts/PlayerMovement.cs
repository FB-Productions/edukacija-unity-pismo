using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller; // referenca za naš character controller 
    public float speed = 12f; // varijabla za brzinu kretanja igraèa
    float gravity; //varijabla za gravitaciju
    public Transform groundCheck; // varijabla u koju spremamo groundcheck objekt
    public float groundDistance = 0.4f; // varijabla koja nam služi za udaljenost u kojoj ground check radi;
    public LayerMask groundMask; //varijabla pomoæu koje možemo izabrati u Unity-u šta da se desi kada smo na
                                 //odreðenome layeru
    public bool isGrounded; //bool pomoæu kojega provjeravamo diramo li zemlju
    public float jumpHeight = 3f; // varijabla pomoæu koje odreðujemo koliko jako možemo skoèiti
    Vector3 velocity;

    public float coyoteTimer = 0.2f; // oh yes, like a pro gamer I can jump in zee air for a bit
    float coyoteTimerInit;
    float posYPrev;

    GameManager gm;
    Vector3 initPos;
    Quaternion initRot;

    private void Start()
    {
        gravity = Physics.gravity.y;
        controller = GetComponent<CharacterController>();
        coyoteTimerInit = coyoteTimer;
        gm = GameObject.FindObjectOfType<GameManager>();
        initPos = transform.position;
        initRot = transform.rotation;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//koristi se za provjeru
        //ima li objekt na tlu ili dodiruje tlo u Unityu

        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButton("Run")) //Go twice as fast while holding the run button
        {
            controller.Move(move * 2 * speed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (!isGrounded)
        {
            if (coyoteTimer > 0)
            {
                coyoteTimer -= Time.deltaTime;

                // If we moved vertically in the last frame, there's less coyote time
                if (transform.position.y < posYPrev - 0.01 || transform.position.y > posYPrev + 0.01)
                {
                    coyoteTimer -= Mathf.Abs(transform.position.y - posYPrev);
                }
            }
        }
        else // On the ground
        {
            coyoteTimer = coyoteTimerInit;
        }

        posYPrev = transform.position.y;

        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimer > 0))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimer = 0;
        }
        
        controller.Move(velocity * Time.deltaTime);

        if (transform.position.y < -100)
        {
            Respawn();
            velocity.y = -2f;
        }
    }

    void Respawn()
    {
        transform.position = initPos;
        transform.rotation = initRot;
    }
}
