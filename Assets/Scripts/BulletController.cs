using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject player;
    public float xDir, yDir, zDir;

    private Rigidbody rb;
    private float speed;
    private bool on;
    private Vector3 initialPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        speed = 7.5f;
        initialPos = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 6)
		{
            on = true;
        }

        if (on) {
            rb.freezeRotation = false;
            rb.velocity = new Vector3(xDir, yDir, zDir) * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
		{
            transform.position = initialPos;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            on = false;
		}
	} 
}
