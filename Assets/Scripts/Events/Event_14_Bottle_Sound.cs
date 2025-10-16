using System;
using UnityEngine;

public class Event_14_Bottle_Sound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    public void PlayDestroySound()
    {
        SoundManager.Instance.PlaySound(audioClip,transform,0.2f);
    }
}
