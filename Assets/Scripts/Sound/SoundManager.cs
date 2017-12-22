using UnityEngine;

// ALLE GELUID SCRIPTS DOOR ANTONIO

public class SoundManager : MonoBehaviour
{
	private static SoundManager _instance;
	public static SoundManager _INSTANCE
	{
		get
		{
			if (_instance == null)
				_instance = 
					((GameObject)GameObject.Instantiate(Resources.Load("SoundManager")))
					.GetComponent<SoundManager>();
			return _instance;
		}
	}

	private AudioSource _audioSource;
	
	private void Start()
	{
		
	}

	public static void PlaySoundAt(Vector3 position, AudioClip clip, float volume = 1.0f)
	{
		AudioSource.PlayClipAtPoint(clip, position, volume);
	}
	
}
