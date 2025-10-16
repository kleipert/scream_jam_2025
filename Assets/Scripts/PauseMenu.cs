using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using StarterAssets;
using TMPro;
using Yarn.Unity;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private TMP_Text fovText;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private AudioMixer audioMixer;

    private bool _settingsOpen;
    private bool _isPaused;

    void Start()
    {
        SafeSetActive(pauseMenu, false);
        SafeSetActive(settingsMenu, false);
        ApplyCursor(false);
    }

    void OnDisable()
    {
        if (_isPaused)
        {
            Time.timeScale = 1f;
            ApplyCursor(false);
            _isPaused = false;
        }
    }

    void Update()
    {
        if (input.escape)
        {
            HandleEscapePress();
            input.escape = false; 
        }
        
        if (fovText && virtualCamera != null)
        {
            float fov = virtualCamera.m_Lens.FieldOfView;
            fovText.text = $"FoV: {Mathf.RoundToInt(fov)}";
        }
    }

    private void HandleEscapePress()
    {
        if (dialogueRunner && dialogueRunner.IsDialogueRunning && !_isPaused)
            return;

        if (_settingsOpen)
        {
            Back(); 
        }
        else
        {
            if (_isPaused) Resume();
            else Pause();
        }
    }

    private void Pause()
    {
        SafeSetActive(pauseMenu, true);
        SafeSetActive(settingsMenu, false);
        _settingsOpen = false;
        _isPaused = true;

        Time.timeScale = 0f;
        StartCoroutine(UnlockCursorRealtime());
    }

    public void SettingsMenu()
    {
        if (!_isPaused) Pause();

        SafeSetActive(pauseMenu, false);
        SafeSetActive(settingsMenu, true);
        _settingsOpen = true;
        
        StartCoroutine(UnlockCursorRealtime());
    }

    public void Resume()
    {
        SafeSetActive(pauseMenu, false);
        SafeSetActive(settingsMenu, false);
        _settingsOpen = false;
        _isPaused = false;

        Time.timeScale = 1f;
        ApplyCursor(false);
    }

    public void Back()
    {
        SafeSetActive(settingsMenu, false);
        SafeSetActive(pauseMenu, true);
        _settingsOpen = false;
        StartCoroutine(UnlockCursorRealtime());
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void SetVolume(float linearVolume)
    {
        audioMixer.SetFloat("volume", linearVolume);
    }
    
    public void SetFoV(float fov)
    {
        if (virtualCamera != null)
        {
            var lens = virtualCamera.m_Lens;
            lens.FieldOfView = fov;
            virtualCamera.m_Lens = lens; 
        }
    }

    private IEnumerator UnlockCursorRealtime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        ApplyCursor(true);
    }

    private void ApplyCursor(bool unlockedVisible)
    {
        if (unlockedVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (input)
            {
                input.cursorLocked = false;
                input.cursorInputForLook = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (input)
            {
                input.cursorLocked = true;
                input.cursorInputForLook = true;
            }
        }
    }

    private static void SafeSetActive(GameObject go, bool active)
    {
        if (go && go.activeSelf != active) go.SetActive(active);
    }
}