using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGatcha : MonoBehaviour
{
    static public bool playShake = false;
    static public bool playRoll = false;
    static public bool playOpen = false;

    #region Sound List
    [Header("Shake")]
    [SerializeField] private AudioSource audioSound;

    [Header("Roll Jingle")]
    [SerializeField] private AudioSource audioRoll;

    [Header("Can Open")]
    [SerializeField] private AudioSource audioOpen;
    #endregion

    private void PlaySounds()
    {
        if (audioSound != null)
        {
            if (playShake)
            {
                audioSound.Play();
            }
        }

        if (audioRoll != null)
        {
            if (playRoll)
            {
                audioRoll.Play();
            }
        }

        if (audioOpen != null)
        {
            if (playOpen)
            {
                audioOpen.Play();
            }
        }
    }

    private void ResetBool()
    {
        if (playShake)
            playShake = false;

        if (playRoll)
            playRoll = false;

        if (playOpen)
            playOpen = false;
    }

    private void LateUpdate()
    {
        PlaySounds();

        ResetBool();
    }
}
