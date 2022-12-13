using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public GameObject player;
    // common base class for sharing stuff (e.g. static counter variables)
    // also forces people to implement minimal functionality
    public virtual void handleInput(gameStates thisObject) { }
    public virtual void report(gameStates thisObject) { }
    public virtual void movement(gameStates thisObject) { }
};

public class AlertState : PlayerState
{
    private int randomNumber;
    private int detectionNumber = 2;
    private float timeInterval = 1.0f;
    public float period = 0.0f;
    public override void handleInput(gameStates thisObject)
    {
        if (period > timeInterval)
        {
            randomNumber = Random.Range(0, 3);
            //Debug.Log(randomNumber);
            if (randomNumber == detectionNumber)
            {
                thisObject.currentState = new ChaseState();
            }
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
        
    }

    public override void report(gameStates thisObject)
    {
        Debug.Log(thisObject.enemyName + " is currently alert");
    }
}

public class ChaseState : PlayerState
{
    private float speed = 8.0f;
    // float maxDist = 8.0f;
    private float minDist = 6.0f;
    public override void movement(gameStates thisObject)
    {
       
        if (Vector2.Distance(thisObject.transform.position, thisObject.player.transform.position) <= minDist)
        {
            thisObject.transform.position = Vector3.MoveTowards(thisObject.transform.position, thisObject.player.transform.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(thisObject.transform.position, thisObject.player.transform.position) >= minDist)
        {
            thisObject.transform.position = Vector3.MoveTowards(thisObject.transform.position, thisObject.player.transform.position, speed * Time.deltaTime);
            thisObject.StartCoroutine(ExecuteAfterTime(3));
            IEnumerator ExecuteAfterTime(float time)
            {
                yield return new WaitForSeconds(time);
                if (Vector2.Distance(thisObject.transform.position, thisObject.player.transform.position) >= minDist)
                {
                    thisObject.currentState = new PatrollingState();
                }
                
            }
            
        }
    }

    public override void report(gameStates thisObject)
    {
        Debug.Log(thisObject.enemyName + " is currently chasing the player");
    }
}


public class PatrollingState : PlayerState
{
    private float speed = 5.0f;
    private float timer = 0.0f;
    private float minDist = 4.0f;
    public override void movement(gameStates thisObject)
    {
        Vector2 enemyPos = thisObject.transform.position;
        timer += Time.deltaTime;
        if (timer % 10 > 5)
        {
            enemyPos.x -= speed * Time.deltaTime;
        }
        else
        {
            enemyPos.x += speed * Time.deltaTime;
        }
        thisObject.transform.position = enemyPos;

        if (Vector2.Distance(thisObject.transform.position, thisObject.player.transform.position) <= minDist)
        {
            thisObject.currentState = new AlertState();
        }
    }

    public override void report(gameStates thisObject)
    {
        Debug.Log(thisObject.enemyName + " is currently patrolling");
    }
}

public class gameStates : MonoBehaviour
{

    public string enemyName;
    public GameObject player;
    public PlayerState currentState;

    // Use this for initialization
    void Start()
    {
        currentState = new PatrollingState();
        InvokeRepeating("Report", 0.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.handleInput(this);
        currentState.movement(this);
    }

    void Report()
    {
        currentState.report(this);
    }

}

