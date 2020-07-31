using UnityEngine;
using UnityEngine.Video;

public class LoadVideo : MonoBehaviour
{
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = Application.streamingAssetsPath + "/test.mp4";
    }

    void Update()
    {

    }
}