using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleWildEvent : MonoBehaviour {

    public WildEventsScript we;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(1);
        Debug.Log("Bonjour !");
        we.FireWildEvent("EventBonjour");
	}	
}
