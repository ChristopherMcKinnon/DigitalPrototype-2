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

    public void SFX(AudioClip audioClip, float volume, float length, float time = 0f, bool loop = false)
    {
        // spawn audio player
        audioSource = Instantiate(SFXObject);

        // set the settings to specified amounts
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.time = time;
        audioSource.PlayOneShot(audioClip); // oneShot lets the clips overlap with others

        // end the clip after the set length
        if (!loop) Destroy(audioSource.gameObject, length);
    }
}
