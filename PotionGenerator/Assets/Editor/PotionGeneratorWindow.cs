using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PotionGeneratorWindow : EditorWindow{

    public static PotionData PotionInfo { get; set; }


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
        //Get random potion data
        PotionInfo.Randomize();
    }

    private void DrawSettings() {
        //Display in editor


        if (GUILayout.Button("Edit", GUILayout.Height(40)))
        {
            //Open an edit window
        }

        if (GUILayout.Button("Finish and Save", GUILayout.Height(40)))
        {
            //Save Potion as new asset
            SaveNewPotion();
        }
    }

    private void SaveNewPotion()
    {
        string prefab_path;
        string new_prefab_path = "";
        string data_path = " ";

        data_path += "Potion/" + PotionInfo.Name + ".asset";
        AssetDatabase.CreateAsset(PotionInfo, data_path);

        prefab_path = AssetDatabase.GetAssetPath(PotionInfo.Prefab);
        new_prefab_path += "Potion/" + PotionInfo.Name + ".prefab";

        AssetDatabase.CopyAsset(prefab_path, new_prefab_path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GameObject potionPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(new_prefab_path, typeof(GameObject));

        if (!potionPrefab.GetComponent<Potion>())
        {
            potionPrefab.AddComponent(typeof(Potion));
            potionPrefab.GetComponent<Potion>().potionData = PotionInfo;
        }

    }
}
