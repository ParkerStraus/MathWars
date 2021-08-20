using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public Image thumbnail;


    public void UpdateItem(PointAndClickInventoryItem item)
    {
        thumbnail.sprite = item.Texture;
    }
}
