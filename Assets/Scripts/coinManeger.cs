using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class coinManeger : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̃R�C������")]
    private int coin = 0;
    [SerializeField, Tooltip("�R�C���X�N���v�g")]
    CoinCount coinscript;
    [SerializeField, Tooltip("�e�L�X�g�I�u�W�F")]
    private GameObject TextObj;
    [SerializeField, Tooltip("1P�p�e�L�X�g�I�u�W�F")]
    private GameObject Text1PObj;
    [SerializeField, Tooltip("1P�ȏ�p�e�L�X�g�I�u�W�F")]
    private GameObject Text2PObj;

    [SerializeField, Tooltip("�V�[���ړ��̃{�^�������������H")]
    private bool sceneButton;
    [SerializeField, Tooltip("�B���I�u�W�F")]
    private GameObject hideSquare;
    [SerializeField, Tooltip("�e�L�X�g�I�u�W�F")]
    private GameObject PlaterTextObj;
    [SerializeField, Tooltip("�X�R�A�I�u�W�F")]
    private GameObject Score;
    [SerializeField, Tooltip("�X�R�A�I�u�W�F")]
    private GameObject Score2;
    [SerializeField, Tooltip("�X�R�A�I�u�W�F")]
    private GameObject Ui;
    [SerializeField, Tooltip("�X�R�A�I�u�W�F")]
    private GameObject[] scoreobj;
    [SerializeField, Tooltip("�����I�u�W�F")]
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
        //�X�R�A�̕\���������擾
        for (int i = 0; i < 5; i++)
        {
            scoreobj[i] = GameObject.Find("UI/score/Sc" + i);
        }
        if (coin == 1)
        {
            //�\������
            Text1PObj.gameObject.SetActive(true);
            Text2PObj.gameObject.SetActive(false);
        }
        else if (coin > 1)
        {
            //�\������
            Text2PObj.gameObject.SetActive(true);
            Text1PObj.gameObject.SetActive(false);
        }
        //�ۑ�
        PlayerPrefs.SetInt("Level", 1);
        //�ۑ�
        PlayerPrefs.SetInt("score1", 0);

    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current.digit1Key.wasPressedThisFrame)
        {
            Score2.gameObject.SetActive(false);
            //�����ꂽ
            sceneButton = true;
            CoinJudgeStart();
        }
        if (current.digit2Key.wasPressedThisFrame)
        {
            Score2.gameObject.SetActive(true);
            //�����ꂽ
            sceneButton = true;
            CoinJudgeStart();
        }
        coin = coinscript.coinreturn();
        if (coin == 1 && !sceneButton)
        {
            //�\������
            Text1PObj.gameObject.SetActive(true);
            Text2PObj.gameObject.SetActive(false);
        }
        else if (coin > 1 && !sceneButton)
        {
            //�\������
            Text2PObj.gameObject.SetActive(true);
            Text1PObj.gameObject.SetActive(false);
        }
    }
    //�X�R�A�̕\��
    void ScoreDisplay()
    {
        //�X�R�A�̎w�肵�����̕ۑ��ϐ�
        int scoredigit;
        //�ꌅ���\�����Ĕ����o��
        for (int i = 0; i <= 4; i++)
        {
            //�I�u�W�F�N�g��sprite��ύX
            scoreobj[i].GetComponent<SpriteRenderer>().sprite = numbersOBJ;
        }
    }

    //�R�C�������鎞�Q�[���X�^�[�g
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
    //�X�^�[�g�e�L�X�g
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
        //��Փx�V�[���Ɉړ�
        SceneManager.LoadScene("game");
    }
}
