using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip currentVideoClip; // Store the video clip

    private static VideoManager instance;

    void Awake()
    {
        // Ensure only one instance exists (Singleton pattern)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVideo(VideoClip clip)
    {
        currentVideoClip = clip;
    }

    public VideoClip GetVideo()
    {
        return currentVideoClip;
    }
}