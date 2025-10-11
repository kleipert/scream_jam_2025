using StarterAssets;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    
    [SerializeField] private StarterAssetsInputs  starterAssetsInputs;
    

    [Header("Node-Namen")]
    [SerializeField] private string nodeForO = "Start";
    [SerializeField] private string nodeForP = "AlternateStart";

    [SerializeField] private bool canInterrupt = false;

    void Awake() {
        if (dialogueRunner == null) dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner == null) Debug.LogError("Kein DialogueRunner in der Szene gefunden!");
    }

    void Update() {
        // Direkte Tastenabfrage mit neuem Input System
        if (Keyboard.current == null) return; // z.B. auf Mobile/Controller

        if (Keyboard.current.oKey.wasPressedThisFrame) {
            TryStart(nodeForO);
        }
        if (Keyboard.current.pKey.wasPressedThisFrame) {
            TryStart(nodeForP);
        }
    }

    private void TryStart(string nodeName) {
        if (dialogueRunner == null || string.IsNullOrEmpty(nodeName)) return;

        if (dialogueRunner.IsDialogueRunning && !canInterrupt) {
            Debug.Log($"Dialog läuft bereits – '{nodeName}' wird ignoriert.");
            return;
        }
        if (dialogueRunner.IsDialogueRunning && canInterrupt) {
            dialogueRunner.Stop();
        }

        dialogueRunner.StartDialogue(nodeName);
    }
}