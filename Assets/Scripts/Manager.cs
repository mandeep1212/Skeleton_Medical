using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class Manager : MonoBehaviour
{
    public static Manager _managerInstance;
   // public Youtube _uTube;
    public BitBenderGames.TouchInputController _tt;
    public GameObject _skeleton3d_Model;

   // [SerializeField]
    public bool isCameraZoomed;

    public GameObject cube_Rotation;
    public Animator skeletonInfoPanel;
    public GameObject detailInfo;

    public OrthographicZoom zoomScript;

    private void Awake()
    {
        _managerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _skeleton3d_Model.SetActive(false);
        Invoke("EnableSkeleton", 1.0f);
    }

    void EnableSkeleton()
    {
        _skeleton3d_Model.SetActive(true);
    }

    private void Update()
    {
        if (isCameraZoomed && cube_Rotation.GetComponent<BoxCollider>().enabled == true)
        {
             cube_Rotation.GetComponent<BoxCollider>().enabled = false;
        }

        if (!isCameraZoomed && cube_Rotation.GetComponent<BoxCollider>().enabled == false)
        {
            cube_Rotation.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void OnPickItem2D(GameObject g)
    {
        Debug.Log("I AM CLICKING "+g.name);
        AnimateTransition_SkeletonInfo();
        isCameraZoomed = true;
        API_Manager.INSTANCE.ClickOnSkeletonCircle(int.Parse(g.name.Trim()));
    }

    void AnimateTransition_SkeletonInfo()
    {
        if(skeletonInfoPanel.GetBool("toup") == false)
        {
            skeletonInfoPanel.SetBool("toup", true);
        }
        else
        {
            skeletonInfoPanel.SetBool("toup", false);
            Invoke("AgainUp_NewData", 0.25f);
        }
    }

    void AgainUp_NewData()
    {
        skeletonInfoPanel.SetBool("toup", true);
    }

   public void Close_skeletoninfo()
    {
        skeletonInfoPanel.SetBool("toup", false);
    }


    public void ShowDetailInfo()
    {
        detailInfo.GetComponent<UIPanelMover>().MoveIn();
        Manager._managerInstance.Close_skeletoninfo();
        zoomScript.ZoomOutCamera();

    }

    public void CloseDetailInfo()
    {
        zoomScript.ZoomOutCamera();
        detailInfo.GetComponent<UIPanelMover>().Moveout();
    }
}
