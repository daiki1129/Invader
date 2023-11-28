using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoenemyBullet : MonoBehaviour
{
    public GameObject title2Maneger;
    bool itido = false;
    // Start is called before the first frame update
    void Start()
    {
        title2Maneger = GameObject.Find("title2Maneger");
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
            title2Maneger.GetComponent<title2Maneger>().playerdown(gameObject, Collider.gameObject);
        }
        if (Collider.gameObject.CompareTag("PlayerBullet"))
        {
            title2Maneger.GetComponent<title2Maneger>().BulletCollision(gameObject, Collider.gameObject);
        }
    }
}
