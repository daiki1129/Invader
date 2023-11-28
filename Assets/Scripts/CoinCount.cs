using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class CoinCount : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̃R�C������")]
    private int iCoin = 0;

    [SerializeField, Tooltip("�����I�u�W�F")]
    private Sprite[] numbersObj;
    [SerializeField, Tooltip("�R�C���I�u�W�F")]
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
        //�R�C������ꂽ��
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
    //�R�C�������̕\��
    void CoinDisplay()
    {
        //�R�C�������̎w�肵�����̕ۑ��ϐ�
        int scoredigit;
        //�ꌅ���\�����Ĕ����o��
        for (int i = 2; i >= 1; i--)
        {
            //�����Ƃ̒l���o��
            scoredigit = (int)(iCoin / Mathf.Pow(10, i - 1)) % 10;
            //�I�u�W�F�N�g��sprite��ύX
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
