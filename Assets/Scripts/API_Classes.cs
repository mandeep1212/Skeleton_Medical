using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProductList
{
    public int enUniqueIdForSkeleton;
    public string englishProductName;
    public string spanishProductName;
}

[Serializable]
public class ProductTypeList
{
    public int enUniqueIdForSkeleton;
    public string englishProductType;
    public string spanishProductType;
    public List<ProductList> productList = new List<ProductList>();
}

[Serializable]
public class AllList
{
    public int enUniqueIdForSkeleton;
    public string englishProductCategory;
    public string spanishProductCategory;
    public List<ProductTypeList> productTypeList = new List<ProductTypeList>();
}

[Serializable]
public class RootObject_CircleLists
{
    public AllList allList;
    public string success;
}


////////////////////////////////  PRODUCT DETAIL //////////////////////////////////////////

[Serializable]
public class Attachment
{
    public int attachmentId;
    public string attachmentName;
    public string attachmentType;
    public string pic;
    public DateTime attachCreatedAt;
    public DateTime attachUpdatedAt;
    public string enDescription;
    public string spDescription;
    public int serialNumber;
}

[Serializable]
public class ProductVideo
{
    public int videoId;
    public string videoUrl;
    public string code;
    public DateTime date;
}

[Serializable]
public class RootObject_ProductDetail
{
    public int productId;
    public string englishProductName;
    public int enUniqueIdForSkeleton;
    public string spanishProductName;
    public string englishProductDescription;
    public string spanishProductDescription;
    public DateTime productCreatedAt;
    public DateTime productUpdatedAt;
    public List<Attachment> attachments;
    public List<ProductVideo> productVideos;
    public int enProductTypeUniqueIdForSkeletion;
    public bool status;
}



