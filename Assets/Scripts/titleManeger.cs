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
    [SerializeField, Tooltip("PLAY�e�L�X�g�I�u�W�F")]
    private GameObject PLAYTextObj;
    [SerializeField, Tooltip("PLAY�e�L�X�g�I�u�W�F")]
    private GameObject[] PLAYTextObjs;
    [SerializeField, Tooltip("SPACE INVADERS�e�L�X�g�I�u�W�F")]
    private GameObject SPACEINVADERSTextObj;
    [SerializeField, Tooltip("SPACE INVADERS�e�L�X�g�I�u�W�F")]
    private GameObject[] SPACEINVADERSTextObjs;
    [SerializeField, Tooltip("SCORE�e�L�X�g�I�u�W�F")]
    private GameObject SCORETextObj;
    [SerializeField, Tooltip("SPACE INVADERS�e�L�X�g�I�u�W�F")]
    private GameObject pointsTextObj;
    [SerializeField, Tooltip("SPACE INVADERS�e�L�X�g�I�u�W�F")]
    private GameObject[] pointsTextObjs;

    [SerializeField, Tooltip("����")]
    private GameObject ikaObj;
    [SerializeField, Tooltip("�ty")]
    private GameObject yObj;

    [SerializeField, Tooltip("�V�[���ړ��̃{�^�������������H")]
    private bool sceneButton;

    [SerializeField, Tooltip("�B���I�u�W�F")]
    private GameObject hideSquare;

    [SerializeField, Tooltip("�A�j���[�V��������G�I�u�W�F�N�g")]
    private GameObject AnimationEnemy;
    [SerializeField, Tooltip("�摜�̃C���f�b�N�X")]
    private int enemyIndex = 0;
    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> ikaTables;
    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> ikYTables;
    [SerializeField, Tooltip("�����摜")]
    public List<Sprite> ikaYreturnTables;

    [SerializeField, Tooltip("�A�j���[�V�������邩�ǂ���")]
    private int iAnimationOK = 0;

    [SerializeField, Tooltip("�R�C���̐�")]
    private int coin;
    [SerializeField, Tooltip("�R�C���X�N���v�g")]
    CoinCount coinscript;

    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
        GameObject coinCountobj= GameObject.Find("CoinCount");
        coinscript= coinCountobj.GetComponent<CoinCount>();
        StartCoroutine(TextAppearance());

        //���݂̃��x��
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
            //coin�V�[���Ɉړ�
            SceneManager.LoadScene("coin");
        }

        //titleChangeScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //text��1���o��������
    IEnumerator TextAppearance()
    {
        //�q�I�u�W�F�N�g�����[�v����
        for (int i = 0; i < PLAYTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //�q�I�u�W�F�N�g���擾����
            PLAYTextObjs[i] = PLAYTextObj.transform.GetChild(i).gameObject;
            //�\������
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

        //�q�I�u�W�F�N�g�����[�v����
        for (int i = 0; i < SPACEINVADERSTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //�q�I�u�W�F�N�g���擾����
            SPACEINVADERSTextObjs[i] = SPACEINVADERSTextObj.transform.GetChild(i).gameObject;
            //�\������
            SPACEINVADERSTextObjs[i].gameObject.SetActive(true);     
        }
        yield return new WaitForSeconds(0.5f);
        //�\������
        SCORETextObj.gameObject.SetActive(true);

        //�q�I�u�W�F�N�g�����[�v����
        for (int i = 0; i < pointsTextObj.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            //�q�I�u�W�F�N�g���擾����
            pointsTextObjs[i] = pointsTextObj.transform.GetChild(i).gameObject;
            //�\������
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

    //�G�̃A�j���[�V����
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
        //�G�̃A�j���[�V������ς���
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
        //�G�̃A�j���[�V������ς���
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
        //�G�̃A�j���[�V������ς���
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
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //��Փx�V�[���Ɉړ�
            SceneManager.LoadScene("game");
        }
    }
    public void titleChangeScene()
    {
        //��x��������Ă��Ȃ���
        if (!sceneButton)
        {
            //�����ꂽ
            sceneButton = true;
            //��Փx�V�[���Ɉړ�
            SceneManager.LoadScene("title2");
        }
    }
}
