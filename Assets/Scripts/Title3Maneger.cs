using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;

public class Title3Maneger : MonoBehaviour
{
    [SerializeField, Tooltip("Title3テキストオブジェ")]
    private GameObject Title3TextObj;
    [SerializeField, Tooltip("Title3テキストオブジェ")]
    private GameObject[] Title3TextObjs;
    [SerializeField, Tooltip("隠すオブジェ")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("シーン移動のボタンを押したか？")]
    private bool sceneButton;

    [SerializeField, Tooltip("アニメーションする敵オブジェクト")]
    private GameObject AnimationEnemy;
    [SerializeField, Tooltip("画像のインデックス")]
    private int enemyIndex = 0;
    [SerializeField, Tooltip("いか画像")]
    public List<Sprite> ikaTables;

    [SerializeField, Tooltip("弾プレハブ")]
    private GameObject enemybulletPrefab;
    [SerializeField, Tooltip("アニメーション弾")]
    private GameObject enemybullet;
    [SerializeField, Tooltip("Cの文字")]
    private GameObject Cobj;
    [SerializeField, Tooltip("爆発プレハブ")]
    private GameObject explosiontPrefab;
    [SerializeField, Tooltip("爆発オブジェクト")]
    private GameObject explosiontObj;

    [SerializeField, Tooltip("アニメーションするかどうか")]
    private int iAnimationOK = 0;

    [SerializeField, Tooltip("コインオブジェ")]
    private GameObject coinobj;
    [SerializeField, Tooltip("コインの数")]
    private int coin;
    [SerializeField, Tooltip("コインスクリプト")]
    CoinCount coinscript;

    [SerializeField, Tooltip("いか")]
    private GameObject ikaObj;
    [SerializeField, Tooltip("逆y")]
    private GameObject CObj;

    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 60;
        coinobj = GameObject.Find("CoinCount");
        coinscript = coinobj.GetComponent<CoinCount>();
        iAnimationOK = PlayerPrefs.GetInt("AnimationOK", 0);
        if (iAnimationOK == 1)
        {
            ikaObj.gameObject.SetActive(true);
            CObj.gameObject.SetActive(true);
        }
        else
        {
            ikaObj.gameObject.SetActive(false);
            CObj.gameObject.SetActive(false);
        }
        if (coin > 0)
        {
            //coinシーンに移動
            SceneManager.LoadScene("coin");
        }
        StartCoroutine(TextAppearance());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //textを1つずつ出現させる
    IEnumerator TextAppearance()
    {
        //子オブジェクト分ループする
        for (int i = 0; i < Title3TextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //子オブジェクトを取得する
            Title3TextObjs[i] = Title3TextObj.transform.GetChild(i).gameObject;
            //表示する
            Title3TextObjs[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        if (iAnimationOK == 1)
        {
            iAnimationOK = 0;
            PlayerPrefs.SetInt("AnimationOK", iAnimationOK);
            StartCoroutine(EnemyAnimation());
        }
        else
        {
            iAnimationOK = 1;
            PlayerPrefs.SetInt("AnimationOK", iAnimationOK);
            StartCoroutine(ChangeScene());
        }
    }

    /*/コインがある時ゲームスタート
    void CoinJudgeStart()
    {
        coin = coinscript.coinreturn();
        if (coin > 0)
        {
            coin--;
            coinscript.setcoin(coin);
            gameChangeScene();
        }
        else
        {
            coinscript.setcoin(coin);
        }
    }*/

    //アニメーションをする
    IEnumerator EnemyAnimation()
    {
        while (AnimationEnemy.transform.position.x < 8)
        {
            AnimationEnemy.transform.position += new Vector3(2, 0, 0);
            EnemymoveAnimation();
            yield return null;
            yield return null;
        }
        //弾を撃つ
        enemybullet = Instantiate(enemybulletPrefab, AnimationEnemy.transform.position, transform.rotation);
        while (enemybullet.transform.position.y > 20)
        {
            enemybullet.transform.position += new Vector3(0, -1, 0);
            yield return null;
        }
        explosiontObj = Instantiate(explosiontPrefab, Cobj.transform.position, transform.rotation);
        //弾を消す
        Destroy(enemybullet);
        //文字を消す
        Destroy(Cobj);
        Destroy(explosiontObj, 0.2f);
        Destroy(AnimationEnemy, 0.5f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ChangeScene());
    }
    //敵のアニメーションを変える
    void EnemymoveAnimation()
    {
        //画像を順番に変える
        enemyIndex++;
        //最後まで行ったら最初に戻す
        if (enemyIndex >= 6)
        {
            enemyIndex = 0;
        }

        SpriteRenderer enemychildSprite = AnimationEnemy.GetComponent<SpriteRenderer>();
        //画像を切り替える
        enemychildSprite.sprite = ikaTables[enemyIndex];
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
        //一度も押されていないか
        if (!sceneButton)
        {
            //押された
            sceneButton = true;
            //難易度シーンに移動
            SceneManager.LoadScene("title");
        }
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
}
