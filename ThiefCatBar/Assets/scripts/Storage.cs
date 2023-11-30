using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<Ingredient> storedItems = new List<Ingredient>();
    
    public Vector3 itemPosition;

    public void StoreItem(Ingredient item)
    {
        storedItems.Add(item);
        item.transform.SetParent(transform);
        item.transform.localPosition = itemPosition;
        item.transform.position += new Vector3(0.1f * storedItems.Count, 0);


    }
}
