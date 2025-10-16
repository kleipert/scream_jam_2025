using UnityEngine;
using UnityEngine.UI;

public class E_Button : MonoBehaviour
{
    [SerializeField] private Image image;
    
    public static E_Button Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        image.enabled = false;
    }

    public void ShowButton()
    {
        image.enabled = true;
    }

    public void HideButton()
    {
        image.enabled = false;
    }
    
}
