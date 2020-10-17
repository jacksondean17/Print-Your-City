using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Google.Maps.Coord;
using Google.Maps.Event;
using Google.Maps.Examples.Shared;
using Google.Maps.Examples;

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

    public GameObject prefab;

    float[] GetCoords()
    {

        Debug.Log("getting coords");
        if (latField.GetComponent<Text>().text == "" &&
            lonField.GetComponent<Text>().text == "")
        {
            // salt lake city
            lat = 40.760780F;
            lon = -111.891045F;
        }
        else
        {
            lat = float.Parse(latField.GetComponent<Text>().text);
            lon = float.Parse(lonField.GetComponent<Text>().text);
        }

        Debug.Log("Got coords");

        if (latWidthField.GetComponent<Text>().text == "" &&
            lonWidthField.GetComponent<Text>().text == "")
        {
            latWidth = 500F;
            lonWidth = 500F;
        }
        else
        {
            latWidth = float.Parse(latWidthField.GetComponent<Text>().text);
            lonWidth = float.Parse(lonWidthField.GetComponent<Text>().text);
        }

        float[] retArray = new float[] { lat, lon, latWidth, lonWidth };
        string s = "";
        foreach (float f in retArray)
        {
            s += f.ToString() + " ";
        }
        Debug.Log(s);
        return retArray;
    }

    public void LoadCity()
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

        GameObject map = Instantiate(prefab);
        BaseMapLoader loader = map.GetComponent<BaseMapLoader>();
        loader.SetPos(coords);
        loader.GetComponent<BaseMapLoader>().Create();


    }
}
