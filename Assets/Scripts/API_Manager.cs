using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class API_Manager : MonoBehaviour
{
    public static API_Manager INSTANCE;

    public string API_ALL_LISTS;

   

    public string API_PRODUCT_DETAIL;

    public RootObject_CircleLists _allLists;
    public RootObject_ProductDetail _productDetail;

    public string selectedCategory;
    public string selectedType;

    public GameObject loadingPanel_list;
    public Text _perentText;

    public Panel_ProductDetail _panelProductDetail;
    public Panel_ProductListScroll _panelProductListScroll;

    public YoutubePlayer _youTubePlayerScript;

    public void Awake()
    {
        INSTANCE = this;
        _youTubePlayerScript.gameObject.SetActive(false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    internal void ClickOnSkeletonCircle(int v)
    {
        StartCoroutine("GetProductList_API",v);
    }
    public string s;
    IEnumerator GetProductList_API(int v)
    {
        s = API_ALL_LISTS + "" + v;
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Get(s);
        www.chunkedTransfer = false;

        yield return www.SendWebRequest();

        if (www.isHttpError)
        {
            Show("HTTP Error", www.error, false);
            yield break;
        }
        if (www.isNetworkError)
        {
            Show("Network Error", www.error, false);
            yield break;
        }
        if (string.IsNullOrEmpty(www.downloadHandler.text))
        {
            Show("No Response", www.error, false);
            yield break;
        }


        string jsonString = www.downloadHandler.text;

        Debug.Log("JSON STRING :: " + jsonString);

        _allLists = JsonUtility.FromJson<RootObject_CircleLists>(jsonString);
        _panelProductListScroll.UpdateHeading(_allLists);

    }

    internal void Fetch_API_detail(int id)
    {
        StartCoroutine("GetProductDetail_API", id);
    }

    IEnumerator GetProductDetail_API(int v)
    {
        s = API_PRODUCT_DETAIL + "" + v;
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Get(s);
        www.chunkedTransfer = false;

        yield return www.SendWebRequest();

        if (www.isHttpError)
        {
            Show("HTTP Error", www.error, false);
            yield break;
        }
        if (www.isNetworkError)
        {
            Show("Network Error", www.error, false);
            yield break;
        }
        if (string.IsNullOrEmpty(www.downloadHandler.text))
        {
            Show("No Response", www.error, false);
            yield break;
        }


        string jsonString = www.downloadHandler.text;

        Debug.Log("JSON STRING :: " + jsonString);

        _productDetail = JsonUtility.FromJson<RootObject_ProductDetail>(jsonString);
        _panelProductDetail.SetProductDetail(_productDetail);

    }

    internal void PlayYoutubeLandscapeVideo(string uTubeURL)
    {
        _youTubePlayerScript.gameObject.SetActive(true);
        _youTubePlayerScript.youtubeUrl = uTubeURL;
        _youTubePlayerScript.LoadAndPlay();
        _youTubePlayerScript.gameObject.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.AudioSource;

    }

    public void DisableVideo()
    {
        _youTubePlayerScript.Stop();
        _youTubePlayerScript.gameObject.SetActive(false);
        _youTubePlayerScript.youtubeUrl = null;
    }

    void Show(string header, string info, bool isautohide)
    {
        Debug.Log("HEADER :  " + header + "/n INFO :" + info);
    }

}
