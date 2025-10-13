using GameManager;
using UnityEngine;
using Yarn.Unity;

public class CallEvent : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    
    void Awake()
    {
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    
    void OnEnable() 
    {
        if (!dialogueRunner) { Debug.LogError("[CallEvent] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler<int>("Call", ActivateEvent);
    }

    void OnDisable()
    {
        if (dialogueRunner) dialogueRunner.RemoveCommandHandler("Call");
    }

    void ActivateEvent(int eventNumber)
    {
        Data.Events test = (Data.Events)eventNumber;
        EventController.instance.StartEvent(test);
    }
}
