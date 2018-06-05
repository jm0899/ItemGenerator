using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ItemInfo;
using System.IO;

public class PotionGeneratorWindow : EditorWindow{

    public static PotionData PotionInfo;


    [MenuItem("Window/Potion Generator")]
    static void OpenWindow()
    {
        //Opens the generator window of the specified size
        PotionGeneratorWindow window = (PotionGeneratorWindow)GetWindow(typeof(PotionGeneratorWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    public void OnEnable()
    {
        InitData();
    }

    private void OnGUI()
    {
        DrawSettings();
    }

    private void InitData() {

        PotionInfo = (PotionData)CreateInstance(typeof(PotionData));
        PotionInfo.Randomize();
    }

    private void DrawSettings() {
        //Display in editor
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ItemID");
        PotionInfo.ItemID = EditorGUILayout.TextField(PotionInfo.ItemID);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        PotionInfo.Name = EditorGUILayout.TextField(PotionInfo.Name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type");
        PotionInfo.potionBuff = (PotionBuff)EditorGUILayout.EnumPopup(PotionInfo.potionBuff);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Quality");
        PotionInfo.potionQuality = (PotionQuality)EditorGUILayout.EnumPopup(PotionInfo.potionQuality);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Description");
        PotionInfo.Description = EditorGUILayout.TextField(PotionInfo.Description);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        EditorGUILayout.ObjectField(PotionInfo.Prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Material");
        EditorGUILayout.ObjectField(PotionInfo.Material, typeof(Material), false);
        EditorGUILayout.EndHorizontal();

        if (PotionInfo.Prefab == null)
        {
            EditorGUILayout.HelpBox("This potion needs a [Prefab] before it can be created.", MessageType.Warning);
        }
        else if (PotionInfo.Name == null || PotionInfo.Name.Length < 1)
        {
            EditorGUILayout.HelpBox("This potion needs a [Name] before it can be created.", MessageType.Warning);
        }

        if (GUILayout.Button("New random potion", GUILayout.Height(40)))
        {
            //Get random potion data
            PotionInfo.Randomize();
        }
       
        if (GUILayout.Button("View Game Object", GUILayout.Height(40)))
        {
            //Spawns the gameobject in the scene
            PotionInfo.Spawn(PotionInfo.Prefab);
        }

        if (GUILayout.Button("Finish and Save", GUILayout.Height(40)))
        {
            //Save Potion as new asset
            SaveNewPotion();
        }
    }

    private void SaveNewPotion()
    {
        //Set paths
        string prefab_path;
        string new_prefab_path = "Assets/Prefabs/";
        string data_path = "Assets/Resources/ItemData/Data/";
        
        //Save data with item id
        data_path += PotionInfo.ItemID + ".asset";

      
        try {
            //Create new asset with potion data component
            AssetDatabase.CreateAsset(PotionInfo, data_path);
            prefab_path = AssetDatabase.GetAssetPath(PotionInfo.Prefab);
            new_prefab_path += PotionInfo.ItemID + ".prefab";

            AssetDatabase.CopyAsset(prefab_path, new_prefab_path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            GameObject potionPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(new_prefab_path, typeof(GameObject));

            //check if go has potion data component
            if (!potionPrefab.GetComponent<Potion>())
            {
                potionPrefab.AddComponent(typeof(Potion));
                potionPrefab.GetComponent<Potion>().potionData = PotionInfo;
            }
        }
        catch (Exception e)
        {
                Debug.LogException(e, this);
        }


    }
}
