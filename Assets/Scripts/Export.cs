using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export : MonoBehaviour
{
    public void ExportToDisk()
    {
        List<Terrain> terrains = new List<Terrain>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Terrain>() != null)
            {
                terrains.Add(child.GetComponent<Terrain>());
            }
        }
        Debug.Log("Terrains");
        foreach (Terrain terrain in terrains)
        {
            Debug.Log(terrain.ToString());
        }
    }
}
