using System.Collections;
using TMPro;
using Yarn.Unity;
using UnityEngine;

public class InputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;       
    [SerializeField] private DialogueRunner dialogueRunner;      

    void Awake()
    {
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        if (input) input.gameObject.SetActive(false);
    }
    
    void OnEnable() 
    {
        if (!dialogueRunner) { Debug.LogError("[FontChanger] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler<string>("Input", Ask);
    }

    private void OnDestroy()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.RemoveCommandHandler("Input");
        }
    }
    
    public IEnumerator Ask(string yarnVarName)
    {
        bool done = false;
        void Submit(string _) => done = true;
        
        if (input) input.gameObject.SetActive(true);

        input.onSubmit.AddListener(Submit);     
        input.onEndEdit.AddListener(Submit);   

        input.text = "";
        input.interactable = true;
        input.ActivateInputField();
        input.Select();

        yield return new WaitUntil(() => done);

        input.onSubmit.RemoveListener(Submit);
        input.onEndEdit.RemoveListener(Submit);
        input.interactable = false;
        
        input.gameObject.SetActive(false);

        dialogueRunner.VariableStorage.SetValue(yarnVarName, input.text);
    }
}
