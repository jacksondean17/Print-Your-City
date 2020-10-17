using Google.Maps.Coord;
using Google.Maps.Event;
using Google.Maps.Examples.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    /// <summary>
    /// This example demonstrates a basic usage of the Maps SDK for Unity.
    /// </summary>
    /// <remarks>
    /// By default, this script loads the Statue of Liberty. If a new lat/lng is set in the Unity
    /// inspector before pressing start, that location will be loaded instead.
    /// </remarks>
    [RequireComponent(typeof(Google.Maps.MapsService))]
    public class GetMapResponse : MonoBehaviour
    {
        [Tooltip("LatLng to load (must be set before hitting play).")]
        public LatLng LatLng = new LatLng(40.6892199, -74.044601);
        public int northSouthBound = 500;
        public int eastWestBound = 500;
        public UnityEngine.Material modeledMaterial;
        public UnityEngine.Material extrudedMaterial;
        private bool isLoaded = false;
        public GameObject togglePrefab;

        /// <summary>
        /// Use <see cref="MapsService"/> to load geometry.
        /// </summary>
        private void Start()
        {
            // Get required MapsService component on this GameObject.
            Google.Maps.MapsService mapsService = GetComponent<Google.Maps.MapsService>();

            // Set real-world location to load.
            mapsService.InitFloatingOrigin(LatLng);

            // Register a listener to be notified when the map is loaded.
            mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);
            mapsService.Events.ModeledStructureEvents.WillCreate.AddListener(WillCreateModeled);
            mapsService.Events.ExtrudedStructureEvents.WillCreate.AddListener(WillCreateExtruded);

            // Load map with default options.
            Bounds bounds = new Bounds(Vector3.zero, new Vector3(northSouthBound, 0, eastWestBound));
            mapsService.LoadMap(bounds, ExampleDefaults.DefaultGameObjectOptions);
        }

        /// <summary>
        /// Example of OnLoaded event listener.
        /// </summary>
        /// <remarks>
        /// The communication between the game and the MapsSDK is done through APIs and event listeners.
        /// </remarks>
        public void OnLoaded(MapLoadedArgs args)
        {
            isLoaded = true;
            // The Map is loaded - you can start/resume gameplay from that point.
            // The new geometry is added under the GameObject that has MapsService as a component.
            Debug.Log(transform);
            List<Transform> ExtrudedBuildings = new List<Transform>();
            GameObject scrollView = GameObject.Find("Building Scroll View/Viewport/Content");
            foreach (Transform child in transform)
            {
                Debug.Log(child);
                if (child.GetComponent<Google.Maps.Unity.ExtrudedStructureComponent>() != null)
                {
                    child.transform.localScale = new Vector3(2, 2, 2);
                    ExtrudedBuildings.Add(child);
                }
            }

            int count = 0;
            scrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ExtrudedBuildings.Count * 21);
            foreach (Transform building in ExtrudedBuildings)
            {
                count++;
                GameObject addedChild = (GameObject)Instantiate(togglePrefab, scrollView.transform);
                addedChild.GetComponent<Transform>().position = new Vector3(110, count * 20, 0);

                Text text = addedChild.GetComponentInChildren<Text>();
                text.text = building.GetComponent<Google.Maps.Unity.ExtrudedStructureComponent>().GetMapFeature().MapFeatureMetadata.Name;

                Toggle toggle = addedChild.GetComponent<Toggle>();
                toggle.onValueChanged.AddListener((bool toggled) =>
                {
                    building.GetComponent<MeshRenderer>().enabled = !building.GetComponent<MeshRenderer>().enabled;
                });
            }
        }

        public void OnSizeChange()
        {
            if (isLoaded)
            {
                float value = GameObject.Find("SizeSlider").GetComponent<Slider>().value;
                foreach (Transform child in transform)
                {
                    Debug.Log(child);
                    if (child.GetComponent<Google.Maps.Unity.ExtrudedStructureComponent>() != null)
                    {
                        child.transform.localScale = new Vector3(value, value, value);
                    }
                }
            }
        }

        public void WillCreateModeled(WillCreateModeledStructureArgs args)
        {
            Google.Maps.Feature.Style.ModeledStructureStyle.Builder newStyle = new Google.Maps.Feature.Style.ModeledStructureStyle.Builder();
            newStyle.Material = modeledMaterial;

            //Debug.Log(args.MapFeature.GameObjectName());
            Debug.Log(args.MapFeature.MapFeatureMetadata.Name);
            args.Style = newStyle.Build();
        }

        public void WillCreateExtruded(WillCreateExtrudedStructureArgs args)
        {
            Google.Maps.Feature.Style.ExtrudedStructureStyle.Builder newStyle = new Google.Maps.Feature.Style.ExtrudedStructureStyle.Builder();
            newStyle.RoofMaterial = extrudedMaterial;
            newStyle.WallMaterial = extrudedMaterial;

            //Debug.Log(args.MapFeature.GameObjectName());
            if (args.MapFeature.MapFeatureMetadata.Name == "")
            {
                args.Cancel = true;
            }
            else
            {
                Debug.Log(args.MapFeature.MapFeatureMetadata.Name);
                args.Style = newStyle.Build();
            }

        }
    }
}
