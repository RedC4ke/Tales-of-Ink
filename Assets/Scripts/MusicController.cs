using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class MusicController : MonoBehaviour
{
    [System.Serializable]
    public class Track
    {
        public string tag;
        public AudioClip intro;
        public AudioClip loop;
    }

    public static MusicController Instance;

    AudioSource[] audioSources;
    [SerializeField] List<Track> tracks;
    Dictionary<string, Track> trackDict = new Dictionary<string, Track>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Track track in tracks)
        {
            trackDict[track.tag] = track;
        }

        audioSources = GetComponents<AudioSource>();
    }
    protected void Start()
    {
        PlayTrack("Menu");
    }

    public void PlayTrack(string tag)
    {
        audioSources[1].clip = trackDict[tag].loop;
        audioSources[1].loop = true;

        if (trackDict[tag].intro != null)
        {
            audioSources[0].clip = trackDict[tag].intro;
            audioSources[0].loop = false;

            audioSources[1].PlayDelayed(trackDict[tag].intro.length);
            audioSources[0].Play();
        } else
        {
            audioSources[1].Play();
        }
    }
}
