using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;

    private Rigidbody2D _rigidbody;
    private WaypointPath _waypointPath;
    private Vector2 _patrolTargetPosition;
    private Vector2 _direction = Vector2.right;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();
    }

    private IEnumerator Start()
    {
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();
        }
        else
        {
            StartCoroutine(PatrolCoroutine());
        }
        yield break;
    }

    private void FixedUpdate()
    {
        if (!_waypointPath) return;
        var dir = _patrolTargetPosition - (Vector2)transform.position;
        if (dir.magnitude <= 0.1)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            dir = _patrolTargetPosition - (Vector2)transform.position;
        }
        if (GameManager.Instance.State == GameState.Playing)
        {
            _rigidbody.velocity = dir.normalized * patrolSpeed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
    public void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    //IEnumerator return type for coroutine
    //that can yield for time and come back
    IEnumerator PatrolCoroutine()
    {
        //change the direction every second
        while (true)
        {
            _direction = new Vector2(1, -1);
            yield return new WaitForSeconds(1);
            _direction = new Vector2(-1, 1);
            yield return new WaitForSeconds(1);
        }
    }
    private void OnEnable()
    {
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }
    private void OnDisable()
    {
        GameManager.OnAfterStateChanged -= HandleGameStateChange;
    }
    private void HandleGameStateChange(GameState state)
    {
        if (state == GameState.Starting)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }

        if (state == GameState.Playing)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }
    private void OnDrawGizmos()
    {
        var transforms = GetComponentsInChildren<Transform>(true);
        if (transforms.Length >= 2)
        {
            for (int i = 0, j = 1; j < transforms.Length; i++, j++)// i++; j++) {
            { 
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(transforms[i].position, transforms[j].position);
            }

            Gizmos.DrawLine(transforms[transforms.Length - 1].position, transforms[0].position);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<HealthSystem>()?.Damage(3);
            Vector2 awayDirection = (Vector2)(other.transform.position - transform.position);
            other.transform.GetComponent<PlayerController>()?.Recoil(awayDirection * 3f);
        }
    }

}