using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    [Serializable]
    public class BackDropElement
    {
        public string id;
        public Texture2D image;
    }

    public List<BackDropElement> backDropImages = new List<BackDropElement>();

    [Serializable]
    public class TreeElement
    {
        public string id;
        public Texture2D treeImage;
        public Texture2D ongoingTypeImage;
    }

    public List<TreeElement> treeElements = new List<TreeElement>();
}
