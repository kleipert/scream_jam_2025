using System;
using UnityEngine;

public class Event_14_Bottle_Sound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    void OnDestroy()
    {
        SoundManager.Instance.PlaySound(audioClip,transform,0.2f);
    }
}
