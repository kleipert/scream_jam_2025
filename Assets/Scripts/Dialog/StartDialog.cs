using GameManager;
using UnityEngine;
using Yarn.Unity;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    
    bool playerInside;

    void Awake()
    {
        if (dialogueRunner == null) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dialog_Start" && !dialogueRunner.IsDialogueRunning && !EventController.instance.isEventActive)
        {
            var nodeName = EventController.instance.lastEvent; 
            dialogueRunner.StartDialogue(Data.EventsToDialog[nodeName]);
        }
    }
}
