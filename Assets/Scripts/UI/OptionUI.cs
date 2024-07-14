using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI ;
using static PlayerInput;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundsEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    public void Awake()
    {
        Instance = this;
        soundsEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();

        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() => 
        {
            RebindBinding(PlayerInput.Binding.Pause);
        });

        gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAltButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Gamepad_InteractAlternate);
        });
        gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Gamepad_Pause);
        });

    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        moveDownText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        moveLeftText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        moveRightText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        interactText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        interactAltText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
        gamepadInteractText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundsEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(PlayerInput.Binding binding)
    {
        ShowPressToRebindKey();
        PlayerInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
