using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI crashSiteResourcesTMP;
    public TextMeshProUGUI communicationStationResourcesTMP;
    public TextMeshProUGUI storageFacilitySpaceshipPartsTMP;
    public TextMeshProUGUI storageFacilityBlueprintsTMP;
    public TextMeshProUGUI storageFacilityResearchTMP;
    public TextMeshProUGUI infirmaryMedipacksTMP;
    public TextMeshProUGUI armoryWeaponsTMP;
    public TextMeshProUGUI investigationZoneResearchTMP;

    public Inventory crashSiteInventory;
    public Inventory communicationStationInventory;
    public Inventory storageFacilityInventory;
    public Inventory infirmaryInventory;
    public Inventory armoryInventory;
    public Inventory investigationZoneInventory;

    void Update()
    {
        crashSiteResourcesTMP.text = crashSiteInventory.spaceshipParts.ToString() + " Spaceship Parts";
        communicationStationResourcesTMP.text = communicationStationInventory.blueprints.ToString() + " Blueprints";
        storageFacilitySpaceshipPartsTMP.text = storageFacilityInventory.spaceshipParts.ToString() + " Spaceship Parts";
        storageFacilityBlueprintsTMP.text = storageFacilityInventory.blueprints.ToString() + " Blueprints";
        storageFacilityResearchTMP.text = storageFacilityInventory.research.ToString() + " Research";
        infirmaryMedipacksTMP.text = infirmaryInventory.medipacks.ToString() + " Medipacks";
        armoryWeaponsTMP.text = armoryInventory.weapons.ToString() + " Weapons";
        investigationZoneResearchTMP.text = investigationZoneInventory.research.ToString() + " Research";
    }
}
