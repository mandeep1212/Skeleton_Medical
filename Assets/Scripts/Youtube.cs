using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Youtube : MonoBehaviour
{
    public VideoPlayer vid;
    public Image _playimg;
    public Image _transImg;
    public bool isplaying = false;

    public Color pauseColor;
    public Color playColor;

    public void Play_pause()
    {
        if (isplaying == false)
        {
            _playimg.enabled = false;
            vid.Play();
            isplaying = true;
            _transImg.color = playColor;
        }
        else
        {
            _playimg.enabled = true;
            vid.Stop();
            isplaying = false;
            _transImg.color = pauseColor;
        }
    }

    
}
