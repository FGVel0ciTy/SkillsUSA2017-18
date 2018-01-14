﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour {

    public GameObject PingPrefab;
    public GameObject TorpedoPrefab;
    public float pingForce = 1000;
    public float torpedoForce = 1500;


    // seconds per shot
    public float pingRateOfFire = 1f;
    float timeSincePing;

    public float torpedoRateOfFire = 2f;
    float timeSinceTorpedo;

    // Use this for initialization
    void Start()
    {
        timeSincePing = pingRateOfFire;
        timeSinceTorpedo = torpedoRateOfFire;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        // left click: torpedo
        if (Input.GetButton("Fire1") && timeSinceTorpedo >= torpedoRateOfFire)
        {
            timeSinceTorpedo = 0;
            Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
            CmdFireProj(mousePos, TorpedoPrefab, torpedoForce);
        }

        // right click: ping
        if (Input.GetButton("Fire2") && timeSincePing >= pingRateOfFire)
        {
            timeSincePing = 0;
            Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
            CmdFireProj(mousePos, PingPrefab, pingForce);
        }

        timeSincePing += Time.deltaTime;
        timeSinceTorpedo += Time.deltaTime;
    }

    // called by client, run on server
    [Command]
    void CmdFireProj(Vector2 mousePos, GameObject prefab, float firingForce)
    {
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        var projectile = Instantiate(prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
        //ping.GetComponent<Ping>().pinger = gameObject; // ignore collision solution
        projectile.GetComponent<Rigidbody2D>().AddForce(mousePos.normalized * firingForce);

        NetworkServer.Spawn(projectile);    
    }
}
