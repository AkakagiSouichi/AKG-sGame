using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour {

    [SerializeField] Text _TimerT;
    [SerializeField] int _min;
    [SerializeField] float _sec;
    private float _sec2;

    // Use this for initialization
    void Start () {
        _min = 0;
        _sec = 0f;
        _sec2 = 0f;
        _TimerT.text = _min.ToString("00") + " : " + _sec.ToString("00");
	}
	
	// Update is called once per frame
	void Update () {
        _sec += Time.deltaTime;
        if(_sec >= 60f)
        {
            _min++;
            _sec = _sec - 60f;
        }

        if(_sec != _sec2)
        {
            _TimerT.text = _min.ToString("00") + " : " + _sec.ToString("00");
        }
        _sec2 = _sec;
	}
}
