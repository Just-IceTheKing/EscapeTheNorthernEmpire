using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    const float BASIC_SCALING_FACTOR = 10.0f;

    public float MoveSpeed = 1.0f;
    public float JumpSpeed = 10.0f;

    public Bullet ForceShot;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * BASIC_SCALING_FACTOR * MoveSpeed);
            transform.localScale = new Vector3(-1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * BASIC_SCALING_FACTOR * MoveSpeed);
            transform.localScale = new Vector3(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * BASIC_SCALING_FACTOR * JumpSpeed);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            var relativeTarget = target - transform.position;
            var angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x);
            var direction = Quaternion.AngleAxis(angle, Vector3.forward);
            var bullet = Instantiate(ForceShot, transform.position, direction);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}

