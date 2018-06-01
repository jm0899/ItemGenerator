using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;
using System;

public class PotionData : ItemData {

    public PotionQuality potionQuality;
    public PotionBuff potionBuff;
    public int healAmount;

    public void Randomize()
    {
        int bonus;
        System.Random random = new System.Random();
        //randomize all info
        switch (potionQuality)
        {
            case PotionQuality.COMMON:
                bonus = 50;
                healAmount = bonus;
                break;
            case PotionQuality.RARE:
                bonus = 500;
                healAmount = bonus;
                break;
            case PotionQuality.LEGENDARY:
                bonus = 1000;
                healAmount = bonus;
                break;
        }
        ItemID = string.Format("ID{0}POT{1}",potionBuff, random.Next(0,100));
        Name = string.Format("{0} potion of {1} restoration.",potionQuality.ToString(), potionBuff.ToString());
        Description = string.Format("Heals your {0} for {1}", potionBuff, healAmount);
        
        Prefab = RandomizePrefab();
        Material = RandomizeMaterial(Prefab);
    }

    private Material RandomizeMaterial(GameObject potion)
    {
        //Create a new Material List
        List<Material> matsList = new List<Material>();

        //Material Directory
        string path = "";

        //Load All Materials
        UnityEngine.Object[] MatDirectory = Resources.LoadAll(path,typeof(Material));

        //Add materials to list
        foreach (Material mat in MatDirectory)
        {
            Material matAsset = mat;
            matsList.Add(matAsset);
        }
        //Get material of prefab
        Material newMat = potion.GetComponent<Renderer>().material;

        //Change to random
        newMat = matsList[UnityEngine.Random.Range(0, matsList.Count)];
        return newMat;

    }

    private UnityEngine.Object[] LoadFromDB(string path)
    {
        return Resources.LoadAll(path, typeof(GameObject));
    }
    private GameObject RandomizePrefab()
    {
        //Create a new Game Object
        //Load Assets
        //Instantiate new game object from a random asset
        GameObject p = new GameObject();
        UnityEngine.Object[] potions = LoadAssets();
        p = (GameObject)potions[UnityEngine.Random.Range(0, potions.Length)];
        GameObject pot = Instantiate(p);
        return pot;
     
    }

    private UnityEngine.Object[] LoadAssets()
    {
        string path = "Assign a Path";
        UnityEngine.Object[] assets = Resources.LoadAll(path, typeof(GameObject));
        switch (potionBuff)
        {
            case PotionBuff.HEALTH:
                path = "\\Potions\\HEALTH";
                break;
            case PotionBuff.MANA:
                path = "\\Potions\\MANA";
                break;
            case PotionBuff.STAMINA:
                path = "\\Potions\\STAMINA";
                break;

        }

        if (path == "Assign a path")
        {
            //error path not assigned todo..
        }
        return assets;
    }
}
