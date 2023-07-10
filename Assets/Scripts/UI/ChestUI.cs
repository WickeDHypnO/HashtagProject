using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour
{
    [SerializeField]
    ItemBuilder _itemBuilder;
    [SerializeField]
    List<Item> _items = new List<Item>();
    public ItemHandler itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++) 
        {
            var prefab = Instantiate(itemPrefab, transform);
            prefab.item = _itemBuilder.GenerateRandomItem();
            prefab.name = prefab.item.itemName;
            prefab.transform.position = new Vector3(Random.Range(200f, 400f), Random.Range(200f, 400f));
            prefab.GetComponentInChildren<TextMeshProUGUI>().SetText(prefab.item.itemName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
