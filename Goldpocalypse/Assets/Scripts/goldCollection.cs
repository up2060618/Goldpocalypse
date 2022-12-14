using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goldCollection : MonoBehaviour
{
    public AudioSource source;
    public AudioClip goldPickup;
    private int goldCount;
    public Text counterText;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            playCollectionSound();
            goldCount++;
        }
    }

    private void Start()
    {
        goldCount = 0;
        counterText.text = "Gold: " + goldCount.ToString();
    }
    private void Update()
    {
        counterText.text = "Gold: " + goldCount.ToString();
        if (goldCount == 51)
        {
            SceneManager.LoadScene("YouWin");
            Cursor.visible = true;
        }
    }

    void playCollectionSound()
    {
        source.PlayOneShot(goldPickup);
    }
}
