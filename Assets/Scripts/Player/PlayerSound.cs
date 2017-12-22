using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public AudioClip[] attackCollisionSounds;
    public AudioClip[] chargeSounds;

    public void PlayRandomFootstepSound()
    {
        SoundManager.PlaySoundAt(transform.position, footstepSounds[Random.Range(0,footstepSounds.Length)]);
    }

    public void PlayRandomCollisionSound()
    {
        SoundManager.PlaySoundAt(transform.position, attackCollisionSounds[Random.Range(0,attackCollisionSounds.Length)]);
    }

    public void PlayRandomAttackSound()
    {
        SoundManager.PlaySoundAt(transform.position, chargeSounds[Random.Range(0,chargeSounds.Length)], 3);

    }
}
