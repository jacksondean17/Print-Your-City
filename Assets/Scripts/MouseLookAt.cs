using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLookAt : MonoBehaviour {
    protected bool locked = false;
    private bool lastState = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        bool currentState = Input.GetKey(KeyCode.Escape);
        if (currentState && currentState!=lastState)
        {
            locked = !locked;
        }
        if (!locked)
        {
            GameObject.Find("SizeSlider").GetComponent<Slider>().enabled = false;
            Look();
        }
        else
        {
            GameObject.Find("SizeSlider").GetComponent<Slider>().enabled = true;
        }
        lastState = currentState;
    }

    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    public void Look () // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        rotation.y += Input.GetAxis ("Mouse X");
        rotation.x += -Input.GetAxis ("Mouse Y");
        Camera.main.transform.localRotation = Quaternion.Euler (rotation.x * lookSpeed, rotation.y * lookSpeed, 0);
    }
}