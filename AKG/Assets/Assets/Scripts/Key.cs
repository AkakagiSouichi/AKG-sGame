using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public static int _key;
    public static int getKeyC()
    {
        return _key;
    }
    private Transform _transform;
    private float y;
    
    // Use this for initialization
    void Start () {
        _transform = GetComponent<Transform>();
        y = 90;
        
	}
	
	// Update is called once per frame
	void Update () {
        _transform.Rotate(new Vector3(0, y*Time.deltaTime, 0));
        y += 0.05f;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            aoudio();
            Destroy(this.gameObject);
            _key++;
        }
    }
    void aoudio()
    {
        
    }
}
