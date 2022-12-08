using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject Player;
    public Camera Camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 playerPos = Player.transform.position;
        Vector3 cameraPos = Camera.transform.position;
        Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10.0f);
       
    }
}
