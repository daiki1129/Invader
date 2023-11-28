using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField, Tooltip("収納場所")]
    public static UI instance;

    void Awake()
    {
        // インスタンスが存在しない場合
        if (instance == null)
        {
            // このインスタンスをSingletonインスタンスにする
            instance = this;
            // シーンを変更してもこのGameObjectを維持する
            DontDestroyOnLoad(gameObject);
        }
        // インスタンスが既に存在する場合
        else
        {
            // このGameObjectを破棄する
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
