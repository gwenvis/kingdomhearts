using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
	public AudioClip[] hurtSounds;

	public void PlayRandomHurtSound()
	{
		SoundManager.PlaySoundAt(transform.position,
			hurtSounds[Random.Range(0, hurtSounds.Length)]);
	}
}
