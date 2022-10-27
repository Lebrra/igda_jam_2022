using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BeauRoutine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    public AudioSource clipAudioSource;
    public AudioSource musicAudioSource;
    [SerializeField]
    List<SoundClips> BackGroundMusic = new List<SoundClips>();
    [SerializeField]
    List<SoundClips> BattleMusic = new List<SoundClips>();
    public List<SoundClips> audioClips;
    public float transTime = 3f;
    float volume;
    float maxVolume;
    float minVolume;

    [System.Serializable]
    public struct SoundClips
    {
        public string clipName;
        public AudioClip audioClip;
    }

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        volume = musicAudioSource.volume;
        maxVolume = musicAudioSource.volume;
        //Routine.Start(songManager());
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

        clipAudioSource.PlayOneShot(clip, clipVolume);
    }
    public void playBackgroundMusic()
    {
        Routine.Stop("battleSongManager");
        Routine.Start(backgroundSongManager());
    }
    public void playBattleMusic()
    {
        Routine.Stop("backgroundSongManager");
        Routine.Start(battleSongManager());
    }
    IEnumerator backgroundSongManager()
    {
        while (true)
        {

            foreach (var song in BackGroundMusic)
            {
                if (!song.Equals(null))
                {
                    musicAudioSource.PlayOneShot(song.audioClip);
                    //Routine.Start(playWithFade(song));
                    yield return new WaitForSeconds(song.audioClip.length);
                    //Routine.Stop("playWithFade");
                }
            }
        }
        
    }
    IEnumerator battleSongManager()
    {
        while (true)
        {

            foreach (var song in BattleMusic)
            {
                if (!song.Equals(null))
                {
                    musicAudioSource.PlayOneShot(song.audioClip);
                    //Routine.Start(playWithFade(song));
                    yield return new WaitForSeconds(song.audioClip.length);
                    //Routine.Stop("playWithFade");
                }
            }
        }
    }
    IEnumerator playWithFade(SoundClips song)
    {
        float tmax = song.audioClip.length;
        float t = song.audioClip.length;
        musicAudioSource.PlayOneShot(song.audioClip);
        while (t >= 0f)
        {
            
            if (t <= transTime)
            {
                Debug.Log(t);
                musicAudioSource.volume = Mathf.Lerp(maxVolume, 0f, transTime);
            }
            t -= Time.fixedTime;
        }
        yield return null;
    }
}
