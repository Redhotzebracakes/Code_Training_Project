using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input; //field to reference Player Input component
    private Rigidbody2D _rigidbody;
    public GameObject ballPrefab;
    private Vector2 _facingVector = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        //You can switch Action Maps using _input.SwitchCurrentActionMap("UI");


        _rigidbody = GetComponent<Rigidbody2D>();


        //Invoke(nameof(AcceptDefeat), 10);
    }

    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            //Debug.Log("Fire activated!");
            var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<BallController>()?.SetDirection(_facingVector);
        }
    }

    private void FixedUpdate()
    {

        var dir = _input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * 5;
        if (dir.magnitude > 0.5)
        {
            _facingVector = _rigidbody.velocity;
        }
    }
}