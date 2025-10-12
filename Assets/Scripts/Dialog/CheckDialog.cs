using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using StarterAssets;

public class CheckDialog : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private StarterAssetsInputs starterInputs;
    [SerializeField] private PlayerInput playerInput;
    
    [SerializeField] private string playerMap = "Player";
    [SerializeField] private string uiMap     = "UI";
    [SerializeField] private bool switchActionMap = true;
    
    void Awake()
    {
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    
    void OnEnable() 
    {
        if (!dialogueRunner) { Debug.LogError("[FontChanger] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler("Lock", () => SetDialogueActive(true));
        dialogueRunner.AddCommandHandler("Unlock",  () => SetDialogueActive(false));
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.RemoveCommandHandler("Lock");
            dialogueRunner.RemoveCommandHandler("Unlock");
        }
    }

    private void SetDialogueActive(bool active)
    {
        if (starterInputs != null) {
            starterInputs.cursorInputForLook = !active;
            starterInputs.cursorLocked       = !active;
        }
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible   = active;

        if (switchActionMap && playerInput != null) {
            var target = active ? uiMap : playerMap;
            if (!string.IsNullOrEmpty(target) && playerInput.currentActionMap?.name != target) {
                playerInput.SwitchCurrentActionMap(target);
            }
        }
    }
}


