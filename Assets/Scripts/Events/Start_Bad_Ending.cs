using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_Bad_Ending : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float duration;
    private bool hasStarted = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (!hasStarted && other.CompareTag("Player"))
            BadEnding();
    }

    void BadEnding()
    {
        hasStarted = true;
        StartCoroutine(FadeRoutine());
    }
    
    IEnumerator FadeRoutine()
    {
        float start = image.color.a;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;  
            float a = Mathf.Lerp(start, 1, t / duration);
            var c = image.color; c.a = a; image.color = c;
            yield return null;
        }
        var final = image.color; final.a = 1; image.color = final;
        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("BadEnding");
    }
    
}
