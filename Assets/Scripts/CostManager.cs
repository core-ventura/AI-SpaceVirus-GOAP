using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    int gatherSpaceshipPartsCost = 1;
    int gatherBlueprintsCost = 1;
    int gatherResearchCost = 1;
    public Inventory storageFacilityInventory;

    GatherSpaceshipPartsFromCrashSite[] gatherSpaceshipPartsFromCrashSiteActions;
    GatherBlueprintsFromCommunicationsStation[] gatherBlueprintsFromCommunicationsStationActions;    
    GatherResearchFromInvestigationZone[] gatherResearchFromInvestigationZoneActions;
    // Update is called once per frame
    void Update()
    {
        // Update global costs
        gatherSpaceshipPartsCost = storageFacilityInventory.spaceshipParts;
        gatherBlueprintsCost = storageFacilityInventory.blueprints;        
        gatherResearchCost = storageFacilityInventory.research;

        // Find all gathering actions
        gatherSpaceshipPartsFromCrashSiteActions = FindObjectsOfType<GatherSpaceshipPartsFromCrashSite>();
        gatherBlueprintsFromCommunicationsStationActions = FindObjectsOfType<GatherBlueprintsFromCommunicationsStation>();
        gatherResearchFromInvestigationZoneActions = FindObjectsOfType<GatherResearchFromInvestigationZone>();  

        // Update their costs
        foreach (GatherSpaceshipPartsFromCrashSite gatheringAction in gatherSpaceshipPartsFromCrashSiteActions)
        {
            gatheringAction.cost = gatherSpaceshipPartsCost;
        }
        foreach (GatherBlueprintsFromCommunicationsStation gatheringAction in gatherBlueprintsFromCommunicationsStationActions)
        {
            gatheringAction.cost = gatherBlueprintsCost;
        }
        foreach (GatherResearchFromInvestigationZone gatheringAction in gatherResearchFromInvestigationZoneActions)
        {
            gatheringAction.cost = gatherResearchCost;
        }

    }

}
