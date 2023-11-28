using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
using UnityEngine.SceneManagement;

public class CPU : MonoBehaviour
{
    public bool bCPUflag;//CPU�t���O

    public float fTimer;//�Ԋu
    public float fMoveTimer;//�Ԋu

    [SerializeField]
    Button m_RecordButton;//���R�[�h�{�^��
    InputEventTrace m_Trace;
    [SerializeField, Tooltip("�R�C���X�N���v�g")]
    CoinCount coinscript;

    // Start is called before the first frame update
    void Start()
    {
        m_Trace = new InputEventTrace(Keyboard.current);
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current.pKey.wasPressedThisFrame)
        {
            bCPUflag = !bCPUflag;
        }
        if (bCPUflag && !(SceneManager.GetActiveScene().name == "game"))
        {
            fTimer += Time.deltaTime; // �o�ߎ��Ԃ��J�E���g

            if (fTimer >= 5)
            {
                fTimer = 0;
                int coin = coinscript.coinreturn();
                if (coin == 0)
                {
                    Loadcoin();
                }
                else
                {
                    Loadstart();
                }
            }
        }
        else if (bCPUflag)
        {
            fMoveTimer += Time.deltaTime; // �o�ߎ��Ԃ��J�E���g

            if (fMoveTimer >= 0.5f)
            {
                fMoveTimer = 0;
                float rnd = Random.Range(1, 10);
                if (rnd <= 5)
                {
                    
                }
                else if (rnd <= 7)
                {
                    Loadleft();
                }
                else if (rnd < 10)
                {
                    Loadright();
                }
                if (rnd % 2 == 0)
                {
                    Loadshot();
                }
            }
        }
    }
    public void Replay()
    {
        m_RecordButton.interactable = false;

        // ���͋L�^���Đ�����
        m_Trace.Replay() // ReplayController���擾����i�����ꂾ���Ă�ł��Đ�����Ȃ��j
            .OnFinished(() => m_RecordButton.interactable = true) // �L�^�̍Đ����I��������̃R�[���o�b�N
            .PlayAllEventsAccordingToTimestamps(); // ���͂��ꂽ�^�C�~���O���܂߂čČ�
    }
    public void Loadcoin()
    {
        // ...
        string filePathcoin = GetFilePathcoin();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }

    public void Loadstart()
    {
        // ...
        string filePathcoin = GetFilePathstart();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadleft()
    {
        // ...
        string filePathcoin = GetFilePathleft();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadright()
    {
        // ...
        string filePathcoin = GetFilePathright();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadshot()
    {
        // ...
        string filePathcoin = GetFilePathShot();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    string GetFilePathcoin() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings","coin.txt");
    string GetFilePathstart() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "start.txt");
    string GetFilePathleft() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "left.txt");
    string GetFilePathright() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "right.txt");
    string GetFilePathShot() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "Shot.txt");

}
