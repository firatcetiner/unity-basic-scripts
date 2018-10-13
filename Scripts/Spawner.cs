/*
 * Spawner.cs
 * Spawn a prefab at touch position.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject obj;

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Firts touch began
        {
            Vector3 touchPos = Input.GetTouch(0).position; // Get touch position
            touchPos.z = 5;
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);
            Instantiate(obj, touchPos, Quaternion.identity); // Instantiate given GameObject with fixed rotation

        }
    }
}