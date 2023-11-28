using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField, Tooltip("���[�ꏊ")]
    public static UI instance;

    void Awake()
    {
        // �C���X�^���X�����݂��Ȃ��ꍇ
        if (instance == null)
        {
            // ���̃C���X�^���X��Singleton�C���X�^���X�ɂ���
            instance = this;
            // �V�[����ύX���Ă�����GameObject���ێ�����
            DontDestroyOnLoad(gameObject);
        }
        // �C���X�^���X�����ɑ��݂���ꍇ
        else
        {
            // ����GameObject��j������
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
