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
    [SerializeField, Tooltip("�L�����o�X")]
    private GameObject canvas;
    int aaa;
    public bool Gameover;//�Q�[���I�[�o�[�t���O
    public bool Gameclear;//�Q�[���N���A�t���O

    [SerializeField, Tooltip("���l�v���C��")]
    private int pleayernumber = 1;

    [SerializeField, Tooltip("�G��|������")]
    private int iLevel;

    [SerializeField, Tooltip("�V�[���ړ��̃{�^�������������H")]
    private bool sceneButton;


    [SerializeField, Tooltip("�n�ʃv���n�u")]
    private GameObject GroundgPrefab;

    [SerializeField, Tooltip("�v���C���[")]
    private GameObject playerobj;
    [SerializeField, Tooltip("�v���C���[�v���n�u")]
    private GameObject playerfab;
    [SerializeField, Tooltip("�v���C���[�̑���")]
    private float speed = 50f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    private GameObject explosionobj;
    [SerializeField, Tooltip("�����̉摜")]
    private Sprite[] explosionSprite;

    [SerializeField, Tooltip("���@�̔���")]
    public Sprite[] PlayerexplosionTables;
    [SerializeField, Tooltip("���@�̔����C���f�b�N�X")]
    public int iPlayerexplosionIndex = 0;

    [SerializeField, Tooltip("���G���ǂ���")]
    private bool binvincible=false;

    [SerializeField, Tooltip("�v���C���[�̎c�@")]
    private int life = 3;
    [SerializeField, Tooltip("�c�@�\���I�u�W�F")]
    private GameObject lifeobj;

    [SerializeField, Tooltip("�����ꏊ")]
    private Vector3 revivalPoint = new Vector3(-90, -104, 0);

    [SerializeField, Tooltip("�e�̔��ˏꏊ")]
    private Vector3 bulletPoint;
    [SerializeField, Tooltip("�e�̈ʒu����x")]
    private int bulletPointx = 1;
    [SerializeField, Tooltip("�e�̈ʒu����y")]
    private int bulletPointy = 5;
    [SerializeField, Tooltip("�e")]
    private GameObject bulletPrefab;
    [SerializeField, Tooltip("�e�̑���")]
    private float bulletspeed = 1f;
    [SerializeField, Tooltip("�e����������")]
    private int bulletnumber = 6;
    [SerializeField, Tooltip("�e�𔭎˂��Ă�������")]
    private bool bulletflag = true;
    [SerializeField, Tooltip("�e��Rigidbody")]
    private Rigidbody2D bulletrb;
    [SerializeField, Tooltip("�����v���n�u")]
    private GameObject bulletexplosion;
    [SerializeField, Tooltip("���@�e�����v���n�u")]
    private GameObject playerbulletexplosion;
    [SerializeField, Tooltip("�G�e�����v���n�u")]
    private GameObject enemybulletexplosion;
    [SerializeField, Tooltip("�G�����v���n�u")]
    private GameObject enemyexplosion;
    [SerializeField, Tooltip("�����I�u�W�F")]
    private GameObject bulletexplosionobj;
    [SerializeField, Tooltip("black box�v���n�u")]
    private GameObject BlackBoxPrefab;
    [SerializeField, Tooltip("black box�I�u�W�F")]
    private GameObject BlackBoxObj;
    [SerializeField, Tooltip("black boxBig�v���n�u")]
    private GameObject BlackBoxBigPrefab;
    [SerializeField, Tooltip("black boxBig�v���n�u")]
    private GameObject boxobj;

    [SerializeField, Tooltip("UFO�o�Ă���")]
    private bool UFOspawn = false;

    [SerializeField, Tooltip("UFO�v���n�u")]
    private GameObject UFOprefab;
    [SerializeField, Tooltip("UFO�I�u�W�F")]
    private GameObject UFOobj;
    [SerializeField, Tooltip("UFO�����v���n�u")]
    private GameObject UFOexplosion;

    [SerializeField, Tooltip("UFO�o���ꏊ")]
    public Vector3 spawnpos;
    [SerializeField, Tooltip("x���WUFO�o���ꏊ")]
    public float spawnposx = -110;

    [SerializeField, Tooltip("UFO�o������")]
    public float spawnInterval = 25f;

    [SerializeField, Tooltip("����")]
    private float timer = 0f;

    [SerializeField, Tooltip("UFO�X�s�[�h")]
    private float UFOspeed = 0.1f;

    [SerializeField, Tooltip("UFO�_��")]
    private int[] UFOscore = { 100, 100, 100, 50, 150, 100, 100, 50, 50, 100, 150, 100, 100, 50, 300 };
    [SerializeField, Tooltip("UFO�|�C���g�I�u�W�F")]
    private GameObject[] pointobj;

    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> ikaTables;
    [SerializeField, Tooltip("���ɉ摜")]
    public List<Sprite> kaniTables;
    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> takoTables;
    [SerializeField, Tooltip("�c���I�u�W�F")]
    public GameObject[] afterimageobj;

    [SerializeField, Tooltip("�摜�̃C���f�b�N�X")]
    private int enemyIndex = 0;

    [SerializeField, Tooltip("�����v���n�u")]
    private GameObject takoPrefab;
    [SerializeField, Tooltip("���Ƀv���n�u")]
    private GameObject kaniPrefab;
    [SerializeField, Tooltip("�����v���n�u")]
    private GameObject ikaPrefab;

    [SerializeField, Tooltip("�s��")]
    private int rows = 5;
    [SerializeField, Tooltip("��")]
    private int columns = 11;

    [SerializeField, Tooltip("�G�z��")]
    public GameObject[,] enemyTables;

    [SerializeField, Tooltip("�G�U���z��")]
    public GameObject[] enemyattackTables;

    [SerializeField, Tooltip("�G�̈ʒu")]
    private Vector3 enemyPos;
    [SerializeField, Tooltip("�[�̓G�̈ʒu")]
    private Vector3 enemyedgePos;

    [SerializeField, Tooltip("���E�ǂ���Ɉړ����邩")]
    private bool moveright = true;//false:���ړ��@true�F�E�ړ�
    [SerializeField, Tooltip("���Ɉړ����邩")]
    private bool movedown = false;
    [SerializeField, Tooltip("�q�b�g�X�g�b�v")]
    private bool hitstop = false;
    [SerializeField, Tooltip("�G��|������")]
    private int iNumberDownEnemy = 0;


    [SerializeField, Tooltip("�v���C���[�̏����_�ȉ��̍��W����������")]
    private Vector3 newPosition;

    [SerializeField, Tooltip("�G�̑��x")]
    private int enemyspeed = 3;
    [SerializeField, Tooltip("�G�̃X�s�[�h")]
    private float espeed = 0.01f;

    [SerializeField, Tooltip("�G�̒e1")]
    private GameObject enemybullet1Prefab;
    [SerializeField, Tooltip("�G�̒e2")]
    private GameObject enemybullet2Prefab;
    [SerializeField, Tooltip("�G�̒e3")]
    private GameObject enemybullet3Prefab;
    [SerializeField, Tooltip("�G�̒e")]
    private GameObject[] enemybullet = { null, null };
    [SerializeField, Tooltip("�G�̒e�̔��ˏꏊ")]
    private Vector3 enemybulletPoint;
    [SerializeField, Tooltip("�G�̒e�̑���")]
    private float enemybulletspeed = -1f;
    [SerializeField, Tooltip("�G�̒e1�摜")]
    public List<Sprite> EnemyBullet1Images;
    [SerializeField, Tooltip("�G�̒e2�摜")]
    public List<Sprite> EnemyBullet2Images;
    [SerializeField, Tooltip("�G�̒e3�摜")]
    public List<Sprite> EnemyBullet3Images;
    [SerializeField, Tooltip("�G�̍U���e�[�u��")]
    private int[] iEnemyAttackTable1 = { 1, 7, 1, 1, 1, 4, 11, 1, 6, 3, 1, 1, 11, 9, 2, 8, 2 };
    [SerializeField, Tooltip("�G�̍U���e�[�u��")]
    private int[] iEnemyAttackTable2 = { 11, 1, 6, 3, 1, 1, 11, 9, 2, 8, 2, 11, 4, 7, 10, 5, 2 };
    [SerializeField, Tooltip("�e�[�u���J�E���g")]
    private int Tablecount = 0;
    [SerializeField, Tooltip("�U�����J�E���g")]
    private int attackcount = 1;
    [SerializeField, Tooltip("�X�R�A")]
    private int score = 0;

    [SerializeField, Tooltip("�^�R�X�R�A")]
    private int takoscore = 10;
    [SerializeField, Tooltip("�J�j�X�R�A")]
    private int kaniscore = 20;
    [SerializeField, Tooltip("�C�J�X�R�A")]
    private int ikascore = 30;

    [SerializeField, Tooltip("�n�C�X�R�A")]
    public int hiscore;
    [SerializeField, Tooltip("hiscore�e�L�X�g")]
    public TextMeshProUGUI hiscoretext;
    [SerializeField, Tooltip("life�e�L�X�g")]
    public TextMeshProUGUI lifetext;

    [SerializeField, Tooltip("�����I�u�W�F")]
    private Sprite[] numbersOBJ;
    [SerializeField, Tooltip("�X�R�A�I�u�W�F")]
    private GameObject[] scoreobj;
    [SerializeField, Tooltip("�n�C�X�R�A�I�u�W�F")]
    private GameObject[] hiscoreobj;

    [SerializeField, Tooltip("player�摜")]
    public Image[] playerImages = { null, null, null, null, null };
    [SerializeField, Tooltip("player�摜")]
    public Image playerImage;

    [SerializeField, Tooltip("�Q�[���I�[�o�[�e�L�X�g")]
    public TextMeshProUGUI Gameovertext;
    [SerializeField, Tooltip("�Q�[���I�[�o�[�e�L�X�g")]
    public string Gameovermozi = "Game Over";
    [SerializeField, Tooltip("�B���I�u�W�F")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("�G��|����")]
    public AudioClip sound1;
    public AudioSource audioSource1;
    [SerializeField, Tooltip("�G�ړ���")]
    public AudioClip[] sound2;
    public AudioSource audioSource2;
    [SerializeField, Tooltip("���@����")]
    public AudioClip sound3;
    public AudioSource audioSource3;
    [SerializeField, Tooltip("UFO����")]
    public AudioClip sound4;
    public AudioSource audioSource4;

    [SerializeField, Tooltip("1�x�̂�")]
    private bool onetime = false;

    [SerializeField, Tooltip("������")]
    private bool bResurrecting = false;

    public int enemybulletIndex = 0;



    [SerializeField, Tooltip("�g�[�`�J�I�u�W�F")]
    private GameObject[] PillboxObj;

    [SerializeField, Tooltip("�g�[�`�J�̔z��")]
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


    [SerializeField, Tooltip("���@�̔����̔z��")]
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

    [SerializeField, Tooltip("�G�̔����̔z��")]
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
        //�X�R�A�̕\���������擾
        for (int i = 0; i < 5; i++)
        {
            scoreobj[i] = GameObject.Find("UI/score/Sc" + i);
        }
        //�n�C�X�R�A�̕\���������擾
        for (int i = 0; i < 5; i++)
        {
            hiscoreobj[i] = GameObject.Find("UI/Hiscore/HiSc" + i);
        }
        //�c�@�̕\���������擾
        lifeobj = GameObject.Find("UI/life");
        //�n�ʐ���
        GroundgGnerate();
        iNumberDownEnemy = 0;
        playerobj = GameObject.Find("player");//�v���C���[��T��
        enemyTables = new GameObject[rows, columns];
        enemyattackTables = new GameObject[columns];
        //������
        Gameover = false;
        Gameclear = false;
        //���݂̃��x��
        iLevel = PlayerPrefs.GetInt("Level", 0);
        //�X�R�A�̎擾
        score = PlayerPrefs.GetInt("score" + pleayernumber, 0);
        //�G���X�|�[��������
        StartCoroutine(enemySpawn());
        //�n�C�X�R�A�̕\��
        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        HiScoreDisplay();
        //Rigidbody�擾
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

            // ���̊Ԋu�ŃI�u�W�F�N�g�𐶐�
            if (timer >= spawnInterval && !Gameover && !Gameclear)
            {
                UFOSpawnObject();
                timer = 0f;
            }
            playermove();//�v���C���[�̈ړ�
            playerbullet();//�e�̔���  
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
                Debug.Log("���G���[�h");
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

    //�n�ʂ𐶐�
    void GroundgGnerate()
    {
        for (int i = 0; i < 224; i++)
        {
            Vector3 Groundgpos = new Vector3(-111.5f + i, -119.5f, 0f);
            playerobj = Instantiate(GroundgPrefab, Groundgpos, transform.rotation);
        }
    }

    //�G�𐶐�
    IEnumerator enemySpawn()
    {
        yield return new WaitForSeconds(0.2f);
        int enemyheight = -16;

        // �Q�[���I�u�W�F�N�g�̐����Ɣz��ւ̑��
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

    //UFO�o��������
    void UFOSpawnObject()
    {
        spawnInterval = 25.0f;
        spawnpos = new Vector3(spawnposx, 96, 0);
        // �v���n�u����I�u�W�F�N�g�𐶐�
        UFOobj = Instantiate(UFOprefab, spawnpos, Quaternion.identity);
        UFOspawn = true;

    }
    //UFO�̓���
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
    //�v���C���[�o��
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


    //�v���C���[�̈ړ�
    void playermove()
    {
        if (!bResurrecting)
        {
            var current = Keyboard.current;
            // A�L�[�i���ړ��j 
            if (playerobj != null && current.aKey.isPressed && playerobj.transform.position.x > -94)
            {
                // newPosition = newPosition - speed * transform.right * Time.deltaTime;
                newPosition = newPosition - speed * transform.right;
                playerobj.transform.position = new Vector3(Mathf.FloorToInt(newPosition.x), newPosition.y, newPosition.z);
            }

            // D�L�[�i�E�ړ��j
            if (playerobj != null && current.dKey.isPressed && playerobj.transform.position.x < 87)
            {
                // newPosition = newPosition + speed * transform.right * Time.deltaTime;
                newPosition = newPosition + speed * transform.right;
                playerobj.transform.position = new Vector3(Mathf.CeilToInt(newPosition.x), newPosition.y, newPosition.z);
            }
        }
    }
    //�|���ꂽ��
    public void playerdown(GameObject enemyBullet, GameObject Player)
    {
        if (!binvincible)
        {
            //��(sound3)��炷
            audioSource3.PlayOneShot(sound3);
            Destroy(enemyBullet);//�e������
                                 // Destroy(Player);//player������
                                 //�������o��������
                                 //GameObject Playerexplosion = Instantiate(bulletexplosion, Player.transform.position, transform.rotation);
            StartCoroutine(PlayerExplosion(Player));
            bResurrecting = true;
            StartCoroutine(revival());
            lifedestroy();
            lifedisplay();
        }
    }
    //�����A�j���[�V����
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
        Destroy(Playerexplosion);//player������
        yield return null;
    }
    //player�̒e�ƓG�̒e�������������̏���
    public void BulletCollision(GameObject enemyBullet, GameObject PlayerBullet)
    {
        //�ǂ̒e�������̐���
        int iprobabilityNumber= Random.Range(0, 4);
        //��������
        if (iprobabilityNumber <= 1)
        {
            Destroy(enemyBullet);
            Destroy(PlayerBullet);
            //�������o��������
            bulletexplosionobj = Instantiate(playerbulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
            //�������o��������
            bulletexplosionobj = Instantiate(enemybulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2�b��ɏ���11
            Destroy(bulletexplosionobj, 0.2f);
        }
        //player�̒e����
        else if (iprobabilityNumber == 2)
        {
            Destroy(PlayerBullet);
            //�������o��������
            bulletexplosionobj = Instantiate(playerbulletexplosion, PlayerBullet.transform.position, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
        }
        //�G�̒e����
        else
        {
            Destroy(enemyBullet);
            //�������o��������
            bulletexplosionobj = Instantiate(enemybulletexplosion, enemyBullet.transform.position, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
        }
    }
    //����
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
    
    //�e�̔���
    void playerbullet()
    {
        var current = Keyboard.current;
        //�e�����Ă�󋵂ŃX�y�[�X�L�[�������ꂽ��
        if (current.spaceKey.wasPressedThisFrame && bulletflag == true && playerobj != null && hitstop == false && !bResurrecting)
        {
            //�e�����ĂȂ�����
            bulletflag = false;
            bulletnumber++;
            if (bulletnumber > UFOscore.Length - 1)
            {
                bulletnumber = 0;
            }
            //�e�̌��ꏊ
            bulletPoint = new Vector3(playerobj.transform.position.x + bulletPointx, playerobj.transform.position.y + bulletPointy, playerobj.transform.position.z);
            //�e������
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint, transform.rotation);
            //�e���i�߂���
            StartCoroutine(DestroyBulletdistance(bullet));
        }
    }

    //�e��y100�ɍs������e������
    IEnumerator DestroyBulletdistance(GameObject Bullet)
    {
        //��΂����e��y100�ɍs���܂őҋ@
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
        //�e�I�u�W�F�N�g�������ĂȂ�������
        if (Bullet != null)
        {
            //�������o��������
            bulletexplosionobj = Instantiate(playerbulletexplosion, Bullet.transform.position, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
            Destroy(Bullet);//�e������
            bulletflag = true;//�e�����Ă�悤�ɂ���
        }
        if (Bullet == null)
        {
            bulletflag = true;//�e�����Ă�悤�ɂ���
        }
    }
    //�G��|��
    public void enemyknockdown(GameObject Bullet, GameObject Enemy)
    {
        hitstop = true;
        Destroy(Bullet);//�e������
        if ("UFO(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            StartCoroutine(UFOpointdisplay(Enemy));
        }
        else
        {
            //�������o��������
            bulletexplosionobj = Instantiate(enemyexplosion, Enemy.transform.position, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
        }
        //�G�ɂ����score��ǉ�����
        if ("tako(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //��(sound1)��炷
            audioSource1.PlayOneShot(sound1);
            score += takoscore;
            iNumberDownEnemy++;
        }
        if ("kani(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //��(sound1)��炷
            audioSource1.PlayOneShot(sound1);
            score += kaniscore;
            iNumberDownEnemy++;
        }
        if ("ika(Clone)" == Enemy.gameObject.transform.parent.gameObject.name)
        {
            //��(sound1)��炷
            audioSource1.PlayOneShot(sound1);
            score += ikascore;
            iNumberDownEnemy++;
        }
        //�ϐ��̕\��
        ScoreDisplay();

        if (iNumberDownEnemy == 55)
        {
            GameClear();
        }

        if (score > hiscore)
        {
            HiscoreSave();
        }

        //�G������
        Destroy(Enemy.gameObject.transform.parent.gameObject);

      
    }

    //UFO�̃|�C���g�\��
    IEnumerator UFOpointdisplay(GameObject Enemy)
    {
        //�������o��������
        bulletexplosionobj = Instantiate(UFOexplosion, Enemy.transform.position, transform.rotation);
        //0.2�b��ɏ���
        Destroy(bulletexplosionobj, 0.2f);
        //��(sound4)��炷
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

    //�X�R�A�̕\��
    void ScoreDisplay()
    {
        //�X�R�A�̎w�肵�����̕ۑ��ϐ�
        int scoredigit;
        //�ꌅ���\�����Ĕ����o��
        for (int i = 5; i >= 1; i--)
        {
            //�����Ƃ̒l���o��
            scoredigit = (int)(score / Mathf.Pow(10, i - 1)) % 10;
            //�I�u�W�F�N�g��sprite��ύX
            scoreobj[i-1].GetComponent<SpriteRenderer>().sprite = numbersOBJ[scoredigit];
        }
    }

    //�n�C�X�R�A�̕\��
    void HiScoreDisplay()
    {
        //�n�C�X�R�A�̎w�肵�����̕ۑ��ϐ�
        int scoredigit;
        //�ꌅ���\�����Ĕ����o��
        for (int i = 5; i >= 1; i--)
        {
            //�����Ƃ̒l���o��
            scoredigit = (int)(hiscore / Mathf.Pow(10, i - 1)) % 10;
            //�I�u�W�F�N�g��sprite��ύX
            hiscoreobj[i - 1].GetComponent<SpriteRenderer>().sprite = numbersOBJ[scoredigit];
        }
    }

    //�n�C�X�R�A��ۑ�
    void HiscoreSave()
    {
        hiscore = score;
       // hiscore = 0;
        //�Z�[�u
        PlayerPrefs.SetInt("hiscore", hiscore);
        
    }



    //�G�����E�ɏ��Ԃɓ�����
    IEnumerator enemymove()
    {
        int enemyaudioIndex = 0;
        //�������[�v
        while (true)
        {
            if (!Gameover && !Gameclear)
            {

                //��(sound2)��炷
                audioSource2.PlayOneShot(sound2[enemyaudioIndex]);
                enemyaudioIndex++;
                if (enemyaudioIndex > 3)
                {
                    enemyaudioIndex = 0;
                }

                //�G�̃A�j���[�V���������݂ɕς���
                if (enemyIndex == 0)
                {
                    enemyIndex = 1;
                }
                else if (enemyIndex == 1)
                {
                    enemyIndex = 0;
                }
                //���Ԃɓ�����
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
                            //�����邩�ǂ���
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
                            //�E�ړ����ǂ���
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
    //�E�ړ��ɂ����邩���ړ��ɂ����邩
    void enemyposjudge()
    {
        //�z�񕪃��[�v
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
    //�G�̃A�j���[�V����
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
    //�G�̍U��
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
    //�G�̍U��1
    IEnumerator enemyattack1()
    {
        GameObject EnemyNearobj;
        // �ł��߂��I�u�W�F�N�g�̋����������邽�߂̕ϐ�
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

                        // nearDistance���X�V
                        nearDistance = distance;

                        // searchTargetObj���X�V
                        EnemyNearobj = enemyattackTables[i];
                        enemybulletPoint = new Vector3(EnemyNearobj.transform.position.x, EnemyNearobj.transform.position.y - 20, EnemyNearobj.transform.position.z);
                    }
                }
            }
            if (enemybullet[0] == null)
            {
                //�e������
                enemybullet[0] = Instantiate(enemybullet1Prefab, enemybulletPoint, transform.rotation);
                StartCoroutine(enemyattackbullet(enemybullet[0]));
            }
        }
        yield return null;
    }
    //�G�̍U��2
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
            //�e������
            enemybullet[0] = Instantiate(enemybullet2Prefab, enemybulletPoint, transform.rotation);
            StartCoroutine(enemyattackbullet(enemybullet[0]));
        }
            yield return null;
    }
    //�G�̍U��3
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
            //�e������
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
    //�G�̒e��-110�܂Ŕ��
    IEnumerator enemyattackbullet(GameObject Bullet)
    {

        int[] icounttime = { 2, 1 };
        int icount = 0;
        int a = 0;

        //��΂����e��y-100�ɍs���܂őҋ@
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
        //�e�I�u�W�F�N�g�������ĂȂ�������
        if (Bullet != null)
        {
            Vector3 BulletHitPos = Bullet.transform.position;
            BulletHitPos.z -= 5.0f;
            //�������o��������
            bulletexplosionobj = Instantiate(enemybulletexplosion, BulletHitPos, transform.rotation);
            //0.2�b��ɏ���
            Destroy(bulletexplosionobj, 0.2f);
            //�������o��������
            bulletexplosionobj = Instantiate(bulletexplosion, Bullet.transform.position, transform.rotation);

            Destroy(Bullet);//�e������
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


    //�g�[�`�J�̓����蔻��
    void PillboxHit(GameObject Bullet)
    {
        for (int o = 0; o < iPillbox1.GetLength(0); o++)
        {
            //1�h�b�g�����肷��
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
                            //�������o��������
                            bulletexplosionobj = Instantiate(enemybulletexplosion, BulletHitPos, transform.rotation);
                            //0.2�b��ɏ���
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
                                //�������Ƃ������������
                                BlackBoxObj = Instantiate(BlackBoxPrefab, BlackBoxpos, transform.rotation);
                            }
                            Destroy(Bullet);//�e������
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
            //1�h�b�g�����肷��
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
                            //�������o��������
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
                                //�������Ƃ������������
                                BlackBoxObj = Instantiate(BlackBoxPrefab, BlackBoxpos, transform.rotation);
                            }
                            //0.2�b��ɏ���
                            //Destroy(bulletexplosionobj, 0.2f);
                            Destroy(Bullet);//�e������
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
        //�g�[�`�J��1�h�b�g�����肷��
        for (int o = 0; o < iPillbox1.GetLength(0); o++)
        {
            for (int i = 0; i < iPillbox1.GetLength(1); i++)
            {
                for (int j = 0; j < iPillbox1.GetLength(2); j++)
                {
                    //���肪�I���ɂȂ��Ă��镔��
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

    //�c�@�̕�playerUI��\��
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
    //���݂̃��C�t�̕\��
    void lifeCountDeisplay()
    {
        lifeobj.GetComponent<SpriteRenderer>().sprite = numbersOBJ[life];
    }

    //�Q�[���N���A
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
    //�Q�[���I�[�o�[
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
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //�Q�[���V�[���Ɉړ�
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
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //��Փx�V�[���Ɉړ�
            SceneManager.LoadScene("title");
        }
    }
}
 