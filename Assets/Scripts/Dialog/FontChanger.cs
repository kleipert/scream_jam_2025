using TMPro;
using UnityEngine;
using Yarn.Unity;

public class FontChanger : MonoBehaviour {
    public TextMeshProUGUI target;
    public TMP_FontAsset normalFont;
    public TMP_FontAsset demonFont;
    public DialogueRunner dialogueRunner;

    void Awake() {
        if (target) target.richText = true;
        if (!dialogueRunner) dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }
    void OnEnable() {
        if (!dialogueRunner) { Debug.LogError("[FontChanger] Kein DialogueRunner."); return; }
        dialogueRunner.AddCommandHandler("Normal", () => Apply(normalFont));
        dialogueRunner.AddCommandHandler("Demon",  () => Apply(demonFont));
    }
    void OnDisable() {
        if (!dialogueRunner) return;
        dialogueRunner.RemoveCommandHandler("Normal");
        dialogueRunner.RemoveCommandHandler("Demon");
    }
    private void Apply(TMP_FontAsset f) {
        if (target && f) target.font = f;
    }
}
