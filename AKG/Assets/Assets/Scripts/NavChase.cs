using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavChase : MonoBehaviour
{

    public Transform target;

    Rigidbody rb;
    Animator anim;
    NavMeshAgent nav;

    public float visibleDistance; 
    float targetDistance;         

    public float sightAngle;      

    Transform lineOfSight1;      
    Ray gazeRay1;                   

    public LayerMask visibleLayer; 

    public float walkSpeed;   
    public float runSpeed;         

    public float targetLostLimitTime; 
    public float targetFindDistance; 
    float _lostTime = 0f;

    public float idleMaxTime;  
    float _idleTime = 0f;

    public float wanderMaxTime;   
    float _wanderTime = 0f;

    enum eState                   
    {
        Idle,     
        Wander,     
        Chase,     
        Attack,   
    }
    eState _state = eState.Idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        lineOfSight1 = GameObject.Find("LineOfSight1").transform;
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case eState.Idle:
                Idle();
                break;

            case eState.Wander:
                Wander();
                break;

            case eState.Chase:
                Chase();
                break;
        }
    }

    public void Idle()
    {
        Search(_state);

        _idleTime += Time.deltaTime;
        if (_idleTime > idleMaxTime)                      
        {

            anim.SetTrigger("Walk");
            nav.Resume();
            nav.SetDestination(new Vector3(Random.Range(-14f, 14f), 0f, Random.Range(-14f, 14f))); 
            _state = eState.Wander;
            nav.speed = walkSpeed;
            _idleTime = 0f;
        }
    }


    public void Wander()
    {
        Search(_state);

        _wanderTime += Time.deltaTime;
        if (_wanderTime > wanderMaxTime || nav.remainingDistance < 0.5f)
        {

            anim.SetTrigger("Idle");
            nav.Stop();
            _state = eState.Idle;
            _wanderTime = 0f;
            return;
        }
    }

    void Search(eState state)
    {
        float _angle = Vector3.Angle(target.position - transform.position, lineOfSight1.forward);

        if (_angle <= sightAngle)
        {


            gazeRay1.origin = lineOfSight1.position;
            gazeRay1.direction = target.position - lineOfSight1.position;
            RaycastHit hit;

            if (Physics.Raycast(gazeRay1, out hit, visibleDistance, visibleLayer))
            {
                Debug.DrawRay(gazeRay1.origin, gazeRay1.direction * hit.distance, Color.red);

                if (hit.collider.gameObject.tag != "Obstacle")  
                {
                    if (state == eState.Idle || state == eState.Wander)
                    {
                        TargetFound();
                    }
                    else if (state == eState.Chase)
                    {
                        TargetInSight();
                    }
                    return;
                }
            }

            Debug.DrawRay(gazeRay1.origin, gazeRay1.direction * visibleDistance, Color.gray);
        }

        targetDistance = (transform.position - target.position).magnitude;

        if (targetDistance < targetFindDistance)         
        {
            if (state == eState.Idle || state == eState.Wander)
            {
                TargetFound();
            }
            else if (state == eState.Chase)
            {
                TargetInSight();
            }
            return;
        }
    }

    public void TargetFound()
    {

        anim.SetTrigger("Run");
        nav.Resume();
        nav.SetDestination(target.position);
        _state = eState.Chase;
        nav.speed = runSpeed;
        _idleTime = 0f;
        _wanderTime = 0f;
    }

    void Chase()                            
    {
        nav.SetDestination(target.position);
        Search(_state);

        _lostTime += Time.deltaTime;
        // Debug.Log ("LostTime: " + _lostTime);

        if (_lostTime > targetLostLimitTime)             
        {
            // ターゲットロスト
            _state = eState.Idle;
            nav.Stop();
            anim.SetTrigger("Idle");
            nav.speed = 0f;
            _lostTime = 0f;
        }
    }

    void TargetInSight()
    {
        _lostTime = 0f;
    }

}

    



