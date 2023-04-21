using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] backgroundAudioSources;
    public AudioSource[] effectAudioSources;

    public Slider backgroundVolumeSlider;
    public Slider effectVolumeSlider;

   public GameObject audioPanel;
   private GameObject audioManager;
    
    void Start()
    {
        backgroundVolumeSlider.value = GetBackgroundVolume();
        effectVolumeSlider.value = GetEffectVolume();

        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        audioPanel.SetActive(false);

                  
    }

    public void SetBackgroundVolume(float volume)
    {
        foreach (AudioSource audioSource in backgroundAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    public void SetEffectVolume(float volume)
    {
        foreach (AudioSource audioSource in effectAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    public float GetBackgroundVolume()
    {
        float volume = 0;
        foreach (AudioSource audioSource in backgroundAudioSources)
        {
            volume = audioSource.volume;
        }
        return volume;
    }

    public float GetEffectVolume()
    {
        float volume = 0;
        foreach (AudioSource audioSource in effectAudioSources)
        {
            volume = audioSource.volume;
        }
        return volume;
    }

    public void UpdateBackgroundVolume()
    {
        SetBackgroundVolume(backgroundVolumeSlider.value);
    }

    public void UpdateEffectVolume()
    {
        SetEffectVolume(effectVolumeSlider.value);
    }

    public void OpenAudioPanel()
    {
        audioPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseAudioPanel()
    {
        audioPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}


