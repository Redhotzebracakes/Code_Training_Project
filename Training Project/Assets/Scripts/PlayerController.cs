using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigid_body;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(3, -2);
        _rigid_body = GetComponent<Rigidbody2D>();
        _rigid_body.velocity = Vector2.right * .5f;
        Invoke(nameof(AcceptDefeat), 12);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    
}