using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject {

    public string ItemID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public GameObject Prefab { get; set; }
    public Material Material { get; set; }

    
}
