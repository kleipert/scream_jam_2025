using System.Collections;
using Cinemachine;
using GameManager;
using UnityEngine;
using Yarn.Unity;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject target;

    private CinemachineComposer _composer;
    bool _playerInside;

    void Awake()
    {
        if (dialogueRunner == null) dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        _composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        if (!_composer) _composer = virtualCamera.AddCinemachineComponent<CinemachineComposer>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dialog_Start") && !dialogueRunner.IsDialogueRunning &&
            !EventController.instance.isEventActive)
        {
            var nodeName = EventController.instance.lastEvent;
            dialogueRunner.StartDialogue(Data.EventsToDialog[nodeName]);
            
            virtualCamera.PreviousStateIsValid = false;
            virtualCamera.LookAt = target.transform;
        }
    }
}
