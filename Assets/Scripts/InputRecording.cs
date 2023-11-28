using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class InputRecording : MonoBehaviour
{
    [SerializeField, Tooltip("収納場所")]
    public static InputRecording instance;


    [SerializeField]
    Button m_RecordButton;//レコードボタン
    [SerializeField]
    Button m_ReplayButton;//リプレイボタン

    [SerializeField]
    Button m_SaveButton;//セーブボタン

    [SerializeField]
    Button m_LoadButton;//ロードボタン

    [SerializeField]
    string m_FileName = "recording";

    InputEventTrace m_Trace;
    // Start is called before the first frame update
    void Awake()
    {
        // インスタンスが存在しない場合
        if (instance == null)
        {
            // このインスタンスをSingletonインスタンスにする
            instance = this;
            // シーンを変更してもこのGameObjectを維持する
            DontDestroyOnLoad(gameObject);
        }
        // インスタンスが既に存在する場合
        else
        {
            // このGameObjectを破棄する
            Destroy(gameObject);
        }
        m_Trace = new InputEventTrace(Keyboard.current);

        //m_RecordButton.onClick.AddListener(ToggleRecording);
        //m_ReplayButton.onClick.AddListener(Replay);
        //m_SaveButton.onClick.AddListener(Save);
        //m_LoadButton.onClick.AddListener(Load);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
        if (current.aKey.wasPressedThisFrame)
        {
            Debug.Log("Aキーが押された！");
        }
        if (current.dKey.wasPressedThisFrame)
        {
            Debug.Log("dキーが押された！");
        }
        if (current.sKey.wasPressedThisFrame)
        {
            Debug.Log("sキーが押された！");
        }
        if (current.wKey.wasPressedThisFrame)
        {
            Debug.Log("wキーが押された！");
        }
        if (current.mKey.wasPressedThisFrame)
        {
            Debug.Log("mキーが押された！");
        }
        if (current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("スペースキーが押された！");
        }
        if (current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("1キーが押された！");
        }
        if (current.digit5Key.wasPressedThisFrame)
        {
            Debug.Log("5キーが押された！");
        }
    }

    public void ToggleRecording()
    {
        if (m_Trace.enabled)
        {
            // 入力の記録を停止
            m_Trace.Disable();
        }
        else
        {
            // 既存の入力記録を削除してから、入力の記録を開始
            m_Trace.Clear();
            m_Trace.Enable();
        }
        Debug.Log("Record");
    }


    public void Replay()
    {
        m_RecordButton.interactable = false;

        // 入力記録を再生する
        m_Trace.Replay() // ReplayControllerを取得する（※これだけ呼んでも再生されない）
            .OnFinished(() => m_RecordButton.interactable = true) // 記録の再生が終わった時のコールバック
            .PlayAllEventsAccordingToTimestamps(); // 入力されたタイミングも含めて再現

        Debug.Log("Replay");
    }


    public void Save()
    {
        // ...
        string filePath = GetFilePath();

        // 入力記録を指定したファイルにセーブする
        m_Trace.WriteTo(filePath);
        Debug.Log("Save to " + filePath);
    }

    public void Load()
    {
        // ...
        string filePath = GetFilePath();
        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePath);
        Debug.Log("Load from " + filePath);
    }
    string GetFilePath() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", m_FileName + ".txt");
}

/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;

public class InputRecording : MonoBehaviour
{

    [SerializeField]
    Transform m_Player;

    [SerializeField]
    Button m_RecordButton;

    [SerializeField]
    Button m_ReplayButton;

    [SerializeField]
    Button m_SaveButton;

    [SerializeField]
    Button m_LoadButton;

    [SerializeField]
    string m_FileName = "recording";

    InputEventTrace m_Trace;
    PlayerInputActions m_PlayerInputActions;
    InputAction m_MoveInput;

    void Awake()
    {
        // InputActionの初期化（PlayerInputActionsは別のファイルで定義されています）
        m_PlayerInputActions = new PlayerInputActions();
        m_PlayerInputActions.Enable();
        m_MoveInput = m_PlayerInputActions.Player.Move;

        // InputEventTraceの初期化
        m_Trace = new InputEventTrace(Keyboard.current);
        m_Trace.onEvent += ev => Debug.Log(ev.ToString());

        // UIへのイベント登録
        m_RecordButton.onClick.AddListener(ToggleRecording);
        m_ReplayButton.onClick.AddListener(Replay);
        m_SaveButton.onClick.AddListener(Save);
        m_LoadButton.onClick.AddListener(Load);
    }

    void OnDestroy()
    {
        m_PlayerInputActions.Dispose();

        // InputEventTraceを使った後は必ず解放する必要がある。
        m_Trace.Dispose();
    }

    void Update()
    {
        // 入力に従って、プレイヤーを移動させる
        var input = m_MoveInput.ReadValue<Vector2>();
        var movement = new Vector3(input.x, 0f, input.y) * 4f * Time.deltaTime;
        m_Player.Translate(movement, Space.Self);
    }

    void ToggleRecording()
    {
        if (m_Trace.enabled)
        {
            // 入力の記録を停止
            m_Trace.Disable();
        }
        else
        {
            // 既存の入力記録を削除してから、入力の記録を開始
            m_Trace.Clear();
            m_Trace.Enable();
        }

        m_Player.position = Vector3.zero;
        m_ReplayButton.interactable = !m_Trace.enabled;

        Debug.Log(m_Trace.enabled ? "Start Recording" : "Stop Recording");
    }

    void Replay()
    {
        m_Player.position = Vector3.zero;
        m_RecordButton.interactable = false;

        // 入力記録を再生する
        m_Trace.Replay() // ReplayControllerを取得する（※これだけ呼んでも再生されない）
            .OnFinished(() => m_RecordButton.interactable = true) // 記録の再生が終わった時のコールバック
            .PlayAllEventsAccordingToTimestamps(); // 入力されたタイミングも含めて再現

        Debug.Log("Replay");
    }

    void Save()
    {
        string filePath = GetFilePath();
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // 入力記録を指定したファイルにセーブする
        m_Trace.WriteTo(filePath);
        Debug.Log("Save to " + filePath);
    }

    void Load()
    {
        string filePath = GetFilePath();
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        // 入力記録を指定したファイルからロードする
        m_Trace.ReadFrom(filePath);
        Debug.Log("Load from " + filePath);
    }

    string GetFilePath() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", m_FileName + ".txt");

}*/
