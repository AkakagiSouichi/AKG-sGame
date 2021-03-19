using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    int i,_count;
    float a, c;
    public GameObject[] gameObjects;
    private GameObject Yuko1;
    [SerializeField] Text _clear,_gameOver;
    [SerializeField] Button _back;
    [SerializeField] GameObject Yuko;
    [SerializeField] GameObject Goal;

    void Start () {
        Instantiate(gameObjects[0], new Vector3(0, 0, 0), Quaternion.identity);
        Yuko1 = Instantiate(Yuko, new Vector3(-5f, 0, -5f), Quaternion.identity);
        Yuko1.name = Yuko.name;

        
        a = 25;
        c = 0;
        for (i = 0; i <= 15 ; i++)
        {
            c += a;
            Instantiate(gameObjects[Random.Range(1,gameObjects.Length)], new Vector3(c, 0, 0), Quaternion.identity);
        }
        Instantiate(Goal, new Vector3(c + 25, 0, 0), Quaternion.identity);
    }
	
	void Update () {
        _count = GoalScript.getCount();
		if(Yuko1.transform.position.y < -8.0f)
        {
            _count = 2;
        }

        switch (_count)
        {

            case 1:
                _back.gameObject.SetActive(true);
                _clear.GetComponent<Text>().enabled = true;
                _gameOver.GetComponent<Text>().enabled = false;
                GetComponent<timerScript>().enabled = false;
                break;

            case 2:
                Yuko1.gameObject.SetActive(false);
                _back.gameObject.SetActive(true);
                _gameOver.GetComponent<Text>().enabled = true;
                GetComponent<timerScript>().enabled = false;
                break;

            default:
                _back.gameObject.SetActive(false);
                _clear.GetComponent<Text>().enabled = false;
                _gameOver.GetComponent<Text>().enabled = false;
                GetComponent<timerScript>().enabled = true;
                break;
        }
	}

    
}
