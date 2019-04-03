using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DentedPixel;
public class OrthographicZoom : MonoBehaviour
{
    public static OrthographicZoom instance;

    private Vector3 defaultCenter;
    private float defaultHeight; // height of orthographic viewport in world units
    public GameObject objectView;
    private Vector3 defaultPos;
    private float zoomValue = -21;
    public GameObject bgLines;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        defaultCenter = GetComponent<Camera>().transform.position;
        defaultHeight = 2f * GetComponent<Camera>().orthographicSize;
        defaultPos = this.transform.position;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicking on a UI ELEMENT");
                return;
            }

            Collider target = GetTarget();

            if (target != null)
            {

                
                if (target.CompareTag("points"))
                {
                    if (Manager._managerInstance.isCameraZoomed)
                    {
                        ZoomOutCamera();
                    }
                    else
                    {
                        //Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        //OrthoZoom(new Vector2(p.x, p.y-0.6f), 2);
                        ZoomInCamera(target.gameObject.transform.position);
                        Manager._managerInstance.OnPickItem2D(target.gameObject);
                    }



                }
                else
                {


                   // Debug.Log("Clicking on back Plane");
                    if (EventSystem.current.IsPointerOverGameObject() == false)
                    {
                        // if(Camera.main.orthographicSize != 7.5f)
                        // {
                        // OrthoZoom(defaultCenter, 7.5f);
                        ZoomOutCamera();
                        Manager._managerInstance.Close_skeletoninfo();
                        Manager._managerInstance.isCameraZoomed = false;
                        Debug.Log("Resetting Zoom target found");
                    }

                }





            }
            else
            {

                Debug.Log("No Collider");

                if (IsPointerOverUIObject() == false)
                {
                    //OrthoZoom(defaultCenter, 7.5f);
                    ZoomOutCamera();
                    Manager._managerInstance.Close_skeletoninfo();
                    Debug.Log("Resetting Zoom no target new method");
                }

                //if (EventSystem.current.currentSelectedGameObject != null)
                //{
                //    // if(Camera.main.orthographicSize != 7.5f)
                //    // {
                //    //OrthoZoom(defaultCenter, 7.5f);
                //    //ZoomOutCamera();
                //    Manager._managerInstance.Close_skeletoninfo();
                //    Debug.Log("Resetting Zoom no target");
                //}


            }

            if (Input.GetKeyDown(KeyCode.R))
                OrthoZoom(defaultCenter, defaultHeight);
        }

    }

    public float offset;
    private void OrthoZoom(Vector2 center, float regionHeight)
    {

        //LeanTween.move(this.gameObject, new Vector3(center.x, center.y + offset, defaultCenter.z), 0.25f);

        GetComponent<Camera>().transform.position = new Vector3(center.x, center.y+offset, defaultCenter.z);


      
        GetComponent<Camera>().orthographicSize = regionHeight;// regionHeight / 2f;
    }


   public void ZoomInCamera(Vector3 targetPos)
    {
        Manager._managerInstance.isCameraZoomed = true;
        LeanTween.move(this.gameObject, new Vector3(targetPos.x, targetPos.y + offset, zoomValue), 0.235f).setEase(LeanTweenType.easeInSine);
    }


   public  void ZoomOutCamera()
    {
        Manager._managerInstance.isCameraZoomed = false;
        bgLines.SetActive(false);
        LeanTween.move(this.gameObject, defaultPos, 0.235f).setEase(LeanTweenType.easeInSine);
        productviewSet = false;
    }

    private bool productviewSet;
    public void SetCameraInProductView()
    {
        if (productviewSet == false)
        {
            bgLines.SetActive(true);
            Vector3 newpos = new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z - 10);
            LeanTween.move(this.gameObject, newpos, 0.235f).setEase(LeanTweenType.easeInSine);
            productviewSet = true;
        }

    }

    private Collider GetTarget()
    {


        var hit = new RaycastHit();
        Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit);

       // Debug.Log("Hitting " + hit.collider.gameObject.name);
        return hit.collider;
    }


    public void ResetZoom()
    {
        if (Manager._managerInstance.isCameraZoomed)
        {
            OrthoZoom(defaultCenter, 7.5f);
            Manager._managerInstance.Close_skeletoninfo();
            Manager._managerInstance.isCameraZoomed = false;
            Debug.Log("Resetting Zoom");
        }
    }


    public void B_BackFromObjectView()
    {
        objectView.SetActive(false);
        Manager._managerInstance.Close_skeletoninfo();
        ResetZoom();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
