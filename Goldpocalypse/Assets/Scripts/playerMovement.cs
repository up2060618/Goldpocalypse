using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public GameObject deadState;
    public GameObject Coin;
    private float speed = 10.0f;
    private Animator animator;
    bool facingRight = true;
    public float currentHealth;
    public float currentFocus;
    public float maxFocus = 100;
    public float maxHealth = 100;
    public healthBar Healthbar;
    public focusBar Focusbar;
    public Image focusBarFill;
    public timeManager timeControl;
    private bool canFocus = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentFocus = maxFocus;
        currentHealth = maxHealth;
        Focusbar.setMaxFocus(maxFocus);
        Healthbar.setMaxHealth(maxHealth);
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

        if (Input.GetKey(KeyCode.Space) && currentFocus >= 0f && canFocus)
        {
            timeControl.timeSlowing();
            currentFocus -= 700 * Time.deltaTime; // time.deltatime used to make sure systems with low fps aren't penalised
            Focusbar.setFocus(currentFocus);
            
        }
        if (currentFocus < 0)
        {
            timeControl.timeReturn();
            canFocus = false;
            focusBarFill.color = Color.grey;
            currentFocus += 25 * Time.deltaTime;
            Focusbar.setFocus(currentFocus);
        }
        else
        {
            if (currentFocus <= maxFocus)
            {
                currentFocus += 25 * Time.deltaTime;
            }
            Focusbar.setFocus(currentFocus);
        }
        if (currentFocus >= maxFocus && !canFocus)
        {
            canFocus = true;
            focusBarFill.color = Color.yellow;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBlue" || collision.gameObject.tag == "EnemyRed" || collision.gameObject.tag == "EnemyGreen")
        {
            currentHealth -= 10;
            Healthbar.setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("You're dead");
                SceneManager.LoadScene("GameOver");
                Cursor.visible = true;
            }
            Destroy(collision.gameObject);
            Instantiate(deadState, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(Coin, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
