using System;
using UnityEngine;
using UnityEngine.InputSystem;   
using Yarn.Unity;
using StarterAssets;

public class DialogueGameplayGate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private StarterAssetsInputs starterInputs;  
    [SerializeField] private PlayerInput playerInput;            

    [Header("Action Map Names")]
    [SerializeField] private string playerMap = "Player";
    [SerializeField] private string uiMap     = "UI";

    [Header("Options")]
    [Tooltip("Bei aktivem Dialog zur UI-Map wechseln und Movement/Look damit automatisch sperren.")]
    [SerializeField] private bool switchActionMap = true;

    /*[Obsolete("Obsolete")]
    void Awake() {
        if (dialogueRunner == null) dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (starterInputs  == null) starterInputs  = FindObjectOfType<StarterAssetsInputs>();
        if (playerInput    == null) playerInput    = FindObjectOfType<PlayerInput>();

        if (dialogueRunner == null) {
            Debug.LogError("DialogueRunner nicht gefunden.");
            enabled = false;
            return;
        }
    }*/
    void OnEnable()
    {
        if (dialogueRunner.onDialogueStart != null) dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
        if (dialogueRunner.onDialogueComplete != null) dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
    }

    void OnDisable() {
        if (dialogueRunner != null)
        {
            if (dialogueRunner.onDialogueStart != null) dialogueRunner.onDialogueStart.RemoveListener(OnDialogueStart);
            if (dialogueRunner.onDialogueComplete != null)
                dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueEnd);
        }
    }

    private void OnDialogueStart()  => SetDialogueActive(true);
    private void OnDialogueEnd()    => SetDialogueActive(false);

    private void SetDialogueActive(bool active)
    {
        // 1) Cursor & Look
        if (starterInputs != null) {
            starterInputs.cursorInputForLook = !active;  
            starterInputs.cursorLocked      = !active;    
        }

        // Sicherheitshalber direkt das OS-Cursorverhalten setzen:
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible   = active;

        // 2) Action Map umschalten (empfohlen)
        if (switchActionMap && playerInput != null) {
            var target = active ? uiMap : playerMap;
            if (!string.IsNullOrEmpty(target) && playerInput.currentActionMap?.name != target) {
                playerInput.SwitchCurrentActionMap(target);
            }
        }
    }
}
