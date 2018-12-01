using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(transform.position.x+Time.deltaTime,0,0);
	}
}
