using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //弾当たった時
    void OnTriggerEnter2D(Collider2D Collider)
    {
        //それが敵だったら
        if (Collider.gameObject.CompareTag("enemy"))
        {
            gamemanager.GetComponent<GameManager>().enemyknockdown(gameObject, Collider.gameObject);
        }
            
    }
}
