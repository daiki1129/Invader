using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
using UnityEngine.SceneManagement;

public class CPU : MonoBehaviour
{
    public bool bCPUflag;//CPUフラグ

    public float fTimer;//間隔
    public float fMoveTimer;//間隔

    [SerializeField]
    Button m_RecordButton;//レコードボタン
    InputEventTrace m_Trace;
    [SerializeField, Tooltip("コインスクリプト")]
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
            fTimer += Time.deltaTime; // 経過時間をカウント

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
            fMoveTimer += Time.deltaTime; // 経過時間をカウント

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

        // 入力記録を再生する
        m_Trace.Replay() // ReplayControllerを取得する（※これだけ呼んでも再生されない）
            .OnFinished(() => m_RecordButton.interactable = true) // 記録の再生が終わった時のコールバック
            .PlayAllEventsAccordingToTimestamps(); // 入力されたタイミングも含めて再現
    }
    public void Loadcoin()
    {
        // ...
        string filePathcoin = GetFilePathcoin();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }

    public void Loadstart()
    {
        // ...
        string filePathcoin = GetFilePathstart();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadleft()
    {
        // ...
        string filePathcoin = GetFilePathleft();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadright()
    {
        // ...
        string filePathcoin = GetFilePathright();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    public void Loadshot()
    {
        // ...
        string filePathcoin = GetFilePathShot();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePathcoin);
        Replay();
    }
    string GetFilePathcoin() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings","coin.txt");
    string GetFilePathstart() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "start.txt");
    string GetFilePathleft() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "left.txt");
    string GetFilePathright() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "right.txt");
    string GetFilePathShot() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", "Shot.txt");

}
