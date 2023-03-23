using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour, IPointerExitHandler
{
    public enum KeyType
    {
        Null,
        Character,
        Space,
        Caps,
        Enter,
        Backspace,
        Clear
    }
    [Tooltip("Specifies the function of this key. Null keys will be ignored. Character keys are keys that enter a character into the field when pressed (regular keys). The remaining types are special function keys (like space, enter, etc).")]
    public KeyType Type = KeyType.Character;
    [Tooltip("If true, this key will not be altered when using shift/caps.")]
    public bool IgnoreCaps = false;

    private Keyboard Keyboard;
    [SerializeField]
    private Button Button;
    [SerializeField]
    private Text Label;

    private void Awake()
    {
        Keyboard = gameObject.GetComponentInParent<Keyboard>();
    }

    private void Start()
    {
        if (Button != null && Keyboard != null)
        {
            switch (Type)
            {
                case KeyType.Null:
                    break;
                case KeyType.Character:
                    if (Label != null)
                        Button.onClick.AddListener(() => Keyboard.EnterKey(Label.text, !IgnoreCaps));
                    break;
                case KeyType.Space:
                    Button.onClick.AddListener(() => Keyboard.EnterKey(" ", false));
                    break;
                case KeyType.Caps:
                    Button.onClick.AddListener(() => Keyboard.CycleCase());
                    break;
                case KeyType.Enter:
                    Button.onClick.AddListener(() => Keyboard.ToggleKeyboard(false));
                    break;
                case KeyType.Backspace:
                    Button.onClick.AddListener(() => Keyboard.Backspace());
                    break;
                case KeyType.Clear:
                    Button.onClick.AddListener(() => Keyboard.ClearTextField());
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
