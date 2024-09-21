using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

[System.Serializable]
public class ItemData 
{
    public string id;
    public int value;
}
[System.Serializable]
public class ItemDataList 
{
    public ItemData[] itemData = new ItemData[0];
}

[CreateAssetMenu (fileName = "Item Library", menuName = "Configs/Inventory/new Item Library")]
public class ItemLibrary : ScriptableObject
{
    [SerializeField] private TextAsset _jsonData;
    [SerializeField] private Dictionary<string, ItemDefinition> _itemLibrary;
    [SerializeField] private ItemDataList _itemDataList = new ItemDataList();

    [Inject]
    public void Construct() 
    {
        Debug.Log("Cos");
       
        LoadData();
        _itemLibrary = new Dictionary<string, ItemDefinition>();
        Object[] allItemObj = Resources.LoadAll("Inventory/Items", typeof(ItemDefinition));
        ItemDefinition tempItemDefinition;
        for (int i = 0; i < allItemObj.Length; i++)
        {
            tempItemDefinition = (ItemDefinition)allItemObj[i];
            _itemLibrary.Add(tempItemDefinition.IdItem, tempItemDefinition);
        }


    }
    public ItemDefinition[] LoadInventory() 
    {

        List<ItemDefinition> allItem = new List<ItemDefinition>();
        if (_itemDataList.itemData.Length>0) 
        {
            for (int i = 0; i < _itemDataList.itemData.Length; i++)
            {
                ItemDefinition tempItem = _itemLibrary[_itemDataList.itemData[i].id].CreateInstance(_itemDataList.itemData[i].value);
                allItem.Add(tempItem);
            }
        }
       
        return allItem.ToArray();
    }
    [ContextMenu("sda")]
   
    public void LoadData()
    {
        var d = Resources.Load("SaveData/PlayerInventoryData");
        Debug.Log(d);
        _jsonData = d as TextAsset;
        Debug.Log(_jsonData.text);
        Debug.Log(_itemDataList);
        _itemDataList = JsonUtility.FromJson<ItemDataList>(_jsonData.text);

       
    }
    public void SaveData(InventoryManager Inventory) 
    {
        IInventoryItem[] tempItem = Inventory.AllItems;
       
        _itemDataList = new ItemDataList();

        _itemDataList.itemData = new ItemData[tempItem.Length];
        
        for (int i = 0; i < tempItem.Length; i++)
        {
            _itemDataList.itemData[i] = new ItemData();
            _itemDataList.itemData[i].id = tempItem[i].IdItem;
            _itemDataList.itemData[i].value = tempItem[i].ValueInStack;
        }
        string strOutput = JsonUtility.ToJson(_itemDataList);
        Debug.Log("SAve");
        File.WriteAllText(Application.dataPath +"/Project/Resources/SaveData/PlayerInventoryData.json", strOutput);
    }

}



