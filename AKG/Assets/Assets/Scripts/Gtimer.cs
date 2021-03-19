using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gtimer : MonoBehaviour {

    public Text _timer;
    int _minuite = TimerController.getMinuite();
    float _second = TimerController.getSec();

	// Use this for initialization
	void Start () {
		_timer.text = _minuite.ToString("00") + ":" + ((int)_second).ToString("00");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
