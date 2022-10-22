using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public List<SoundClips> audioClips;

    [System.Serializable]
    public struct SoundClips
    {
        public string clipName;
        public AudioClip audioClip;
    }

    public AudioClip StringToClip(string s)
    {
        var test = audioClips.Where(x => x.clipName == s).ToList();

        if (test.Count >= 1)
            return test[0].audioClip;
        else
            return null;
    }

    public void playSoundClip(string clipName, float clipVolume)
    {
        var clip = StringToClip(clipName);

        audioSource.PlayOneShot(clip, clipVolume);
    }
}
