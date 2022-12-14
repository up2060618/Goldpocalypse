using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject player;
    public GameObject redSlime;
    public GameObject blueSlime;
    public GameObject greenSlime;
    public GameObject rocks;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 17; i++) 
        {
            Vector2 position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 20));
            GameObject enemySpawn = Instantiate(redSlime, position, Quaternion.identity);
            gameStates behaviour = enemySpawn.GetComponent<gameStates>();
            behaviour.player = player;
        }
        for (int i = 0; i < 17; i++) 
        {
            Vector2 position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 20));
            GameObject enemySpawn = Instantiate(greenSlime, position, Quaternion.identity);
            gameStates behaviour = enemySpawn.GetComponent<gameStates>();
            behaviour.player = player;
        }
        for (int i = 0; i < 17; i++) 
        {
            Vector2 position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 20));
            GameObject enemySpawn = Instantiate(blueSlime, position, Quaternion.identity);
            gameStates behaviour = enemySpawn.GetComponent<gameStates>();
            behaviour.player = player;
        }
        for (int i = 0; i < 25; i++)
        {
            Vector2 position = new Vector2(Random.Range(-40, 40), Random.Range(-25, 20));
            Instantiate(rocks, position, Quaternion.identity);
        }
    }

   
   
}
