using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour {

    int _key;
    public Text _keyN,_No;
    


	// Use this for initialization
	void Start ()
    {
        _keyN.text = " 0 / 3";
        _key = 0;
        _No.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        _key = Key.getKeyC();
        if (_key < 3)
        {
            _keyN.text = _key + " / 3";
        }else if(_key == 3)
        {
            _keyN.text = "EXIT";
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("gameover");
        }

        if (hit.gameObject.tag == "Door")
        {
            if(_key == 3)
            {
                SceneManager.LoadScene("goal");
            }else if (_key < 3)
            {
                _No.enabled = true;
                Invoke("Diray", 2.0f);
            }
        }
        
    }

    void Diray()
    {
        _No.enabled = false;
    }
}
