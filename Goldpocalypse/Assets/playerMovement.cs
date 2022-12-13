using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float speed = 10.0f;
    private Animator animator;
    bool facingRight = true;

    public timeManager timeControl; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", false);
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        if (Input.GetKey("d"))
        {
            if (!facingRight)
            {
                Flip();
            }
            pos.x += speed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        if (Input.GetKey("a"))
        {
            if (facingRight)
            {
                Flip();
            }
            pos.x -= speed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeControl.timeSlowing();
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeControl.timeReturn();
        }

        transform.position = pos;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
