using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public GameObject gamemanager;
    bool itido = false;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        itido = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //’e“–‚½‚Á‚½Žž
    void OnTriggerEnter2D(Collider2D Collider)
    {
        //‚»‚ê‚ª“G‚¾‚Á‚½‚ç
        if (Collider.gameObject.CompareTag("Player") && itido == false)
        {
            itido = true;
            gamemanager.GetComponent<GameManager>().playerdown(gameObject, Collider.gameObject);
        }
        if (Collider.gameObject.CompareTag("PlayerBullet"))
        {
            gamemanager.GetComponent<GameManager>().BulletCollision(gameObject, Collider.gameObject);
        }
    }


}
