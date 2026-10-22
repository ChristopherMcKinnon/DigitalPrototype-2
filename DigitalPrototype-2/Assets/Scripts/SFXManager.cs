using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField] private AudioSource SFXObject;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SFX(AudioClip audioClip, float volume, float length, bool loop = false)
    {
        if (audioSource == null)
        {
            // spawn audio player
            audioSource = Instantiate(SFXObject);

            // set the settings to specified amounts
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();

            // end the clip after the set length
            if (!loop) Destroy(audioSource.gameObject, length);
        }
    }
}
