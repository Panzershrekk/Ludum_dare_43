using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTitleScript : MonoBehaviour {

    public float speed = 10;

    private float dirX, dirY;

    private RectTransform rt;

    private void Start()
    {
        dirX = 1;
        dirY = 1;

        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        rt.position = rt.position + new Vector3(dirX * Time.deltaTime * speed, dirY * Time.deltaTime * speed);
        Debug.Log(rt.localPosition);
        if (rt.localPosition.y > 320)  dirY = -1;
        if (rt.localPosition.x > 100)  dirX = -1;
        if (rt.localPosition.y < 280)  dirY = 1;
        if (rt.localPosition.x < -100) dirX = 1;
	}
}
