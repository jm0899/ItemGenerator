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
        Array values;
        System.Random random = new System.Random();
        //randomize all info
      
        //Generate random item stats
        values = Enum.GetValues(typeof(PotionQuality));
        var randomQuality = (PotionQuality)values.GetValue(random.Next(values.Length));
        potionQuality = randomQuality;

        switch (potionQuality)
        {
            case PotionQuality.COMMON:
                healAmount = 50;
                break;
            case PotionQuality.RARE:
                healAmount = 500;
                break;
            case PotionQuality.LEGENDARY:
                healAmount = 1000;
                break;
        }

        values = Enum.GetValues(typeof(PotionBuff));
        var randomBuff = (PotionBuff)values.GetValue(random.Next(values.Length));
        potionBuff = randomBuff;

        ItemID = string.Format("ID{0}POT{1}", potionBuff, random.Next(0, 100));
        Name = string.Format("{0} potion of {1} restoration.", potionQuality.ToString().ToLower(), potionBuff.ToString().ToLower());
        Description = string.Format("Heals your {0} for {1} points.", potionBuff.ToString().ToLower(), healAmount);
        Prefab = RandomizePrefab();
        Material = RandomizeMaterial(Prefab);
    }

    private Material RandomizeMaterial(GameObject potion)
    {
        //Create a new Material List
        List<Material> matsList = new List<Material>();

        //Material Directory
        string path = "ItemData\\Materials\\Potion";

        //Load All Materials
        UnityEngine.Object[] MatDirectory = Resources.LoadAll(path,typeof(Material));

        //Add materials to list
        foreach (Material mat in MatDirectory)
        {
            Material matAsset = mat;
            matsList.Add(matAsset);
        }

        //Get a random material from list
        Material newMat = matsList[UnityEngine.Random.Range(0, matsList.Count)];
        Renderer rend = potion.GetComponent<Renderer>();
      
        //set material
        rend.material = newMat;
     
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
        
               
        UnityEngine.Object[] potions = LoadAssets();
        GameObject p = (GameObject)potions[UnityEngine.Random.Range(0, potions.Length)];
        
        return p;
     
    }

    public void Spawn(GameObject potion)
    {
        Instantiate(potion);
    }
    private UnityEngine.Object[] LoadAssets()
    {
        string path = "Assign a Path";
        
        switch (potionBuff)
        {
            case PotionBuff.HEALTH:
                path = "Potions\\HEALTH";
                
                break;
            case PotionBuff.MANA:
                path = "Potions\\MANA";
                break;
            case PotionBuff.STAMINA:
                path = "Potions\\STAMINA";
                break;

        }
        
        UnityEngine.Object[] assets = LoadFromDB(path);

        
        return assets;
    }
}
