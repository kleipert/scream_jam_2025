using StarterAssets;
using UnityEngine;
using Yarn.Unity;

public class CloseDialogue : MonoBehaviour
{
    [SerializeField] LinePresenter linePresenter;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private GameObject button;
    
    void Awake()
    {
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    
    void OnEnable() 
    {
        if (!dialogueRunner) { Debug.LogError("[FontChanger] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler("CloseEnable", () => CloseEnable());
        dialogueRunner.AddCommandHandler("CloseDisable", () => CloseDisable());
    }

    void CloseEnable()
    {
        linePresenter.autoAdvance = true;
        button.SetActive(false);
    }

    void CloseDisable()
    {
        linePresenter.autoAdvance = false;
        button.SetActive(true);
    }
}
