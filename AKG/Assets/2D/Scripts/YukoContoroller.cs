using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukoContoroller : MonoBehaviour {

    Rigidbody2D _rd;             
    Animator _animator;          

    [SerializeField]float _jumpForce;       
    [SerializeField] float _jumpThreshold;    
    [SerializeField] float _runForce;       
    [SerializeField] float _runSpeed;       
    [SerializeField] float _runThreshold;   
    bool _isGround = true;        
    int _key = 0;

    string _state;                
    string _prevState;            
    float _stateEffect = 1;       

    void Start () {
        _rd = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
	}

	void Update () {
        GetInputKey();          
        ChangeState();          
        ChangeAnimation();      
        Move();                 
    }

    void GetInputKey()
    {
        _key = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _key = -5;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _key = 5;
        }

    }

    void ChangeState()
    {
        if(Mathf.Abs(_rd.velocity.y) > _jumpThreshold)
        {
            _isGround = false;
        }

        if (_isGround)
        {
            if(_key != 0)
            {
                _state = "RUN";
            }
            else
            {
                _state = "IDLE";
            }
        }
        else
        {
            if(_rd.velocity.y > 0)
            {
                _state = "JUMP";
            }
            else if(_rd.velocity.y < 0)
            {
                _state = "FALL";
            }
        }
    }

    void ChangeAnimation()
    {
        if(_prevState != _state)
        {
            switch (_state)
            {

                case "RUN":
                    _animator.SetBool("IsJump", false);
                    _animator.SetBool("IsFall", false);
                    _animator.SetBool("IsRun", true);
                    _animator.SetBool("IsIdle", false);
                    _stateEffect = 1f;
                    transform.localScale = new Vector3(_key, 5, 5);
                    break;

                case "JUMP":
                    _animator.SetBool("IsJump", true);
                    _animator.SetBool("IsFall", false);
                    _animator.SetBool("IsRun", false);
                    _animator.SetBool("IsIdle", false);
                    _stateEffect = 0.5f;
                    break;

                case "FALL":
                    _animator.SetBool("IsJump", false);
                    _animator.SetBool("IsFall", true);
                    _animator.SetBool("IsRun", false);
                    _animator.SetBool("IsIdle", false);
                    _stateEffect = 0.5f;
                    break;

                default:
                    _animator.SetBool("IsIdle", true);
                    _animator.SetBool("IsFall", false);
                    _animator.SetBool("IsRun", false);
                    _animator.SetBool("IsJump", false);
                    _stateEffect = 1f;
                    break;
            }

            _prevState = null;
        }
    }

    void Move()
    {
        if (_isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._rd.AddForce(transform.up * this._jumpForce);
                _isGround = false;
            }
        }

        float _MSpeed = Mathf.Abs(_rd.velocity.x);
        if(_MSpeed < _runThreshold)
        {
            _rd.AddForce(transform.right * _key * _runForce * _stateEffect);
        }
        else
        {
            transform.position += new Vector3(_runSpeed * _key * _stateEffect * Time.deltaTime, 0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D _collider2D)
    {
        if(_collider2D.gameObject.tag == "Ground")
        {
            if (!_isGround)
            {
                _isGround = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D _collider2D)
    {
     if(_collider2D.gameObject.tag == "Ground")
        {
            if (!_isGround)
            {
                _isGround = true;
            }
        }   
    }
}
