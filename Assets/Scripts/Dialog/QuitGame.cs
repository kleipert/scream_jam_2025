using UnityEngine;
using Yarn.Unity;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    
    void Awake()
    {
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    
    void OnEnable() 
    {
        if (!dialogueRunner) { Debug.LogError("[FontChanger] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler("Quit", () => Quit());

    }

    void Quit()
    {
        Application.Quit();
    }
}