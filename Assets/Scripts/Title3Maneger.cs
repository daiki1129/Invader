using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;

public class Title3Maneger : MonoBehaviour
{
    [SerializeField, Tooltip("Title3�e�L�X�g�I�u�W�F")]
    private GameObject Title3TextObj;
    [SerializeField, Tooltip("Title3�e�L�X�g�I�u�W�F")]
    private GameObject[] Title3TextObjs;
    [SerializeField, Tooltip("�B���I�u�W�F")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("�V�[���ړ��̃{�^�������������H")]
    private bool sceneButton;

    [SerializeField, Tooltip("�A�j���[�V��������G�I�u�W�F�N�g")]
    private GameObject AnimationEnemy;
    [SerializeField, Tooltip("�摜�̃C���f�b�N�X")]
    private int enemyIndex = 0;
    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> ikaTables;

    [SerializeField, Tooltip("�e�v���n�u")]
    private GameObject enemybulletPrefab;
    [SerializeField, Tooltip("�A�j���[�V�����e")]
    private GameObject enemybullet;
    [SerializeField, Tooltip("C�̕���")]
    private GameObject Cobj;
    [SerializeField, Tooltip("�����v���n�u")]
    private GameObject explosiontPrefab;
    [SerializeField, Tooltip("�����I�u�W�F�N�g")]
    private GameObject explosiontObj;

    [SerializeField, Tooltip("�A�j���[�V�������邩�ǂ���")]
    private int iAnimationOK = 0;

    [SerializeField, Tooltip("�R�C���I�u�W�F")]
    private GameObject coinobj;
    [SerializeField, Tooltip("�R�C���̐�")]
    private int coin;
    [SerializeField, Tooltip("�R�C���X�N���v�g")]
    CoinCount coinscript;

    [SerializeField, Tooltip("����")]
    private GameObject ikaObj;
    [SerializeField, Tooltip("�ty")]
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
            //coin�V�[���Ɉړ�
            SceneManager.LoadScene("coin");
        }
        StartCoroutine(TextAppearance());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //text��1���o��������
    IEnumerator TextAppearance()
    {
        //�q�I�u�W�F�N�g�����[�v����
        for (int i = 0; i < Title3TextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //�q�I�u�W�F�N�g���擾����
            Title3TextObjs[i] = Title3TextObj.transform.GetChild(i).gameObject;
            //�\������
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

    /*/�R�C�������鎞�Q�[���X�^�[�g
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

    //�A�j���[�V����������
    IEnumerator EnemyAnimation()
    {
        while (AnimationEnemy.transform.position.x < 8)
        {
            AnimationEnemy.transform.position += new Vector3(2, 0, 0);
            EnemymoveAnimation();
            yield return null;
            yield return null;
        }
        //�e������
        enemybullet = Instantiate(enemybulletPrefab, AnimationEnemy.transform.position, transform.rotation);
        while (enemybullet.transform.position.y > 20)
        {
            enemybullet.transform.position += new Vector3(0, -1, 0);
            yield return null;
        }
        explosiontObj = Instantiate(explosiontPrefab, Cobj.transform.position, transform.rotation);
        //�e������
        Destroy(enemybullet);
        //����������
        Destroy(Cobj);
        Destroy(explosiontObj, 0.2f);
        Destroy(AnimationEnemy, 0.5f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ChangeScene());
    }
    //�G�̃A�j���[�V������ς���
    void EnemymoveAnimation()
    {
        //�摜�����Ԃɕς���
        enemyIndex++;
        //�Ō�܂ōs������ŏ��ɖ߂�
        if (enemyIndex >= 6)
        {
            enemyIndex = 0;
        }

        SpriteRenderer enemychildSprite = AnimationEnemy.GetComponent<SpriteRenderer>();
        //�摜��؂�ւ���
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
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //��Փx�V�[���Ɉړ�
            SceneManager.LoadScene("title");
        }
    }
    public void gameChangeScene()
    {
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //��Փx�V�[���Ɉړ�
            SceneManager.LoadScene("game");
        }
    }
}
