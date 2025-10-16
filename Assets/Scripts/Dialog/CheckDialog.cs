using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using StarterAssets;
using Cinemachine;

public class CheckDialog : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private StarterAssetsInputs starterInputs;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineVirtualCamera dialogCamera;
    
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
        if (starterInputs != null) 
        {
            starterInputs.cursorInputForLook = !active;
            starterInputs.cursorLocked       = !active;
            starterInputs.canMove = !active;
        }

        if (!active)
        {
            dialogCamera.enabled = false;
            virtualCamera.enabled = true;
        }
        
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible   = active;
    }
}


