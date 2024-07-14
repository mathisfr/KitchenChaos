using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textKeyMoveRight;
    [SerializeField] private TextMeshProUGUI textKeyMoveLeft;
    [SerializeField] private TextMeshProUGUI textKeyMoveDown;
    [SerializeField] private TextMeshProUGUI textKeyMoveUp;
    [SerializeField] private TextMeshProUGUI textKeyInteract;
    [SerializeField] private TextMeshProUGUI textKeyInteractAlternate;
    [SerializeField] private TextMeshProUGUI textKeyPause;
    [SerializeField] private TextMeshProUGUI textKeyGamepadMove;
    [SerializeField] private TextMeshProUGUI textKeyGamepadInteract;
    [SerializeField] private TextMeshProUGUI textKeyGamepadInteractAlternate;
    [SerializeField] private TextMeshProUGUI textKeyGamepadPause;

    private void Start()
    {
        PlayerInput.Instance.OnBindingRebind += PlayerInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void PlayerInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        textKeyMoveRight.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        textKeyMoveLeft.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        textKeyMoveDown.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        textKeyMoveUp.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        textKeyInteract.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Interact);
        textKeyInteractAlternate.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.InteractAlternate);
        textKeyPause.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);

        textKeyGamepadInteract.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Interact);
        textKeyGamepadInteractAlternate.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_InteractAlternate);
        textKeyGamepadPause.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Gamepad_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
