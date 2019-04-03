using UnityEngine;
using System.Collections;


public class Rotation : MonoBehaviour
{
    public Transform skeleton;
    public float rotationSpeed = 0.2f;
    public Camera mainCamera;




    void OnMouseDrag()
    {
        Debug.Log("Dragging Mouse");
        //  if (Manager._managerInstance.skeletonInfoPanel.GetBool("toup") == false)
        if (Manager._managerInstance.isCameraZoomed == false)
        { 
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
  
             skeleton.RotateAround(Vector3.down, XaxisRotation);
          
        }
    }



    public void ResetRotation()
    {
        skeleton.eulerAngles = Vector3.zero;
    }



}