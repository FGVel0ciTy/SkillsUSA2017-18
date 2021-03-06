﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// works regardless on ControllerEnabled... i was lazy
public class GridExpand : MonoBehaviour {
    RectTransform rect;
    public float scaleAmount;
    float mapWidth;
    float mapHeight;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        mapWidth = ServerManager.instance.mapWidth;
        mapHeight = ServerManager.instance.mapHeight;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Map") && !GameManager.instance.playerSettings.InputIsDisabled)
        {
            // keep constant height
            rect.localScale = new Vector3((scaleAmount / mapHeight) * mapWidth, scaleAmount, 1f);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0, 0); 
        }
        else
        {
            rect.localScale = new Vector3((1 / mapHeight) * mapWidth, 1, 1);
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            rect.anchoredPosition = new Vector2(-10, 10);
        }
    }
}
