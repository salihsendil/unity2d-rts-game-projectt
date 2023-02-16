using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftCoordsPlanning : MonoBehaviour
{
    RaycastHit hit;
    public GameObject buildingPrefab;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z=10;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);//this sil dene
        }
    }
}
