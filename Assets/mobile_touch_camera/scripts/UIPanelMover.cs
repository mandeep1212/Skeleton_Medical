using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class UIPanelMover : MonoBehaviour
{
    private RectTransform deafaultPos;
    [SerializeField]
    public Vector3 targetpos;


    // Start is called before the first frame update
    void start()
    {
        
    }

    public void MoveIn()
    {
        LeanTween.move(GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.25f);
    }

    public void Moveout()
    {
        LeanTween.move(GetComponent<RectTransform>(), targetpos, 0.25f);
    }

}
