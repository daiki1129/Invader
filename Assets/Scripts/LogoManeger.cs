using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LogoManeger : MonoBehaviour
{

    [SerializeField, Tooltip("�B���I�u�W�F")]
    private GameObject hideSquare;
    [SerializeField, Tooltip("���S�C���[�W")]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        //�R���[�`����\��
        StartCoroutine(ChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //���S�}�[�N�̕\��
    IEnumerator Logo()
    {
        //�F���擾
        var c = image.color;
        var ca = c.a;

        //�F���t���܂ŌJ��Ԃ�
        while (ca < 1f)
        {
            //�F�̕t�����x
            ca += 0.7f * Time.deltaTime;
            //�F��ς���
            image.color = new Color(c.r, c.g, c.b, ca);

            yield return null;
        }
        // �w�肳�ꂽ���Ԃ����ҋ@
        yield return new WaitForSeconds(1.0f);
        //�F�������ɂȂ�܂ŌJ��Ԃ�
        while (ca > 0f)
        {
            //�����ɂȂ��Ă������x
            ca -= 0.7f * Time.deltaTime;
            //�F��ς���
            image.color = new Color(c.r, c.g, c.b, ca);
            yield return null;
        }

        //�R���[�`����\��
        StartCoroutine(ChangeScene());
    }

    //�^�C�g���V�[���Ɉړ�
    IEnumerator ChangeScene()
    {
        // �w�肳�ꂽ���Ԃ����ҋ@
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i <= 224; i++)
        {
            hideSquare.transform.position += new Vector3(1, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("title");
    }
}
