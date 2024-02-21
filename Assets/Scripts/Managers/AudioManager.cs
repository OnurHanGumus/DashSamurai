using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private AudioSignals AudioSignals { get; set; }
    #endregion

    #region Serialized Variables
    [SerializeField] private List<AudioSource> sources;
    [SerializeField] private List<AudioClip> sounds;

    #endregion

    #endregion

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        AudioSignals.onPlaySound += OnPlaySound;
    }

    private void UnsubscribeEvents()
    {
        AudioSignals.onPlaySound -= OnPlaySound;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void OnPlaySound(AudioSoundEnums id)
    {
        foreach (var i in sources)
        {
            if (i.isPlaying)
            {
                continue;
            }
            else
            {
                i.PlayOneShot(sounds[(int)id]);
                break;
            }
        }
    }
    public void ButtonClickSound()
    {
        OnPlaySound(AudioSoundEnums.Click);
    }
}