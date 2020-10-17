using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{

    public GameObject latField;
    public GameObject lonField;
    public GameObject latWidthField;
    public GameObject lonWidthField;
    float lat;
    float lon;
    float latWidth;
    float lonWidth;


    float[] GetCoords()
    {

        Debug.Log("getting coords");
        lat = float.Parse(latField.GetComponent<Text>().text);
        lon = float.Parse(lonField.GetComponent<Text>().text);
        Debug.Log("Got coords");
        latWidth = float.Parse(latWidthField.GetComponent<Text>().text);
        lonWidth = float.Parse(lonWidthField.GetComponent<Text>().text);
        float[] retArray = new float[] { lat, lon, latWidth, lonWidth };
        string s = "";
        foreach (float f in retArray)
        {
            s += f.ToString() + " ";
        }
        Debug.Log(s);
        return retArray;
    }

    public void PrintCity()
    {
        float[] coords;
        try
        {
            // lat, lon, latWidth, lonWidth
            coords = GetCoords();
        }
        catch (FormatException) // incorrect string
        {
            return;
        }

        CreateWorld(coords);
    }

    private void CreateWorld(float[] coords)
    {

    }
}
