using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoContent : MonoBehaviour
{
    public int id;
    public string uTubeURL;


    public void PlayYOUTUBEPLAYERVIDEO()
    {
        API_Manager.INSTANCE.PlayYoutubeLandscapeVideo(uTubeURL);
    }

}
