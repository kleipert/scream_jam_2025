using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using StarterAssets;
using TMPro;
using UnityEngine.Serialization;
using Yarn.Unity;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] StarterAssetsInputs input;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] private TMP_Text fovText;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private DialogueRunner dialogueRunner;
    
    private bool _settingsOpen;
    
    public AudioMixer audioMixer;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (input.escape && !_settingsOpen && !dialogueRunner.IsDialogueRunning)
        {
            pauseMenu.SetActive(true);
            input.cursorLocked = false;
            input.cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        
        fovText.text = "FoV: " + mainCamera.fieldOfView;
    }

    public void SettingsMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        _settingsOpen = true;
        StartCoroutine(UnlockCursor());
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        input.cursorLocked = true;
        input.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        _settingsOpen = false;
        StartCoroutine(UnlockCursor());
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    public void SetFoV(float fov)
    {
        virtualCamera.m_Lens.FieldOfView = fov;
    }

    private IEnumerator UnlockCursor()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.lockState = CursorLockMode.None;
        input.cursorLocked = false;
        input.cursorInputForLook = false;
    }
}
