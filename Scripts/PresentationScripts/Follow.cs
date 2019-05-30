using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float smoothie = 0.2f;

    private Vector3 velocity = Vector3.zero;

    public float XMaxValue = 0f;
    public float XMinValue = 0f;
    public bool XMaxEnabled = false;
    public bool XMinEnabled = false;

    private void Awake()
    {
        Vector2 clamps = ScreenSwitches.getCamera("clamps", "prison");
        Vector2 pos = ScreenSwitches.getCamera("position", "prison");
        transform.position = new Vector3(pos.x, pos.y, -10);
        XMinValue = clamps.x;
        XMaxValue = clamps.y;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 targetPos = transform.position;

        targetPos.x = target.position.x;

        if (XMaxEnabled && XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        else
        {
            if (XMinEnabled)
                targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
            if (XMaxEnabled)
                targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothie);
	}

    public void ChangeRoom(string to)
    {
        Vector2 clamps = ScreenSwitches.getCamera("clamps", to);
        Vector2 pos = ScreenSwitches.getCamera("position", to);
        transform.position = new Vector3(pos.x, pos.y, -10);
        XMinValue = clamps.x;
        XMaxValue = clamps.y;
    }
}
