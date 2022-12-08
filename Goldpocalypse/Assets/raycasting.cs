using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycasting : MonoBehaviour
{
    private Animation anim;
    public GameObject shootPoint;
    public GameObject Crosshair;
    public GameObject Coin;
    public GameObject deadState;
    private int obstacleLayer;
    private int enemyLayer;
    private bool shootable = true;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        Debug.Log(obstacleLayer);
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Crosshair.transform.position = cursorPos;
        Vector2 playerPos = shootPoint.transform.position;
        Vector2 distanceBetween = cursorPos - playerPos;


        RaycastHit2D hit = Physics2D.Raycast(playerPos, distanceBetween, distanceBetween.magnitude, obstacleLayer);
        if (!hit)
        {
            Debug.DrawRay(playerPos, distanceBetween, Color.green, 0.0f);
            shootable = true;
            // Debug.Log("No Obstacle");
        }
        else
        {
            Debug.DrawRay(playerPos, distanceBetween, Color.red, 0.0f);
            shootable = false;
            //Debug.Log("Obstacle");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && shootable)
        {
            RaycastHit2D rayshoot = Physics2D.Raycast(playerPos, distanceBetween, distanceBetween.magnitude, enemyLayer);
            GameObject hitObj = rayshoot.collider.gameObject;
            Debug.Log(hitObj);
            if (hitObj.tag == "Enemy")
            {
                Debug.Log("hit enemy");
                Destroy(hitObj, 0.0f);
                Instantiate(deadState, hitObj.transform.position, hitObj.transform.rotation);
                Instantiate(Coin, hitObj.transform.position, hitObj.transform.rotation);

            }
        }
    }
}
