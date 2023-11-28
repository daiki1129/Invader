using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class coinManeger : MonoBehaviour
{
    [SerializeField, Tooltip("現在のコイン枚数")]
    private int coin = 0;
    [SerializeField, Tooltip("コインスクリプト")]
    CoinCount coinscript;
    [SerializeField, Tooltip("テキストオブジェ")]
    private GameObject TextObj;
    [SerializeField, Tooltip("1P用テキストオブジェ")]
    private GameObject Text1PObj;
    [SerializeField, Tooltip("1P以上用テキストオブジェ")]
    private GameObject Text2PObj;

    [SerializeField, Tooltip("シーン移動のボタンを押したか？")]
    private bool sceneButton;
    [SerializeField, Tooltip("隠すオブジェ")]
    private GameObject hideSquare;
    [SerializeField, Tooltip("テキストオブジェ")]
    private GameObject PlaterTextObj;
    [SerializeField, Tooltip("スコアオブジェ")]
    private GameObject Score;
    [SerializeField, Tooltip("スコアオブジェ")]
    private GameObject Score2;
    [SerializeField, Tooltip("スコアオブジェ")]
    private GameObject Ui;
    [SerializeField, Tooltip("スコアオブジェ")]
    private GameObject[] scoreobj;
    [SerializeField, Tooltip("数字オブジェ")]
    private Sprite numbersOBJ;

    // Start is called before the first frame update
    void Start()
    {
        Ui = GameObject.Find("UI");
        Score = GameObject.Find("score");
        Score2 = Ui.transform.Find("score2").gameObject;
        GameObject coinCountobj = GameObject.Find("CoinCount");
        coinscript = coinCountobj.GetComponent<CoinCount>();
        PlaterTextObj.gameObject.SetActive(false);
        coin = coinscript.coinreturn();
        sceneButton = false;
        //スコアの表示部分を取得
        for (int i = 0; i < 5; i++)
        {
            scoreobj[i] = GameObject.Find("UI/score/Sc" + i);
        }
        if (coin == 1)
        {
            //表示する
            Text1PObj.gameObject.SetActive(true);
            Text2PObj.gameObject.SetActive(false);
        }
        else if (coin > 1)
        {
            //表示する
            Text2PObj.gameObject.SetActive(true);
            Text1PObj.gameObject.SetActive(false);
        }
        //保存
        PlayerPrefs.SetInt("Level", 1);
        //保存
        PlayerPrefs.SetInt("score1", 0);

    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current.digit1Key.wasPressedThisFrame)
        {
            Score2.gameObject.SetActive(false);
            //押された
            sceneButton = true;
            CoinJudgeStart();
        }
        if (current.digit2Key.wasPressedThisFrame)
        {
            Score2.gameObject.SetActive(true);
            //押された
            sceneButton = true;
            CoinJudgeStart();
        }
        coin = coinscript.coinreturn();
        if (coin == 1 && !sceneButton)
        {
            //表示する
            Text1PObj.gameObject.SetActive(true);
            Text2PObj.gameObject.SetActive(false);
        }
        else if (coin > 1 && !sceneButton)
        {
            //表示する
            Text2PObj.gameObject.SetActive(true);
            Text1PObj.gameObject.SetActive(false);
        }
    }
    //スコアの表示
    void ScoreDisplay()
    {
        //スコアの指定した桁の保存変数
        int scoredigit;
        //一桁ずつ表示して抜け出す
        for (int i = 0; i <= 4; i++)
        {
            //オブジェクトをspriteを変更
            scoreobj[i].GetComponent<SpriteRenderer>().sprite = numbersOBJ;
        }
    }

    //コインがある時ゲームスタート
    void CoinJudgeStart()
    {

        coin = coinscript.coinreturn();
        if (coin > 0)
        {
            coin--;
            coinscript.setcoin(coin);
            StartCoroutine(starttext());
        }
        else
        {
            coinscript.setcoin(coin);
        }
    }
    //スタートテキスト
    IEnumerator starttext()
    {
        TextObj.gameObject.SetActive(false);
        Text1PObj.gameObject.SetActive(false);
        Text2PObj.gameObject.SetActive(false);
        PlaterTextObj.gameObject.SetActive(true);
        ScoreDisplay();
        for (int i = 0; i < 23; i++)
        {
            Score.gameObject.SetActive(false);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            Score.gameObject.SetActive(true);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        for (int i = 0; i <= 28; i++)
        {
            hideSquare.transform.position += new Vector3(8, 0, 0);
            yield return null;
        }
        yield return null;
        titleChangeScene();
    }
    public void titleChangeScene()
    {
        //難易度シーンに移動
        SceneManager.LoadScene("game");
    }
}
