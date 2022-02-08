using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Dictionary<int, GameObject> ItemList = new Dictionary<int, GameObject>();

    public bool isInlinking = false;
    // Start is called before the first frame update
    
    public void RemoveItem(int itemInstanceId)
    {
        ItemList.Remove(itemInstanceId);
    }

    public void AddNewItem(int itemInstanceId, GameObject itemGameObject)
    {
        ItemList.Add(itemInstanceId,itemGameObject);
    }

    public Dictionary<int, GameObject> GetItemList()
    {
        return ItemList;
    }

    public bool CheckValid()
    {
        foreach (var item in ItemList)
        {
            var _item = item.Value.GetComponent<Item>();
        }

        return false;
    }
}
