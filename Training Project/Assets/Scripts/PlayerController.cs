using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private Rigidbody2D _rigid_body;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigid_body = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(3, -2);
        //_rigid_body.velocity = Vector2.right * .5f;
        Invoke(nameof(AcceptDefeat), 12);

=======
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            Debug.Log("Fire activated!");
        }
    }

    private void FixedUpdate()
    {
        var dir = _input.actions["Move"].ReadValue<Vector2>();
        _rigid_body.velocity = dir * 5;
    }

    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    
}
=======
        
    }
}
>>>>>>> Stashed changes
