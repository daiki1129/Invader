using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBullet : MonoBehaviour
{
    public GameObject title2Maneger;
    // Start is called before the first frame update
    void Start()
    {
        title2Maneger = GameObject.Find("title2Maneger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�e����������
    void OnTriggerEnter2D(Collider2D Collider)
    {
        //���ꂪ�G��������
        if (Collider.gameObject.CompareTag("enemy"))
        {
            title2Maneger.GetComponent<title2Maneger>().enemyknockdown(gameObject, Collider.gameObject);
        }

    }
}
