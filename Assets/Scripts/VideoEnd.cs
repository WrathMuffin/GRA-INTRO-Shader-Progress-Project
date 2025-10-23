using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    private VideoPlayer video_player;

    void Start()
    {
        video_player = GetComponent<VideoPlayer>();
    
        video_player.loopPointReached += VideoEnded;
    }

    void VideoEnded(VideoPlayer player)
    {
       SwitchScene.SwapScene("BranchScene");
    }

}
