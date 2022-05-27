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

    AudioSource audioSource;
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

        audioSource = GetComponent<AudioSource>();
    }
    protected void Start()
    {
        Timing.RunCoroutine(_PlayTrack("Ice"), Segment.LateUpdate);
    }

    public IEnumerator<float> _PlayTrack(string tag)
    {
        if (trackDict[tag].intro != null)
        {
            audioSource.clip = trackDict[tag].intro;
            audioSource.Play();
            yield return Timing.WaitForSeconds(trackDict[tag].intro.length);
        }

        audioSource.clip = trackDict[tag].loop;
        audioSource.Play();
    }
}
