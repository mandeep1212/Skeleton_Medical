using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ProductDetail : MonoBehaviour
{
    public Texture noImgAvailable;

    public Text productCategoryText;
    public Text typeText;
    public Text nameText;
    public Text descriptionText;

    public GameObject videoContainer;
    public GameObject attachmentContainer;

    public GameObject attachmentPrefab;
    public GameObject videoPrefab;
    public Scrollbar verticalScroll;
    
    Texture Decode(string base64Img)
    {
        byte[] Bytes = System.Convert.FromBase64String(base64Img);
        Texture2D tex = new Texture2D(1207, 605);
        tex.LoadImage(Bytes);
        //Rect rect = new Rect(0, 0, tex.width, tex.height);
        //sprite = Sprite.Create(www.texture, rect, new Vector2(), 100f);
        return tex;

    }

    internal void SetProductDetail(RootObject_ProductDetail productDetail)
    {
        productCategoryText.text = API_Manager.INSTANCE.selectedCategory;
        typeText.text = API_Manager.INSTANCE.selectedType;

        nameText.text = productDetail.englishProductName;
        descriptionText.text = productDetail.englishProductDescription;


        // Delete child of attachments
        foreach(Transform t in attachmentContainer.transform)
        {
            Destroy(t.gameObject);
        }


        if (productDetail.attachments.Count > 0)
        {
            foreach (Attachment a in productDetail.attachments)
            {
                GameObject attach = Instantiate(attachmentPrefab, attachmentContainer.transform);
                attach.GetComponent<RawImage>().texture = Decode(a.pic);
            }
        }
        else
        {
            // Show No Image Found
            GameObject attach = Instantiate(attachmentPrefab, attachmentContainer.transform);
        }

        foreach (Transform t in videoContainer.transform)
        {
            Destroy(t.gameObject);
        }

        if (productDetail.productVideos.Count > 0)
        {
            foreach (ProductVideo a in productDetail.productVideos)
            {
                GameObject video = Instantiate(videoPrefab, videoContainer.transform);
                video.GetComponent<VideoContent>().uTubeURL = a.videoUrl;
            }
        }
        else
        {

        }

        verticalScroll.value = 1;
        OrthographicZoom.instance.bgLines.SetActive(true);
        API_Manager.INSTANCE._panelProductDetail.GetComponent<UIPanelMover>().MoveIn();
    }


    public void Back()
    {
        OrthographicZoom.instance.bgLines.SetActive(false);
        API_Manager.INSTANCE._panelProductDetail.GetComponent<UIPanelMover>().Moveout();
    }

    public void CreateQuote()
    {

    }
}
