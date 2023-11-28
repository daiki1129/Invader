using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class CoinCount : MonoBehaviour
{
    [SerializeField, Tooltip("現在のコイン枚数")]
    private int iCoin = 0;

    [SerializeField, Tooltip("数字オブジェ")]
    private Sprite[] numbersObj;
    [SerializeField, Tooltip("コインオブジェ")]
    private GameObject[] coinobj;

    // Start is called before the first frame update
    void Start()
    {
        coinobj[0] = GameObject.Find("coin01");
        coinobj[1] = GameObject.Find("coin02");

        CoinDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        //コインを入れた時
        if (current.digit5Key.wasPressedThisFrame && iCoin < 99)
        {
            iCoin++;
            CoinDisplay();
            if (SceneManager.GetActiveScene().name == "title" || SceneManager.GetActiveScene().name == "title2" || SceneManager.GetActiveScene().name == "title3")
            {
                SceneManager.LoadScene("coin");
            }
        }
    }
    //コイン枚数の表示
    void CoinDisplay()
    {
        //コイン枚数の指定した桁の保存変数
        int scoredigit;
        //一桁ずつ表示して抜け出す
        for (int i = 2; i >= 1; i--)
        {
            //桁ごとの値を出す
            scoredigit = (int)(iCoin / Mathf.Pow(10, i - 1)) % 10;
            //オブジェクトをspriteを変更
            coinobj[i - 1].GetComponent<SpriteRenderer>().sprite = numbersObj[scoredigit];
        }
    }
    public int coinreturn()
    {
        return iCoin;
    }

    public void setcoin(int coin)
    {

        iCoin = coin;
        Debug.Log(iCoin);
        CoinDisplay();
    }
}
