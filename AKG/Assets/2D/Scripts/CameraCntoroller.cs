using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntoroller : MonoBehaviour {

    GameObject Player;

    void Start () {
        Player = GameObject.Find("Yuko");
        
    }
	
	
	void LateUpdate () {
        transform.position = new Vector3(Player.transform.position.x+4, 2, -10);
        
    }
}
