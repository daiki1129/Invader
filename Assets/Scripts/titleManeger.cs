using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;

public class titleManeger : MonoBehaviour
{
    [SerializeField, Tooltip("PLAYテキストオブジェ")]
    private GameObject PLAYTextObj;
    [SerializeField, Tooltip("PLAYテキストオブジェ")]
    private GameObject[] PLAYTextObjs;
    [SerializeField, Tooltip("SPACE INVADERSテキストオブジェ")]
    private GameObject SPACEINVADERSTextObj;
    [SerializeField, Tooltip("SPACE INVADERSテキストオブジェ")]
    private GameObject[] SPACEINVADERSTextObjs;
    [SerializeField, Tooltip("SCOREテキストオブジェ")]
    private GameObject SCORETextObj;
    [SerializeField, Tooltip("SPACE INVADERSテキストオブジェ")]
    private GameObject pointsTextObj;
    [SerializeField, Tooltip("SPACE INVADERSテキストオブジェ")]
    private GameObject[] pointsTextObjs;

    [SerializeField, Tooltip("いか")]
    private GameObject ikaObj;
    [SerializeField, Tooltip("逆y")]
    private GameObject yObj;

    [SerializeField, Tooltip("シーン移動のボタンを押したか？")]
    private bool sceneButton;

    [SerializeField, Tooltip("隠すオブジェ")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("アニメーションする敵オブジェクト")]
    private GameObject AnimationEnemy;
    [SerializeField, Tooltip("画像のインデックス")]
    private int enemyIndex = 0;
    [SerializeField, Tooltip("いか画像")]
    public List<Sprite> ikaTables;
    [SerializeField, Tooltip("いか画像")]
    public List<Sprite> ikYTables;
    [SerializeField, Tooltip("いか画像")]
    public List<Sprite> ikaYreturnTables;

    [SerializeField, Tooltip("アニメーションするかどうか")]
    private int iAnimationOK = 0;

    [SerializeField, Tooltip("コインの数")]
    private int coin;
    [SerializeField, Tooltip("コインスクリプト")]
    CoinCount coinscript;

    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
        GameObject coinCountobj= GameObject.Find("CoinCount");
        coinscript= coinCountobj.GetComponent<CoinCount>();
        StartCoroutine(TextAppearance());

        //現在のレベル
        iAnimationOK = PlayerPrefs.GetInt("AnimationOK", 0);
        if (iAnimationOK == 1)
        {
            ikaObj.gameObject.SetActive(true);
        }
        else
        {
            ikaObj.gameObject.SetActive(false);
        }
        coin = coinscript.coinreturn();
        if (coin > 0)
        {
            //coinシーンに移動
            SceneManager.LoadScene("coin");
        }

        //titleChangeScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //textを1つずつ出現させる
    IEnumerator TextAppearance()
    {
        //子オブジェクト分ループする
        for (int i = 0; i < PLAYTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //子オブジェクトを取得する
            PLAYTextObjs[i] = PLAYTextObj.transform.GetChild(i).gameObject;
            //表示する
            PLAYTextObjs[i].gameObject.SetActive(true);
            if (iAnimationOK == 1 && i == 3)
            {
                yObj.gameObject.SetActive(true);
                PLAYTextObjs[i].gameObject.SetActive(false);
            }
            else
            {
                yObj.gameObject.SetActive(false);
            }
        }

        //子オブジェクト分ループする
        for (int i = 0; i < SPACEINVADERSTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //子オブジェクトを取得する
            SPACEINVADERSTextObjs[i] = SPACEINVADERSTextObj.transform.GetChild(i).gameObject;
            //表示する
            SPACEINVADERSTextObjs[i].gameObject.SetActive(true);     
        }
        yield return new WaitForSeconds(0.5f);
        //表示する
        SCORETextObj.gameObject.SetActive(true);

        //子オブジェクト分ループする
        for (int i = 0; i < pointsTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //子オブジェクトを取得する
            pointsTextObjs[i] = pointsTextObj.transform.GetChild(i).gameObject;
            //表示する
            pointsTextObjs[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        if (iAnimationOK == 1) {
            StartCoroutine(EnemyAnimation());
        }
        else
        {
            StartCoroutine(ChangeScene());
        }
    }

    //敵のアニメーション
    IEnumerator EnemyAnimation()
    {
        while (AnimationEnemy.transform.position.x > 21)
        {
            EnemymoveAnimation();
            AnimationEnemy.transform.position += new Vector3(-1, 0, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        yObj.gameObject.SetActive(false);

        while (AnimationEnemy.transform.position.x < 115)
        {
            EnemyYmoveAnimation();
            AnimationEnemy.transform.position += new Vector3(1, 0, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        while (AnimationEnemy.transform.position.x > 17)
        {
            EnemyYreturnmoveAnimation();
            AnimationEnemy.transform.position += new Vector3(-1, 0, 0);
            yield return null;
        }
        PLAYTextObjs[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(AnimationEnemy);
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeScene());
    }

    void EnemymoveAnimation()
    {
        //敵のアニメーションを変える
        enemyIndex++;
        if (enemyIndex >= 6)
        {
            enemyIndex = 0;
        }

        SpriteRenderer enemychildSprite = AnimationEnemy.GetComponent<SpriteRenderer>();
        enemychildSprite.sprite = ikaTables[enemyIndex];
    }

    void EnemyYmoveAnimation()
    {
        //敵のアニメーションを変える
        enemyIndex++;
        if (enemyIndex >= 6)
        {
            enemyIndex = 0;
        }

        SpriteRenderer enemychildSprite = AnimationEnemy.GetComponent<SpriteRenderer>();
        enemychildSprite.sprite = ikYTables[enemyIndex];
    }

    void EnemyYreturnmoveAnimation()
    {
        //敵のアニメーションを変える
        enemyIndex++;
        if (enemyIndex >= 6)
        {
            enemyIndex = 0;
        }

        SpriteRenderer enemychildSprite = AnimationEnemy.GetComponent<SpriteRenderer>();
        enemychildSprite.sprite = ikaYreturnTables[enemyIndex];
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

    public void gameChangeScene()
    {
        //一度も押されていないか
        if (!sceneButton)
        {
            //押された
            sceneButton = true;
            //難易度シーンに移動
            SceneManager.LoadScene("game");
        }
    }
    public void titleChangeScene()
    {
        //一度も押されていないか
        if (!sceneButton)
        {
            //押された
            sceneButton = true;
            //難易度シーンに移動
            SceneManager.LoadScene("title2");
        }
    }
}
