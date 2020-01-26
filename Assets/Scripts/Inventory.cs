using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour {
    [Header("Resources")]
	public int spaceshipParts = 0;
    public int blueprints = 0;
    public int medipacks = 0;
    public int weapons = 0;
    public int research = 0;
    
    [Header("Regeneration Rate")]
	public int spaceshipPartsRegenRate = 0;
    public int blueprintsRegenRate = 0;
    public int medipacksRegenRate = 0;
    public int weaponsRegenRate= 0;
    public int researchRegenRate = 0;

    void Start()
    {
        InvokeRepeating("IncreaseInventoryUnits", 10f, 10f);
    }
    public void IncreaseInventoryUnits()
    {
        spaceshipParts += spaceshipPartsRegenRate;
        blueprints += blueprintsRegenRate;
        medipacks += medipacksRegenRate;
        weaponsRegenRate += weaponsRegenRate;
        research += researchRegenRate;
    }
}