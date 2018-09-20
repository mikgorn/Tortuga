using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemEditor : EditorWindow {

    public Inventory inventoryItemList;
    private int viewIndex = 1;

    [MenuItem("Window/Inventory Item Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(ItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            inventoryItemList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(Inventory)) as Inventory;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);
        if (inventoryItemList != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = inventoryItemList;
            }
        }
        if (GUILayout.Button("Open Item List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = inventoryItemList;
        }
        GUILayout.EndHorizontal();

        if (inventoryItemList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (inventoryItemList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < inventoryItemList.items.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (inventoryItemList.items == null)
                Debug.Log("wtf");
            if (inventoryItemList.items.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.items.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("of   " + inventoryItemList.items.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                inventoryItemList.items[viewIndex - 1].name = EditorGUILayout.TextField("Item Name", inventoryItemList.items[viewIndex - 1].name as string);
                inventoryItemList.items[viewIndex - 1].image = EditorGUILayout.ObjectField("Item Icon", inventoryItemList.items[viewIndex - 1].image, typeof(Sprite), false) as Sprite;
                inventoryItemList.items[viewIndex - 1].amount = EditorGUILayout.IntField("Item Amount", inventoryItemList.items[viewIndex - 1].amount);

                GUILayout.Space(10);

                //GUILayout.BeginHorizontal();
                //inventoryItemList.items[viewIndex - 1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex - 1].isUnique, GUILayout.ExpandWidth(false));
                //inventoryItemList.items[viewIndex - 1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));
                //inventoryItemList.items[viewIndex - 1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex - 1].isQuestItem, GUILayout.ExpandWidth(false));
                //GUILayout.EndHorizontal();

                GUILayout.Space(10);

                //GUILayout.BeginHorizontal();
                //inventoryItemList.itemList[viewIndex - 1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
                //inventoryItemList.itemList[viewIndex - 1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex - 1].encumbranceValue, GUILayout.ExpandWidth(false));
                //GUILayout.EndHorizontal();

                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This Inventory List is Empty.");
            }
        }
        if (GUI.changed)
        {
           // EditorUtility.SetDirty(inventoryItemList);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        inventoryItemList = CreateInventory.Create();
        if (inventoryItemList)
        {
            inventoryItemList.items = new List<Item>();
            string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            inventoryItemList = AssetDatabase.LoadAssetAtPath(relPath, typeof(Inventory)) as Inventory;
            if (inventoryItemList.items == null)
                inventoryItemList.items = new List<Item>();
            if (inventoryItemList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Item newItem = new Item();
        newItem.name = "New Item";
        inventoryItemList.add_item(newItem);
        viewIndex = inventoryItemList.items.Count;
    }

    void DeleteItem(int index)
    {
        inventoryItemList.items.RemoveAt(index);
    }
}
