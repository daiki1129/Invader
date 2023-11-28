using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("キャンバス")]
    private GameObject canvas;
    int aaa;
    public bool Gameover;//ゲームオーバーフラグ
    public bool Gameclear;//ゲームクリアフラグ

    [SerializeField, Tooltip("何人プレイか")]
    private int pleayernumber = 1;

    [SerializeField, Tooltip("敵を倒した数")]
    private int iLevel;

    [SerializeField, Tooltip("シーン移動のボタンを押したか？")]
    private bool sceneButton;


    [SerializeField, Tooltip("地面プレハブ")]
    private GameObject GroundgPrefab;

    [SerializeField, Tooltip("プレイヤー")]
    private GameObject playerobj;
    [SerializeField, Tooltip("プレイヤープレハブ")]
    private GameObject playerfab;
    [SerializeField, Tooltip("プレイヤーの速さ")]
    private float speed = 50f;
    [SerializeField, Tooltip("爆発エフェクト")]
    private GameObject explosionobj;
    [SerializeField, Tooltip("爆発の画像")]
    private Sprite[] explosionSprite;

    [SerializeField, Tooltip("自機の爆発")]
    public Sprite[] PlayerexplosionTables;
    [SerializeField, Tooltip("自機の爆発インデックス")]
    public int iPlayerexplosionIndex = 0;

    [SerializeField, Tooltip("無敵かどうか")]
    private bool binvincible=false;

    [SerializeField, Tooltip("プレイヤーの残機")]
    private int life = 3;
    [SerializeField, Tooltip("残機表示オブジェ")]
    private GameObject lifeobj;

    [SerializeField, Tooltip("復活場所")]
    private Vector3 revivalPoint = new Vector3(-90, -104, 0);

    [SerializeField, Tooltip("弾の発射場所")]
    private Vector3 bulletPoint;
    [SerializeField, Tooltip("弾の位置調整x")]
    private int bulletPointx = 1;
    [SerializeField, Tooltip("弾の位置調整y")]
    private int bulletPointy = 5;
    [SerializeField, Tooltip("弾")]
    private GameObject bulletPrefab;
    [SerializeField, Tooltip("弾の速さ")]
    private float bulletspeed = 1f;
    [SerializeField, Tooltip("弾を撃った回数")]
    private int bulletnumber = 6;
    [SerializeField, Tooltip("弾を発射してもいいか")]
    private bool bulletflag = true;
    [SerializeField, Tooltip("弾のRigidbody")]
    private Rigidbody2D bulletrb;
    [SerializeField, Tooltip("爆発プレハブ")]
    private GameObject bulletexplosion;
    [SerializeField, Tooltip("自機弾爆発プレハブ")]
    private GameObject playerbulletexplosion;
    [SerializeField, Tooltip("敵弾爆発プレハブ")]
    private GameObject enemybulletexplosion;
    [SerializeField, Tooltip("敵爆発プレハブ")]
    private GameObject enemyexplosion;
    [SerializeField, Tooltip("爆発オブジェ")]
    private GameObject bulletexplosionobj;
    [SerializeField, Tooltip("black boxプレハブ")]
    private GameObject BlackBoxPrefab;
    [SerializeField, Tooltip("black boxオブジェ")]
    private GameObject BlackBoxObj;
    [SerializeField, Tooltip("black boxBigプレハブ")]
    private GameObject BlackBoxBigPrefab;
    [SerializeField, Tooltip("black boxBigプレハブ")]
    private GameObject boxobj;

    [SerializeField, Tooltip("UFO出ている")]
    private bool UFOspawn = false;

    [SerializeField, Tooltip("UFOプレハブ")]
    private GameObject UFOprefab;
    [SerializeField, Tooltip("UFOオブジェ")]
    private GameObject UFOobj;
    [SerializeField, Tooltip("UFO爆発プレハブ")]
    private GameObject UFOexplosion;

    [SerializeField, Tooltip("UFO出現場所")]
    public Vector3 spawnpos;
    [SerializeField, Tooltip("x座標UFO出現場所")]
    public float spawnposx = -110;

    [SerializeField, Tooltip("UFO出現時間")]
    public float spawnInterval = 25f;

    [SerializeField, Tooltip("時間")]
    private float timer = 0f;

    [SerializeField, Tooltip("UFOスピード")]
    private float UFOspeed = 0.1f;

    [SerializeField, Tooltip("UFO点数")]
    private int[] UFOscore = { 100, 100, 100, 50, 150, 100, 100, 50, 50, 100, 150, 100, 100, 50, 300 };
    [SerializeField, Tooltip("UFOポイントオブジェ")]
    private GameObject[] pointobj;

    [SerializeField, Tooltip("いか画像")]
    public List<Sprite> ikaTables;
    [SerializeField, Tooltip("かに画像")]
    public List<Sprite> kaniTables;
    [SerializeField, Tooltip("たこ画像")]
    public List<Sprite> takoTables;
    [SerializeField, Tooltip("残像オブジェ")]
    public GameObject[] afterimageobj;

    [SerializeField, Tooltip("画像のインデックス")]
    private int enemyIndex = 0;

    [SerializeField, Tooltip("たこプレハブ")]
    private GameObject takoPrefab;
    [SerializeField, Tooltip("かにプレハブ")]
    private GameObject kaniPrefab;
    [SerializeField, Tooltip("いかプレハブ")]
    private GameObject ikaPrefab;

    [SerializeField, Tooltip("行数")]
    private int rows = 5;
    [SerializeField, Tooltip("列数")]
    private int columns = 11;

    [SerializeField, Tooltip("敵配列")]
    public GameObject[,] enemyTables;

    [SerializeField, Tooltip("敵攻撃配列")]
    public GameObject[] enemyattackTables;

    [SerializeField, Tooltip("敵の位置")]
    private Vector3 enemyPos;
    [SerializeField, Tooltip("端の敵の位置")]
    private Vector3 enemyedgePos;

    [SerializeField, Tooltip("左右どちらに移動するか")]
    private bool moveright = true;//false:左移動　true：右移動
    [SerializeField, Tooltip("下に移動するか")]
    private bool movedown = false;
    [SerializeField, Tooltip("ヒットストップ")]
    private bool hitstop = false;
    [SerializeField, Tooltip("敵を倒した数")]
    private int iNumberDownEnemy = 0;


    [SerializeField, Tooltip("プレイヤーの少数点以下の座標を消すため")]
    private Vector3 newPosition;

    [SerializeField, Tooltip("敵の速度")]
    private int enemyspeed = 3;
    [SerializeField, Tooltip("敵のスピード")]
    private float espeed = 0.01f;

    [SerializeField, Tooltip("敵の弾1")]
    private GameObject enemybullet1Prefab;
    [SerializeField, Tooltip("敵の弾2")]
    private GameObject enemybullet2Prefab;
    [SerializeField, Tooltip("敵の弾3")]
    private GameObject enemybullet3Prefab;
    [SerializeField, Tooltip("敵の弾")]
    private GameObject[] enemybullet = { null, null };
    [SerializeField, Tooltip("敵の弾の発射場所")]
    private Vector3 enemybulletPoint;
    [SerializeField, Tooltip("敵の弾の速さ")]
    private float enemybulletspeed = -1f;
    [SerializeField, Tooltip("敵の弾1画像")]
    public List<Sprite> EnemyBullet1Images;
    [SerializeField, Tooltip("敵の弾2画像")]
    public List<Sprite> EnemyBullet2Images;
    [SerializeField, Tooltip("敵の弾3画像")]
    public List<Sprite> EnemyBullet3Images;
    [SerializeField, Tooltip("敵の攻撃テーブル")]
    private int[] iEnemyAttackTable1 = { 1, 7, 1, 1, 1, 4, 11, 1, 6, 3, 1, 1, 11, 9, 2, 8, 2 };
    [SerializeField, Tooltip("敵の攻撃テーブル")]
    private int[] iEnemyAttackTable2 = { 11, 1, 6, 3, 1, 1, 11, 9, 2, 8, 2, 11, 4, 7, 10, 5, 2 };
    [SerializeField, Tooltip("テーブルカウント")]
    private int Tablecount = 0;
    [SerializeField, Tooltip("攻撃順カウント")]
    private int attackcount = 1;
    [SerializeField, Tooltip("スコア")]
    private int score = 0;

    [SerializeField, Tooltip("タコスコア")]
    private int takoscore = 10;
    [SerializeField, Tooltip("カニスコア")]
    private int kaniscore = 20;
    [SerializeField, Tooltip("イカスコア")]
    private int ikascore = 30;

    [SerializeField, Tooltip("ハイスコア")]
    public int hiscore;
    [SerializeField, Tooltip("hiscoreテキスト")]
    public TextMeshProUGUI hiscoretext;
    [SerializeField, Tooltip("lifeテキスト")]
    public TextMeshProUGUI lifetext;

    [SerializeField, Tooltip("数字オブジェ")]
    private Sprite[] numbersOBJ;
    [SerializeField, Tooltip("スコアオブジェ")]
    private GameObject[] scoreobj;
    [SerializeField, Tooltip("ハイスコアオブジェ")]
    private GameObject[] hiscoreobj;

    [SerializeField, Tooltip("player画像")]
    public Image[] playerImages = { null, null, null, null, null };
    [SerializeField, Tooltip("player画像")]
    public Image playerImage;

    [SerializeField, Tooltip("ゲームオーバーテキスト")]
    public TextMeshProUGUI Gameovertext;
    [SerializeField, Tooltip("ゲームオーバーテキスト")]
    public string Gameovermozi = "Game Over";
    [SerializeField, Tooltip("隠すオブジェ")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("敵を倒す音")]
    public AudioClip sound1;
    public AudioSource audioSource1;
    [SerializeField, Tooltip("敵移動音")]
    public AudioClip[] sound2;
    public AudioSource audioSource2;
    [SerializeField, Tooltip("自機爆発")]
    public AudioClip sound3;
    public AudioSource audioSource3;
    [SerializeField, Tooltip("UFO爆発")]
    public AudioClip sound4;
    public AudioSource audioSource4;

    [SerializeField, Tooltip("1度のみ")]
    private bool onetime = false;

    [SerializeField, Tooltip("復活中")]
    private bool bResurrecting = false;

    public int enemybulletIndex = 0;



    [SerializeField, Tooltip("トーチカオブジェ")]
    private GameObject[] PillboxObj;

    [SerializeField, Tooltip("トーチカの配列")]
    int[,,] iPillbox1 =
    {
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},      
            {0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},     
            {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},       
            {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            {0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
             {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        },
        {
             {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
            { 0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},
            { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
            { 0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        },
    };


    [SerializeField, Tooltip("自機の爆発の配列")]
    int[,] EnemyBulletExplosionSize =
{
        {0,4},
        {-2,3},
        {2,3},
        {0,2},
        {1,2},
        {3,2},
        {-1,1},
        {0,1},
        {1,1},
        {2,1},
        {-2,0},
        {0,0},
        {1,0},
        {2,0},
        {-1,-1},
        {0,-1},
        {1,-1},
        {2,-1},
        {3,-1},
        {-2,-2},
        {0,-2},
        {1,-2},
        {2,-2},
        {-1,-3},
        {1,-3},
        {3,-3},
    };

    [SerializeField, Tooltip("敵の爆発の配列")]
    int[,] PlayerBulletExplosionSize =
    {
        {-3,3},
        {1,3},
        {4,3},
        {-1,2},
        {3,2},
        {-2,1},
        {-1,1},
        {0,1},
        {1,1},
        {2,1},
        {3,1},
        {-3,0},
        {-2,0},
        {-1,0},
        {0,0},
        {1,0},
        {2,0},
        {3,0},
        {4,0},
        {-3,-1},
        {-2,-1},
        {-1,-1},
        {0,-1},
        {1,-1},
        {2,-1},
        {3,-1},
        {4,-1},
        {-2,-2},
        {-1,-2},
        {0,-2},
        {1,-2},
        {2,-2},
        {3,-2},
        {-1,-3},
        {2,-3},
        {-3,-4},
        {0,-4},
        {4,-4},
    };


// Start is called before the first frame update
void Start()
    {
        Application.targetFrameRate = 60;
        //スコアの表示部分を取得
        for (int i = 0; i < 5; i++)
        {
            scoreobj[i] = GameObject.Find("UI/score/Sc" + i);
        }
        //ハイスコアの表示部分を取得
        for (int i = 0; i < 5; i++)
        {
            hiscoreobj[i] = GameObject.Find("UI/Hiscore/HiSc" + i);
        }
        //残機の表示部分を取得
        lifeobj = GameObject.Find("UI/life");
        //地面生成
        GroundgGnerate();
        iNumberDownEnemy = 0;
        playerobj = GameObject.Find("player");//プレイヤーを探す
        enemyTables = new GameObject[rows, columns];
        enemyattackTables = new GameObject[columns];
        //初期化
        Gameover = false;
        Gameclear = false;
        //現在のレベル
        iLevel = PlayerPrefs.GetInt("Level", 0);
        //スコアの取得
        score = PlayerPrefs.GetInt("score" + pleayernumber, 0);
        //敵をスポーンさせる
        StartCoroutine(enemySpawn());
        //ハイスコアの表示
        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        HiScoreDisplay();
        //Rigidbody取得
        StartCoroutine(enemymove());
        StartCoroutine(enemyattack());
        //hiscoretext.text = hiscore.ToString("d5");
        //lifetext.text = life.ToString();

        lifedisplay();
        StartCoroutine(PlayerAppear());
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!Gameover && !Gameclear)
        {
            timer += Time.deltaTime;

            // 一定の間隔でオブジェクトを生成
            if (timer >= spawnInterval && !Gameover && !Gameclear)
            {
                UFOSpawnObject();
                timer = 0f;
            }
            playermove();//プレイヤーの移動
            playerbullet();//弾の発射  
            if (UFOspawn&& !Gameover)
            {
                UFOmove();
            }
        }
        if (Gameover == true && !onetime)
        {
            onetime = true;
            HiScoreDisplay();
            StartCoroutine(GameOverChangeScene());
            Gameovertext.gameObject.SetActive(true);
        }
        if (Gameclear == true && !onetime)
        {
            StartCoroutine(GameClearChangeScene());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            binvincible = !binvincible;
            if (binvincible)
            {
                Debug.Log("無敵モード");
            }
        }
    }
    IEnumerator playerknockdown()
    {
        yield return null;
    }
    IEnumerator playergameover()
    {
        yield return null;
    }

    //地面を生成
    void GroundgGnerate()
    {
        for (int i = 0; i < 224; i++)
        {
            Vector3 Groundgpos = new Vector3(-111.5f + i, -119.5f, 0f);
            playerobj = Instantiate(GroundgPrefab, Groundgpos, transform.rotation);
        }
    }

    //敵を生成
    IEnumerator enemySpawn()
    {
        yield return new WaitForSeconds(0.2f);
        int enemyheight = -16;

        // ゲームオブジェクトの生成と配列への代入
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i < 2)
                {
                    enemyTables[i, j] = Instantiate<GameObject>(takoPrefab, new Vector3(-82 + j * 16, enemyheight + i * 16, 0), Quaternion.identity);
                }
                else if (i < 4)
                {
                    enemyTables[i, j] = Instantiate<GameObject>(kaniPrefab, new Vector3(-82 + j * 16, enemyheight + i * 16, 0), Quaternion.identity);
                }
                else if (i < 5)
                {
                    enemyTables[i, j] = Instantiate<GameObject>(ikaPrefab, new Vector3(-82 + j * 16, enemyheight + i * 16, 0), Quaternion.identity);
                }
                yield return null;
            }
        }

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (enemyTables[j, i] != null)
                {
                    enemyattackTables[i] = enemyTables[j, i];
                    break;
                }
            }
        }
    }

    //UFO出現させる
    void UFOSpawnObject()
    {
        spawnInterval = 25.0f;
        spawnpos = new Vector3(spawnposx, 96, 0);
        // プレハブからオブジェクトを生成
        UFOobj = Instantiate(UFOprefab, spawnpos, Quaternion.identity);
        UFOspawn = true;

    }
    //UFOの動き
    void UFOmove()
    {

        if (UFOobj != null)
        {
            UFOobj.transform.position += new Vector3(UFOspeed, 0, 0);
            if (UFOspeed > 0 && UFOobj.transform.position.x > 120)
            {
                Destroy(UFOobj);
            }
            else if (UFOspeed < 0&& UFOobj.transform.position.x < -120)
            {
                Destroy(UFOobj);
            }
        }

        if (UFOobj == null)
        {
            UFOspawn = false;
            spawnposx *= -1;
            UFOspeed *= -1;
        }
    }
    //プレイヤー出現
    IEnumerator PlayerAppear()
    {
        yield return new WaitForSeconds(2f);

        if (playerobj == null)
        {
            playerobj = Instantiate(playerfab, revivalPoint, transform.rotation);
            newPosition = revivalPoint;
            newPosition = playerobj.transform.position;
        }
        yield return null;
    }


    //プレイヤーの移動
    void playermove()
    {
        if (!bResurrecting)
        {
            var current = Keyboard.current;
            // Aキー（左移動） 
            if (playerobj != null && current.aKey.isPressed && playerobj.transform.position.x > -94)
            {
                // newPosition = newPosition - speed * transform.right * Time.deltaTime;
                newPosition = newPosition - speed * transform.right;
                playerobj.transform.position = new Vector3(Mathf.FloorToInt(newPosition.x), newPosition.y, newPosition.z);
            }

            // Dキー（右移動）
            if (playerobj != null && current.dKey.isPressed && playerobj.transform.position.x < 87)
            {
                // newPosition = newPosition + speed * transform.right * Time.deltaTime;
                newPosition = newPosition + speed * transform.right;
                playerobj.transform.position = new Vector3(Mathf.CeilToInt(newPosition.x), newPosition.y, newPosition.z);
            }
        }
    }
    //倒された時
    public void playerdown(GameObject enemyBullet, GameObject Player)
    {
        if (!binvincible)
        {
            //音(sound3)を鳴らす
            audioSource3.PlayOneShot(sound3);
            Destroy(enemyBullet);//弾を消す
                                 // Destroy(Player);//playerを消す
                                 //爆発を出現させる
                                 //GameObject Playerexplosion = Instantiate(bulletexplosion, Player.transform.position, transform.rotation);
            StartCoroutine(PlayerExplosion(Player));
            bResurrecting = true;
            StartCoroutine(revival());
            lifedestroy();
            lifedisplay();
        }
    }
    //爆発アニメーション
    IEnumerator PlayerExplosion(GameObject Playerexplosion)
    {
        int a = 0;
        while (a < 32)
        {
            a++;
            SpriteRenderer explosion = Playerexplosion.GetComponent<SpriteRenderer>();
            explosion.sprite = PlayerexplosionTables[iPlayerexplosionIndex];
            iPlayerexplosionIndex++;
            if (iPlayerexplosionIndex >= 5)
            {
                iPlayerexplosionIndex = 0;
            }
            yield return null;
            yield return null;
        }
        Destroy(Playerexplosion);//playerを消す
        yield return null;
    }
    //playerの弾と敵の弾が当たった時の処理
    public void BulletCollision(GameObject enemyBullet, GameObject PlayerBullet)
    {
        //どの弾が勝つかの整数
        int iprobabilityNumber= Random.Range(0, 4);
        //両方消滅
        if (iprobabilityNumber <= 1)
        {
            Destroy(enemyBullet);
            Destroy(PlayerBullet);
            //爆発を出現させる
            bulletexplosionobj = Instantiate(playerbulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
            //爆発を出現させる
            bulletexplosionobj = Instantiate(enemybulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2秒後に消す11
            Destroy(bulletexplosionobj, 0.2f);
        }
        //playerの弾消滅
        else if (iprobabilityNumber == 2)
        {
            Destroy(PlayerBullet);
            //爆発を出現させる
            bulletexplosionobj = Instantiate(playerbulletexplosion, PlayerBullet.transform.position, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
        }
        //敵の弾消滅
        else
        {
            Destroy(enemyBullet);
            //爆発を出現させる
            bulletexplosionobj = Instantiate(enemybulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
        }
    }
    //復活
    IEnumerator revival()
    {
        if (bResurrecting == true)
        {        
            life -= 1;
            //lifetext.text = life.ToString();
            if (life <= 0)
            {
                GameOver();
            }
        }

        yield return new WaitForSeconds(3f);

        if (playerobj == null&& !Gameover)
        {
            playerobj = Instantiate(playerfab, revivalPoint, transform.rotation);
            newPosition = revivalPoint;
            bResurrecting = false;
        }
        yield return null;
    }
    
    //弾の発射
    void playerbullet()
    {
        var current = Keyboard.current;
        //弾が撃てる状況でスペースキーが押されたら
        if (current.spaceKey.wasPressedThisFrame && bulletflag == true && playerobj != null && hitstop == false && !bResurrecting)
        {
            //弾を撃てなくする
            bulletflag = false;
            bulletnumber++;
            if (bulletnumber > UFOscore.Length - 1)
            {
                bulletnumber = 0;
            }
            //弾の撃つ場所
            bulletPoint = new Vector3(playerobj.transform.position.x + bulletPointx, playerobj.transform.position.y + bulletPointy, playerobj.transform.position.z);
            //弾を撃つ
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint, transform.rotation);
            //弾が進める上限
            StartCoroutine(DestroyBulletdistance(bullet));
        }
    }

    //弾がy100に行ったら弾を消す
    IEnumerator DestroyBulletdistance(GameObject Bullet)
    {
        //飛ばした弾がy100に行くまで待機
        while (Bullet != null && Bullet.transform.position.y < 102)
        {
            PlayerPillboxHit(Bullet);
            Bullet.transform.position += new Vector3(0, bulletspeed, 0);
            PlayerPillboxHit(Bullet);
            Bullet.transform.position += new Vector3(0, bulletspeed, 0);
            PlayerPillboxHit(Bullet);
            Bullet.transform.position += new Vector3(0, bulletspeed, 0);
            PlayerPillboxHit(Bullet);
            Bullet.transform.position += new Vector3(0, bulletspeed, 0);
            yield return null;
        }
        //弾オブジェクトが消えてなかったら
        if (Bullet != null)
        {
            //爆発を出現させる
            bulletexplosionobj = Instantiate(playerbulletexplosion, Bullet.transform.position, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
            Destroy(Bullet);//弾を消す
            bulletflag = true;//弾を撃てるようにする
        }
        if (Bullet == null)
        {
            bulletflag = true;//弾を撃てるようにする
        }
    }
    //敵を倒す
    public void enemyknockdown(GameObject Bullet, GameObject Enemy)
    {
        hitstop = true;
        Destroy(Bullet);//弾を消す
        if ("UFO(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            StartCoroutine(UFOpointdisplay(Enemy));
        }
        else
        {
            //爆発を出現させる
            bulletexplosionobj = Instantiate(enemyexplosion, Enemy.transform.position, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
        }
        //敵によってscoreを追加する
        if ("tako(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //音(sound1)を鳴らす
            audioSource1.PlayOneShot(sound1);
            score += takoscore;
            iNumberDownEnemy++;
        }
        if ("kani(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //音(sound1)を鳴らす
            audioSource1.PlayOneShot(sound1);
            score += kaniscore;
            iNumberDownEnemy++;
        }
        if ("ika(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //音(sound1)を鳴らす
            audioSource1.PlayOneShot(sound1);
            score += ikascore;
            iNumberDownEnemy++;
        }
        //変数の表示
        ScoreDisplay();

        if (iNumberDownEnemy == 55)
        {
            GameClear();
        }

        if (score > hiscore)
        {
            HiscoreSave();
        }

        //敵を消す
        Destroy(Enemy.gameObject.transform.parent.gameObject);

      
    }

    //UFOのポイント表示
    IEnumerator UFOpointdisplay(GameObject Enemy)
    {
        //爆発を出現させる
        bulletexplosionobj = Instantiate(UFOexplosion, Enemy.transform.position, transform.rotation);
        //0.2秒後に消す
        Destroy(bulletexplosionobj, 0.2f);
        //音(sound4)を鳴らす
        audioSource4.PlayOneShot(sound4);
        score += UFOscore[bulletnumber];
        Vector3 UFOpoint = Enemy.transform.position;
        yield return new WaitForSeconds(0.2f);
        switch (UFOscore[bulletnumber])
        {
            case 50:
                Destroy(Instantiate(pointobj[0], UFOpoint, transform.rotation), 1.0f);
                break;
            case 100:
                Destroy(Instantiate(pointobj[1], UFOpoint, transform.rotation), 1.0f);
                break;
            case 150:
                Destroy(Instantiate(pointobj[2], UFOpoint, transform.rotation), 1.0f);
                break;
            case 300:
                Destroy(Instantiate(pointobj[3], UFOpoint, transform.rotation), 1.0f);
                break;
        }
    }

    //スコアの表示
    void ScoreDisplay()
    {
        //スコアの指定した桁の保存変数
        int scoredigit;
        //一桁ずつ表示して抜け出す
        for (int i = 5; i >= 1; i--)
        {
            //桁ごとの値を出す
            scoredigit = (int)(score / Mathf.Pow(10, i - 1)) % 10;
            //オブジェクトをspriteを変更
            scoreobj[i-1].GetComponent<SpriteRenderer>().sprite = numbersOBJ[scoredigit];
        }
    }

    //ハイスコアの表示
    void HiScoreDisplay()
    {
        //ハイスコアの指定した桁の保存変数
        int scoredigit;
        //一桁ずつ表示して抜け出す
        for (int i = 5; i >= 1; i--)
        {
            //桁ごとの値を出す
            scoredigit = (int)(hiscore / Mathf.Pow(10, i - 1)) % 10;
            //オブジェクトをspriteを変更
            hiscoreobj[i - 1].GetComponent<SpriteRenderer>().sprite = numbersOBJ[scoredigit];
        }
    }

    //ハイスコアを保存
    void HiscoreSave()
    {
        hiscore = score;
       // hiscore = 0;
        //セーブ
        PlayerPrefs.SetInt("hiscore", hiscore);
        
    }



    //敵を左右に順番に動かす
    IEnumerator enemymove()
    {
        int enemyaudioIndex = 0;
        //無限ループ
        while (true)
        {
            if (!Gameover && !Gameclear)
            {

                //音(sound2)を鳴らす
                audioSource2.PlayOneShot(sound2[enemyaudioIndex]);
                enemyaudioIndex++;
                if (enemyaudioIndex > 3)
                {
                    enemyaudioIndex = 0;
                }

                //敵のアニメーションを交互に変える
                if (enemyIndex == 0)
                {
                    enemyIndex = 1;
                }
                else if (enemyIndex == 1)
                {
                    enemyIndex = 0;
                }
                //順番に動かす
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (enemyTables[i, j] != null)
                        {
                            if (hitstop)
                            {
                                yield return new WaitForSeconds(0.2f);
                                hitstop = false;
                            }
                            while(bResurrecting)
                            {
                                yield return null;
                            }
                            //下げるかどうか
                            if (movedown)
                            {
                                enemyTables[i, j].transform.position += new Vector3(0, -8, 0);
                                if (enemyTables[i, j].transform.position.y < -100)
                                {
                                    GameOver();
                                }
                            }
                            if (moveright && iNumberDownEnemy == 54 && i == 0)
                            {
                                enemyTables[i, j].transform.position += new Vector3(3, 0, 0);
                                if (enemyIndex == 0)
                                {
                                    Instantiate(afterimageobj[enemyIndex], enemyTables[i, j].transform.position, transform.rotation);
                                }
                                else
                                {
                                    Instantiate(afterimageobj[enemyIndex], enemyTables[i, j].transform.position, transform.rotation);
                                }
                            }
                            else if (moveright && iNumberDownEnemy == 54)
                            {
                                enemyTables[i, j].transform.position += new Vector3(3, 0, 0);
                            }
                            //右移動かどうか
                            else if (moveright)
                            {
                                enemyTables[i, j].transform.position += new Vector3(2, 0, 0);
                            }
                            else if (!moveright)
                            {
                                enemyTables[i, j].transform.position -= new Vector3(2, 0, 0);
                            }

                            enemyAnimation(enemyTables[i, j]);
                            if (enemyTables[i, j].transform.position.y <= -72)
                            {
                                Vector3 BlackBoxBigpos = enemyTables[i, j].transform.position;
                                BlackBoxBigpos.x += -8;
                                BlackBoxBigpos.y += 4;
                                boxobj =Instantiate(BlackBoxBigPrefab, BlackBoxBigpos, transform.rotation);
                                enemyPillboxHit(boxobj);
                            }
                            //yield return new WaitForSeconds(espeed);
                            yield return null;
                        }
                    }
                }
                yield return null;
                if (enemybullet[0] == null)
                {
                    StartCoroutine(enemyattack());
                }
                movedown = false;
                enemyposjudge();
            }
            yield return null;
        }
    }
    //右移動にさせるか左移動にさせるか
    void enemyposjudge()
    {
        //配列分ループ
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (moveright)
                {
                    if (enemyTables[i, j] != null && enemyedgePos.x < enemyTables[i, j].transform.position.x)
                    {
                        enemyedgePos = enemyTables[i, j].transform.position;
                    }
                }
                else if (!moveright)
                {
                    if (enemyTables[i, j] != null && enemyedgePos.x > enemyTables[i, j].transform.position.x)
                    {
                        enemyedgePos = enemyTables[i, j].transform.position;
                    }
                }
            }
        }
        if (moveright && enemyedgePos.x >= 96)
        {
            moveright = !moveright;
            movedown = true;
        }
        if (!moveright && enemyedgePos.x <= -96)
        {
            moveright = !moveright;
            movedown = true;
        }
    }
    //敵のアニメーション
    void enemyAnimation(GameObject enemy)
    {
        if (enemy != null && enemy.name == "tako(Clone)")
        {
            GameObject enemychild = enemy.transform.GetChild(0).gameObject;
            SpriteRenderer enemychildSprite = enemychild.GetComponent<SpriteRenderer>();
            enemychildSprite.sprite = takoTables[enemyIndex];
        }
        else if (enemy != null && enemy.name == "kani(Clone)")
        {
            GameObject enemychild = enemy.transform.GetChild(0).gameObject;
            SpriteRenderer enemychildSprite = enemychild.GetComponent<SpriteRenderer>();
            enemychildSprite.sprite = kaniTables[enemyIndex];
        }
        else if (enemy != null && enemy.name == "ika(Clone)")
        {
            GameObject enemychild = enemy.transform.GetChild(0).gameObject;
            SpriteRenderer enemychildSprite = enemychild.GetComponent<SpriteRenderer>();
            enemychildSprite.sprite = ikaTables[enemyIndex];
        }
    }
    //敵の攻撃
    IEnumerator enemyattack()
    {
        switch (attackcount)
        {
            case 1:
                StartCoroutine(enemyattack1());
                break;
            case 2:
                StartCoroutine(enemyattack2());
                break;
            case 3:
                StartCoroutine(enemyattack3());
                break;
            default:
                break;
        }
        attackcount++;
        if (attackcount>3)
        {
            attackcount = 1;
        }
        yield return null;
    }
    //敵の攻撃1
    IEnumerator enemyattack1()
    {
        GameObject EnemyNearobj;
        // 最も近いオブジェクトの距離を代入するための変数
        float nearDistance = 0;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (enemyTables[j, i] != null)
                {
                    enemyattackTables[i] = enemyTables[j, i];
                    break;
                }

            }
        }
        if (playerobj != null)
        {
            for (int i = 0; i < enemyattackTables.Length; i++)
            {
                if (enemyattackTables[i] != null)
                {
                    float distance = Vector3.Distance(enemyattackTables[i].transform.position, playerobj.transform.position);
                    if (nearDistance == 0 || nearDistance > distance)
                    {

                        // nearDistanceを更新
                        nearDistance = distance;

                        // searchTargetObjを更新
                        EnemyNearobj = enemyattackTables[i];
                        enemybulletPoint = new Vector3(EnemyNearobj.transform.position.x, EnemyNearobj.transform.position.y - 20, EnemyNearobj.transform.position.z);
                    }
                }
            }
            if (enemybullet[0] == null)
            {
                //弾を撃つ
                enemybullet[0] = Instantiate(enemybullet1Prefab, enemybulletPoint, transform.rotation);
                StartCoroutine(enemyattackbullet(enemybullet[0]));
            }
        }
        yield return null;
    }
    //敵の攻撃2
    IEnumerator enemyattack2()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (enemyTables[j, i] != null)
                {
                    enemyattackTables[i] = enemyTables[j, i];
                    break;
                }

            }
        }
        if (enemyattackTables[Tablecount] != null)
        {
            enemybulletPoint = new Vector3(enemyattackTables[Tablecount].transform.position.x, enemyattackTables[Tablecount].transform.position.y - 20, enemyattackTables[Tablecount].transform.position.z);
            //弾を撃つ
            enemybullet[0] = Instantiate(enemybullet2Prefab, enemybulletPoint, transform.rotation);
            StartCoroutine(enemyattackbullet(enemybullet[0]));
        }
            yield return null;
    }
    //敵の攻撃3
    IEnumerator enemyattack3()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (enemyTables[j, i] != null)
                {
                    enemyattackTables[i] = enemyTables[j, i];
                    break;
                }

            }
        }
        if (enemyattackTables[Tablecount] != null)
        {
            enemybulletPoint = new Vector3(enemyattackTables[Tablecount].transform.position.x, enemyattackTables[Tablecount].transform.position.y - 20, enemyattackTables[Tablecount].transform.position.z);
            //弾を撃つ
            enemybullet[0] = Instantiate(enemybullet3Prefab, enemybulletPoint, transform.rotation);
            StartCoroutine(enemyattackbullet(enemybullet[0]));
            Tablecount++;
        }
        if (Tablecount >= 11)
        {
            Tablecount = 0;
        }
        yield return null;
    }
    //敵の弾の-110まで飛ぶ
    IEnumerator enemyattackbullet(GameObject Bullet)
    {

        int[] icounttime = { 2, 1 };
        int icount = 0;
        int a = 0;

        //飛ばした弾がy-100に行くまで待機
        while (Bullet != null && Bullet.transform.position.y >= -115)
        {
            if (icount >= icounttime[a]) {
                if (a == 0)
                {
                    a = 1;
                }
                else
                {
                    a = 0;
                }
                PillboxHit(Bullet);
                if (Bullet.transform.position.y >= -115)
                {
                    Bullet.transform.position += new Vector3(0, enemybulletspeed, 0);
                    PillboxHit(Bullet);
                }
                if (Bullet.transform.position.y >= -115)
                {
                    Bullet.transform.position += new Vector3(0, enemybulletspeed, 0);
                    PillboxHit(Bullet);
                }
                if (Bullet.transform.position.y >= -115)
                {
                    Bullet.transform.position += new Vector3(0, enemybulletspeed, 0);
                    PillboxHit(Bullet);
                }
                if (Bullet.transform.position.y >= -115)
                {
                    Bullet.transform.position += new Vector3(0, enemybulletspeed, 0);
                    PillboxHit(Bullet);
                }
                Bulletanime(Bullet);
                if (Bullet != null)
                {
                    yield return null;
                }
                //yield return null;
            }
            yield return null;
            yield return null;
            icount++;
        }
        //弾オブジェクトが消えてなかったら
        if (Bullet != null)
        {
            Vector3 BulletHitPos = Bullet.transform.position;
            BulletHitPos.z -= 5.0f;
            //爆発を出現させる
            bulletexplosionobj = Instantiate(enemybulletexplosion, BulletHitPos, transform.rotation);
            //0.2秒後に消す
            Destroy(bulletexplosionobj, 0.2f);
            //爆発を出現させる
            bulletexplosionobj = Instantiate(bulletexplosion, Bullet.transform.position, transform.rotation);

            Destroy(Bullet);//弾を消す
        }
        yield return null;
    }

    void Bulletanime(GameObject Bullet)
    {
        SpriteRenderer BulletSprite = Bullet.GetComponent<SpriteRenderer>();
        if (Bullet.name == "EnemyBullet1(Clone)")
        {
            BulletSprite.sprite = EnemyBullet1Images[enemybulletIndex];
        }
        else if (Bullet.name == "EnemyBullet2(Clone)")
        {
            BulletSprite.sprite = EnemyBullet2Images[enemybulletIndex];
        }
        else
        {
            BulletSprite.sprite = EnemyBullet3Images[enemybulletIndex];
        }
        enemybulletIndex++;
        if (enemybulletIndex == 4)
        {
            enemybulletIndex = 0;
        }
    }


    //トーチカの当たり判定
    void PillboxHit(GameObject Bullet)
    {
        for (int o = 0; o < iPillbox1.GetLength(0); o++)
        {
            //1ドットずつ判定する
            for (int i = 0; i < iPillbox1.GetLength(1); i++)
            {
                for (int j = 0; j < iPillbox1.GetLength(2); j++)
                {
                    if (iPillbox1[o, i, j] == 1)
                    {
                        Vector3 PillboxHitPos = PillboxObj[o].transform.position;
                        PillboxHitPos.x += j;
                        PillboxHitPos.y -= i;
                        Vector3 BulletHitPos = Bullet.transform.position;
                        BulletHitPos.y -= 3.0f;
                        if (BulletHitPos == PillboxHitPos)
                        {
                            BulletHitPos.z -= 5.0f;
                            //爆発を出現させる
                            bulletexplosionobj = Instantiate(enemybulletexplosion, BulletHitPos, transform.rotation);
                            //0.2秒後に消す
                            Destroy(bulletexplosionobj, 0.2f);
                            for (int k = 0; k < EnemyBulletExplosionSize.GetLength(0); k++)
                            {

                                int m = j + EnemyBulletExplosionSize[k, 0];
                                int n = i + EnemyBulletExplosionSize[k, 1];
                                if (1 >= n)
                                {
                                    n = 1;
                                }
                                if (17 <= n)
                                {
                                    n = 17;
                                }
                                if (0 >= m)
                                {
                                    m = 0;
                                }
                                if (23 <= m)
                                {
                                    m = 23;
                                }
                                iPillbox1[o, n, m] = 0;
                                Vector3 BlackBoxpos = PillboxHitPos;
                                BlackBoxpos.x += EnemyBulletExplosionSize[k, 0] + 0.5f;
                                BlackBoxpos.y += EnemyBulletExplosionSize[k, 1] + 0.5f;
                                BlackBoxpos.z += EnemyBulletExplosionSize[k, 1] + 4.0f;
                                //消えたところを黒くする
                                BlackBoxObj = Instantiate(BlackBoxPrefab, BlackBoxpos, transform.rotation);
                            }
                            Destroy(Bullet);//弾を消す
                        }
                    }
                }
            }
        }
    }
    void PlayerPillboxHit(GameObject Bullet)
    {
        for (int o = 0; o < iPillbox1.GetLength(0); o++)
        {
            //1ドットずつ判定する
            for (int i = 0; i < iPillbox1.GetLength(1); i++)
            {
                for (int j = 0; j < iPillbox1.GetLength(2); j++)
                {
                    if (iPillbox1[o, i, j] == 1)
                    {
                        Vector3 PillboxHitPos = PillboxObj[o].transform.position;
                        PillboxHitPos.x += j;
                        PillboxHitPos.y -= i;
                        Vector3 BulletHitPos = Bullet.transform.position;
                        BulletHitPos.y -= 3.0f;

                        if (BulletHitPos == PillboxHitPos)
                        {
                            //爆発を出現させる
                            //bulletexplosionobj = Instantiate(bulletexplosion, BulletHitPos, transform.rotation);
                            for (int k = 0; k < PlayerBulletExplosionSize.GetLength(0); k++)
                            {
                                int m = j + PlayerBulletExplosionSize[k, 0];
                                int n = i + PlayerBulletExplosionSize[k, 1];
                                if (1 >= n)
                                {
                                    n = 1;
                                }
                                if (17 <= n)
                                {
                                    n = 17;
                                }
                                if (0 >= m)
                                {
                                    m = 0;
                                }
                                if (23 <= m)
                                {
                                    m = 23;
                                }
                                iPillbox1[o, n, m] = 0;

                                Vector3 BlackBoxpos = PillboxHitPos;
                                BlackBoxpos.x += PlayerBulletExplosionSize[k, 0] + 0.5f;
                                BlackBoxpos.y += PlayerBulletExplosionSize[k, 1] + 0.5f;
                                //BlackBoxpos.z += 1;
                                //消えたところを黒くする
                                BlackBoxObj = Instantiate(BlackBoxPrefab, BlackBoxpos, transform.rotation);
                            }
                            //0.2秒後に消す
                            //Destroy(bulletexplosionobj, 0.2f);
                            Destroy(Bullet);//弾を消す
                            for (int a = 0; a < iPillbox1.GetLength(0); a++)
                            {
                                string str = "";
                                for (int b = 0; b < iPillbox1.GetLength(1); b++)
                                {
                                    str = str + iPillbox1[o, a, b] + "";
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void enemyPillboxHit(GameObject box)
    {
        //トーチカを1ドットずつ判定する
        for (int o = 0; o < iPillbox1.GetLength(0); o++)
        {
            for (int i = 0; i < iPillbox1.GetLength(1); i++)
            {
                for (int j = 0; j < iPillbox1.GetLength(2); j++)
                {
                    //判定がオンになっている部分
                    if (iPillbox1[o, i, j] == 1)
                    {
                        Vector3 PillboxHitPos = PillboxObj[o].transform.position;
                        PillboxHitPos.x += j;
                        PillboxHitPos.y -= i;
                        for (int p = 0; p < 16; p++)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                Vector3 boxHitPos = box.transform.position;
                                boxHitPos.x += p;
                                boxHitPos.y -= l;
                                if (boxHitPos == PillboxHitPos)
                                {
                                    iPillbox1[o, i, j] = 0;

                                }
                            }
                        }
                    }
                    
                }
            }
        }
    }

    void lifedestroy()
    {
        for (int i = 0; i < playerImages.Length; i++)
        {
            if (playerImages[i] != null)
            {
                Destroy(playerImages[i].gameObject);
            }
        }
    }

    //残機の分playerUIを表示
    void lifedisplay()
    {

        for (int i = 0; i < life - 1; i++)
        {
            playerImages[i] = Instantiate(playerImage, new Vector3(-90 + 15 * i, -124, 0.0f), Quaternion.identity, canvas.transform);
        }
        if (life >= 0)
        {
            lifeCountDeisplay();
        }
    }
    //現在のライフの表示
    void lifeCountDeisplay()
    {
        lifeobj.GetComponent<SpriteRenderer>().sprite = numbersOBJ[life];
    }

    //ゲームクリア
    public void GameClear()
    {
        Gameclear = true;
        iLevel++;
        if (iLevel > 9)
        {
            iLevel = 2;
        }
        PlayerPrefs.SetInt("Level", iLevel);
        PlayerPrefs.SetInt("score1", score);
    }
    //ゲームオーバー
    public void GameOver()
    {
        Gameover = true;
    }

    IEnumerator GameClearChangeScene()
    {
        for (int i = 0; i <= 28; i++)
        {
            hideSquare.transform.position += new Vector3(8, 0, 0);
            yield return null;
        }
        yield return null;
        GameChangeScene();
    }

    public void GameChangeScene()
    {
        //一度も押されていないか
        if (!sceneButton)
        {
            //押された
            sceneButton = true;
            //ゲームシーンに移動
            SceneManager.LoadScene("game");
        }
    }

    IEnumerator GameOverChangeScene()
    {
        for(int i = 0; i <= Gameovermozi.Length; i++)
        {
            Gameovertext.text = Gameovermozi.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i <=28 ; i++)
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
}
 