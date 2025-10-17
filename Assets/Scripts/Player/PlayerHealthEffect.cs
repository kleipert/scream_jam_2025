using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class PlayerHealthEffect : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Volume volume; // Assign your Global Volume in the Inspector

        [Header("Effect")] [SerializeField] private Color hitColor = Color.red;
        [SerializeField, Range(0f, 1f)] private float peakIntensity = 0.3f;
        [SerializeField, Range(0f, 1f)] private float peakIntensityCrit = 0.7f;
        [SerializeField, Range(0f, 1f)] private float smoothness = 0.3f;
        [SerializeField] private float spikeTime = 0.05f;
        [SerializeField] private float fadeTime = 0.35f;

        private Vignette vignette;
        private float defaultIntensity;
        private Color defaultColor;
        private float defaultSmoothness;
        private Coroutine routine;

        void Awake()
        {
            // Make sure we have an instance profile, not the shared asset
            if (volume == null) volume = GetComponent<Volume>();
            if (volume == null)
            {
                Debug.LogError("Volume reference missing.");
                return;
            }

            if (volume.profile == null && volume.sharedProfile != null)
                volume.profile = Instantiate(volume.sharedProfile);

            if (volume.profile == null)
                volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();

            // Ensure a Vignette override exists
            if (!volume.profile.TryGet(out vignette))
                vignette = volume.profile.Add<Vignette>(true);

            // Initialize defaults
            vignette.active = true;
            vignette.intensity.overrideState = true;
            vignette.color.overrideState = true;
            vignette.smoothness.overrideState = true;

            defaultIntensity = vignette.intensity.value;
            defaultColor = vignette.color.value;
            defaultSmoothness = vignette.smoothness.value;
        }

        /// <summary>Call this when the player takes damage.</summary>
        public void OnDamage(float holdTime)
        {
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(DoVignettePulse(holdTime));
        }
        
        public void OnDamageCritical(float holdTime)
        {
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(DoVignettePulseCrit(holdTime));
        }
        
        

        private IEnumerator DoVignettePulse(float holdTime)
        {
            // Spike
            float t = 0f;
            float startIntensity = vignette.intensity.value;
            Color startColor = vignette.color.value;
            float startSmooth = vignette.smoothness.value;

            while (t < spikeTime)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / spikeTime);
                vignette.intensity.value = Mathf.Lerp(startIntensity, peakIntensity, a);
                vignette.color.value = Color.Lerp(startColor, hitColor, a);
                vignette.smoothness.value = Mathf.Lerp(startSmooth, smoothness, a);
                yield return null;
            }
            
            // Hold phase
            vignette.intensity.value = peakIntensity;
            vignette.color.value = hitColor;
            vignette.smoothness.value = smoothness;
            yield return new WaitForSeconds(holdTime);

            // Fade
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / fadeTime);
                vignette.intensity.value = Mathf.Lerp(peakIntensity, defaultIntensity, a);
                vignette.color.value = Color.Lerp(hitColor, defaultColor, a);
                vignette.smoothness.value = Mathf.Lerp(smoothness, defaultSmoothness, a);
                yield return null;
            }

            vignette.intensity.value = defaultIntensity;
            vignette.color.value = defaultColor;
            vignette.smoothness.value = defaultSmoothness;
            routine = null;
        }
        
        private IEnumerator DoVignettePulseCrit(float holdTime)
        {
            // Spike
            float t = 0f;
            float startIntensity = vignette.intensity.value;
            Color startColor = vignette.color.value;
            float startSmooth = vignette.smoothness.value;

            while (t < spikeTime)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / spikeTime);
                vignette.intensity.value = Mathf.Lerp(startIntensity, peakIntensityCrit, a);
                vignette.color.value = Color.Lerp(startColor, hitColor, a);
                vignette.smoothness.value = Mathf.Lerp(startSmooth, smoothness, a);
                yield return null;
            }
            
            // Hold phase
            vignette.intensity.value = peakIntensity;
            vignette.color.value = hitColor;
            vignette.smoothness.value = smoothness;
            yield return new WaitForSeconds(holdTime);

            // Fade
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / fadeTime);
                vignette.intensity.value = Mathf.Lerp(peakIntensityCrit, defaultIntensity, a);
                vignette.color.value = Color.Lerp(hitColor, defaultColor, a);
                vignette.smoothness.value = Mathf.Lerp(smoothness, defaultSmoothness, a);
                yield return null;
            }

            vignette.intensity.value = defaultIntensity;
            vignette.color.value = defaultColor;
            vignette.smoothness.value = defaultSmoothness;
            routine = null;
        }

        public void ShowGreenVignette(bool show)
        {
            vignette.intensity.value = show ? peakIntensityCrit : defaultIntensity;
            vignette.color.value = show ? Color.green : defaultColor;
            vignette.smoothness.value = show ? smoothness : defaultSmoothness;
        }
    }
}
