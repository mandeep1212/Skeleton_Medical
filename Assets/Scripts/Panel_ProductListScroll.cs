using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ProductListScroll : MonoBehaviour
{
    public GameObject _btnPrefab;
    public GameObject _sub_btnPrefab;
    public GameObject scrollContainer;

    public TMPro.TextMeshProUGUI _head;

    void BtnCilck(bool b, GameObject menu)
    {
        Debug.Log("hide and unhide submenu");
        API_Manager.INSTANCE.selectedType = menu.GetComponent<Button_Detail>().label;
        foreach (Transform g in scrollContainer.transform)
        {
            if (g.name.Contains(menu.name + "_submenu"))
            {
                g.gameObject.SetActive(b);
            }
        }
    }

    internal void UpdateHeading(RootObject_CircleLists allListsData)
    {
        _head.text = allListsData.allList.englishProductCategory;
        API_Manager.INSTANCE.selectedCategory = _head.text.ToUpper();
        //Clear Child
        foreach (Transform t in scrollContainer.transform)
        {
            Destroy(t.gameObject);
        }

        // Generate List
        for (int i = 0; i < allListsData.allList.productTypeList.Count; i++)
        {
            GameObject menu = Instantiate(_btnPrefab, scrollContainer.transform);
            menu.GetComponentInChildren<Text>().text = allListsData.allList.productTypeList[i].englishProductType;
            menu.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => BtnCilck(menu.GetComponent<Toggle>().isOn,menu));
            menu.GetComponent<Button_Detail>().id = allListsData.allList.productTypeList[i].enUniqueIdForSkeleton;
            menu.GetComponent<Button_Detail>().label = allListsData.allList.productTypeList[i].englishProductType;
            menu.name = menu.GetComponentInChildren<Text>().text;

            for(int j=0;j< allListsData.allList.productTypeList[i].productList.Count;j++)
            {
                GameObject submenu = Instantiate(_sub_btnPrefab, scrollContainer.transform);
                submenu.GetComponentInChildren<Text>().text = allListsData.allList.productTypeList[i].productList[j].englishProductName;
                submenu.GetComponent<Button>().onClick.AddListener(() => SubBtnCilck(submenu));
                submenu.GetComponent<Button_Detail>().id = allListsData.allList.productTypeList[i].productList[j].enUniqueIdForSkeleton;
                submenu.GetComponent<Button_Detail>().label = allListsData.allList.productTypeList[i].productList[j].englishProductName;
                submenu.name = menu.name + "_submenu";

                submenu.SetActive(false);
            }

        }
    }

    

    void SubBtnCilck(GameObject g)
    {
        Debug.Log("show detail CLICK");
        // FETCH JSON OF PRODUTC DETAIL
        OrthographicZoom.instance.SetCameraInProductView();
        API_Manager.INSTANCE.Fetch_API_detail(g.GetComponent<Button_Detail>().id);
    }
}
