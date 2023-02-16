using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuilding : MonoBehaviour
{
    public GameObject buildDraftPrefab;

    public void BuildDraft()
    {
        GameObject buildDraft = Instantiate(buildDraftPrefab);
        DraftCoordsPlanning coordsPlanning = buildDraft.GetComponent<DraftCoordsPlanning>();
        coordsPlanning.enabled = true;
    }
}