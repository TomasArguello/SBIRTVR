using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Keyboard : MonoBehaviour
{
    [Header("Keyboard Setup")]
    public GameObject KeyboardHolder;
    private enum CaseState
    {
        Lower,
        Shift,
        Caps
    }
    private CaseState Case;
    [SerializeField]
    private ObjectCycler CapsIconCycler;

    private Dictionary<string, string> LowerEquivalent = new Dictionary<string, string> { };
    private Dictionary<string, string> UpperEquivalent = new Dictionary<string, string>
    {
        {"9", "("},
        {"8", "*"},
        {"7", "&"},
        {"6", "^"},
        {"5", "%"},
        {"4", "$"},
        {"3", "#"},
        {"2", "@"},
        {"1", "!"},
        {"0", ")"},
        {"-", "_"},
        {"=", "+"},
        {"`", "~"},
        {"[", "{"},
        {"]", "}"},
        {"\\", "|"},
        {";", ":"},
        {"'", "\""},
        {",", "<"},
        {".", ">"},
        {"/", "?"}
    };

    [Header("Text Field")]
    public Text TextField;
    public Button TextFieldButton;
    public int MaxLength = 15;
    public GameObject DefaultFill;
    
    //[Header("Entry Validation")]
    //private NetworkManager NetworkManager;
    public enum ValidationType
    {
        Username,
        RoomName
    }
    public ValidationType Validation;
    [Tooltip("Whether to leave the last valid entry entered in the text field when returning to this UI panel.")]
    public bool KeepLastValidEntry = true;
    private string LastValidEntry;
    public GameObject ErrorMessage;
    [Tooltip("Buttons to disable when validation fails.")]
    public Button[] DependentButtons = new Button[0];
    

    private void Awake()
    {
        LowerEquivalent = UpperEquivalent.ToDictionary(x => x.Value, x => x.Key);
        //NetworkManager = FindObjectOfType<NetworkManager>();
        LastValidEntry = null;
    }

    private void Start()
    {
        if (TextFieldButton != null)
            TextFieldButton.onClick.AddListener(() => ToggleKeyboard(true));
    }

    private void OnEnable()
    {
        Reset();
    }

    public void EnterKey(string c, bool ExitShift = true)
    {
        if ((MaxLength - TextField.text.Length) >= c.Length)
        {
            TextField.text += c;

            if (Case == CaseState.Shift && ExitShift)
            {
                Case = CaseState.Lower;
                UseLowerCase();
                if (CapsIconCycler != null)
                    CapsIconCycler.Reset();
            }
        }
    }

    public void Backspace()
    {
        if (TextField.text.Length > 0)
        {
            TextField.text = TextField.text.Remove(TextField.text.Length-1, 1);
        }
    }

    public void CycleCase()
    {
        Case = (CaseState)(((int)Case + 1) % System.Enum.GetNames(typeof(CaseState)).Length); // cycle to next enum value

        if (Case == CaseState.Lower) UseLowerCase();
        else UseUpperCase();
    }

    private void UseLowerCase()
    {
        Key[] keys = KeyboardHolder.GetComponentsInChildren<Key>();
        foreach (Key key in keys)
        {
            if (key.Type != Key.KeyType.Null && !key.IgnoreCaps)
            {
                Text t = key.gameObject.GetComponentInChildren<Text>();
                //Debug.Log("The text t is " + t.text);
                if (t != null)
                    t.text = ToLower(t.text);
            }
        }
    }

    private void UseUpperCase()
    {
        Key[] keys = KeyboardHolder.GetComponentsInChildren<Key>();
        foreach (Key key in keys)
        {
            if (key.Type != Key.KeyType.Null && !key.IgnoreCaps)
            {
                Text t = key.gameObject.GetComponentInChildren<Text>();
                if (t != null)
                    t.text = ToUpper(t.text);
            }
        }
    }

    private string ToLower(string c)
    {
        c = c.ToLower();
        //Debug.Log("The lowercase version of this is " + c);
        if (LowerEquivalent.ContainsKey(c))
            c = LowerEquivalent[c];
        return c;
    }

    private string ToUpper(string c)
    {
        c = c.ToUpper();
        if (UpperEquivalent.ContainsKey(c))
            c = UpperEquivalent[c];
        return c;
    }

    public void ResetTextField()
    {
        if (KeepLastValidEntry && LastValidEntry != null)
        {
            TextField.text = LastValidEntry;
            if (DefaultFill != null)
                DefaultFill.SetActive(false);
            /*
            if (Validate())
                ShowValid();
            else
                ShowInvalid();
            */
        }
        else
        {
            if (ErrorMessage != null)
                ErrorMessage.SetActive(false);
            EnableDependentButtons(false);
            TextField.text = "";
            if (DefaultFill != null)
                DefaultFill.SetActive(true);
        }
    }

    public void ClearTextField()
    {
        if (KeyboardHolder.activeSelf)
        {
            TextField.text = "";
        }
    }

    public void Reset()
    {
        TextFieldButton.interactable = true;
        UseLowerCase();
        if (!KeepLastValidEntry)
            LastValidEntry = null;
        ResetTextField();
        KeyboardHolder.SetActive(false);
    }

    private void EnableDependentButtons(bool enable)
    {
        foreach (Button b in DependentButtons)
            b.interactable = enable;
    }

    /*
    public bool Validate()
    {
        bool isValid = true;
        switch (Validation)
        {
            case ValidationType.Username:
            {
                if (NetworkManager != null)
                    isValid = (!NetworkManager.UsernameTaken(TextField.text)) || (NetworkManager.IsConnected() && TextField.text == NetworkManager.GetNickname());
                else
                    isValid = false;
            } break;

            case ValidationType.RoomName:
            {
                if (NetworkManager != null)
                    isValid = !NetworkManager.DoesRoomExist(TextField.text);
                else
                    isValid = false;
            } break;

            default:
            {
                isValid = true;
            } break;
        }
        return isValid;
    }
    */

    public void ToggleKeyboard(bool show)
    {
        KeyboardHolder.SetActive(show);
        if (show)
        {
            TextFieldButton.interactable = false;
            EnableDependentButtons(false);
            Case = CaseState.Lower;
            UseLowerCase();
            if (DefaultFill != null)
                DefaultFill.SetActive(false);
            if (ErrorMessage != null)
                ErrorMessage.SetActive(false);
        }
        else
        {
            TextFieldButton.interactable = true;
            if (TextField.text.Length == 0)
            {
                if (DefaultFill != null)
                    DefaultFill.SetActive(true);
            }
            else
            {
                /*
                if (Validate())
                {
                    ShowValid();
                }
                else
                {
                    ShowInvalid();
                }
                */
            }
        }
    }

    public void ShowValid()
    {
        LastValidEntry = TextField.text;
        EnableDependentButtons(true);
        if (ErrorMessage != null)
            ErrorMessage.SetActive(false);
    }

    public void ShowInvalid()
    {
        EnableDependentButtons(false);
        if (ErrorMessage != null)
            ErrorMessage.SetActive(true);
    }

    public string GetEntry()
    {
        return TextField.text;
    }
}
