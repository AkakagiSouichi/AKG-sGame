using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour {

    [SerializeField] private static int _minuite;
    [SerializeField] private static float _seconds;
    public static int getMinuite()
    {
        return _minuite;
    }
    public static float getSec()
    {
        return _seconds;
    }
    private float _oldsecond;
    public Text _timer;

	// Use this for initialization
	void Start () {
        _minuite = 0;
        _seconds = 0f;
        _oldsecond = 0f;

	}
	
	// Update is called once per frame
	void Update () {
        _seconds += Time.deltaTime;
        if (_seconds >= 60f)
        {
            _minuite++;
            _seconds = _seconds - 60f;
        }

        if((int)_seconds != (int)_oldsecond)
        {
            _timer.text = _minuite.ToString("00") + ":" + ((int)_seconds).ToString("00");
        }
        _oldsecond = _seconds;

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("main");
        }
	}
}
