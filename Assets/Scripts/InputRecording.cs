using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.IO;
public class InputRecording : MonoBehaviour
{
    [SerializeField, Tooltip("���[�ꏊ")]
    public static InputRecording instance;


    [SerializeField]
    Button m_RecordButton;//���R�[�h�{�^��
    [SerializeField]
    Button m_ReplayButton;//���v���C�{�^��

    [SerializeField]
    Button m_SaveButton;//�Z�[�u�{�^��

    [SerializeField]
    Button m_LoadButton;//���[�h�{�^��

    [SerializeField]
    string m_FileName = "recording";

    InputEventTrace m_Trace;
    // Start is called before the first frame update
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
            Debug.Log("A�L�[�������ꂽ�I");
        }
        if (current.dKey.wasPressedThisFrame)
        {
            Debug.Log("d�L�[�������ꂽ�I");
        }
        if (current.sKey.wasPressedThisFrame)
        {
            Debug.Log("s�L�[�������ꂽ�I");
        }
        if (current.wKey.wasPressedThisFrame)
        {
            Debug.Log("w�L�[�������ꂽ�I");
        }
        if (current.mKey.wasPressedThisFrame)
        {
            Debug.Log("m�L�[�������ꂽ�I");
        }
        if (current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("�X�y�[�X�L�[�������ꂽ�I");
        }
        if (current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("1�L�[�������ꂽ�I");
        }
        if (current.digit5Key.wasPressedThisFrame)
        {
            Debug.Log("5�L�[�������ꂽ�I");
        }
    }

    public void ToggleRecording()
    {
        if (m_Trace.enabled)
        {
            // ���͂̋L�^���~
            m_Trace.Disable();
        }
        else
        {
            // �����̓��͋L�^���폜���Ă���A���͂̋L�^���J�n
            m_Trace.Clear();
            m_Trace.Enable();
        }
        Debug.Log("Record");
    }


    public void Replay()
    {
        m_RecordButton.interactable = false;

        // ���͋L�^���Đ�����
        m_Trace.Replay() // ReplayController���擾����i�����ꂾ���Ă�ł��Đ�����Ȃ��j
            .OnFinished(() => m_RecordButton.interactable = true) // �L�^�̍Đ����I��������̃R�[���o�b�N
            .PlayAllEventsAccordingToTimestamps(); // ���͂��ꂽ�^�C�~���O���܂߂čČ�

        Debug.Log("Replay");
    }


    public void Save()
    {
        // ...
        string filePath = GetFilePath();

        // ���͋L�^���w�肵���t�@�C���ɃZ�[�u����
        m_Trace.WriteTo(filePath);
        Debug.Log("Save to " + filePath);
    }

    public void Load()
    {
        // ...
        string filePath = GetFilePath();
        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
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
        // InputAction�̏������iPlayerInputActions�͕ʂ̃t�@�C���Œ�`����Ă��܂��j
        m_PlayerInputActions = new PlayerInputActions();
        m_PlayerInputActions.Enable();
        m_MoveInput = m_PlayerInputActions.Player.Move;

        // InputEventTrace�̏�����
        m_Trace = new InputEventTrace(Keyboard.current);
        m_Trace.onEvent += ev => Debug.Log(ev.ToString());

        // UI�ւ̃C�x���g�o�^
        m_RecordButton.onClick.AddListener(ToggleRecording);
        m_ReplayButton.onClick.AddListener(Replay);
        m_SaveButton.onClick.AddListener(Save);
        m_LoadButton.onClick.AddListener(Load);
    }

    void OnDestroy()
    {
        m_PlayerInputActions.Dispose();

        // InputEventTrace���g������͕K���������K�v������B
        m_Trace.Dispose();
    }

    void Update()
    {
        // ���͂ɏ]���āA�v���C���[���ړ�������
        var input = m_MoveInput.ReadValue<Vector2>();
        var movement = new Vector3(input.x, 0f, input.y) * 4f * Time.deltaTime;
        m_Player.Translate(movement, Space.Self);
    }

    void ToggleRecording()
    {
        if (m_Trace.enabled)
        {
            // ���͂̋L�^���~
            m_Trace.Disable();
        }
        else
        {
            // �����̓��͋L�^���폜���Ă���A���͂̋L�^���J�n
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

        // ���͋L�^���Đ�����
        m_Trace.Replay() // ReplayController���擾����i�����ꂾ���Ă�ł��Đ�����Ȃ��j
            .OnFinished(() => m_RecordButton.interactable = true) // �L�^�̍Đ����I��������̃R�[���o�b�N
            .PlayAllEventsAccordingToTimestamps(); // ���͂��ꂽ�^�C�~���O���܂߂čČ�

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

        // ���͋L�^���w�肵���t�@�C���ɃZ�[�u����
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

        // ���͋L�^���w�肵���t�@�C�����烍�[�h����
        m_Trace.ReadFrom(filePath);
        Debug.Log("Load from " + filePath);
    }

    string GetFilePath() => Path.Combine(Path.GetDirectoryName(Application.dataPath), "InputRecordings", m_FileName + ".txt");

}*/
