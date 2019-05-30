using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {

    public GameObject player;
    public Camera cam;
    private Vector3 playerPos;
    private RectTransform rt;
    private Vector3 playerScreenPos;

	// Use this for initialization
	void Start () {
        playerPos = player.transform.position;

        rt = GetComponent<RectTransform>();
        playerScreenPos = cam.WorldToViewportPoint(player.transform.TransformPoint(playerPos));
        rt.anchorMin = playerScreenPos;
        rt.anchorMax = playerScreenPos;
	}
	
	// Update is called once per frame
	void Update () {
        playerScreenPos = cam.WorldToViewportPoint(player.transform.TransformPoint(playerPos));
        rt.anchorMin = playerScreenPos;
        rt.anchorMax = playerScreenPos;

    }
}
