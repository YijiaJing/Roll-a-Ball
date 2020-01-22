using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text stateText;
    public Text tipText;

    private Rigidbody rb;
    private int count;
    private int health;
    private bool active;

    void Start()
	{
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        
        count = 0;
        health = 3;
        SetCountText();
        winText.text = "";
        stateText.text = "";
        active = true;
    }

    void FixedUpdate()
	{
        // restart
        if (Input.GetKeyDown(KeyCode.R))
		{
            Application.LoadLevel(0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
		{
            Application.Quit();
		}

        // when falling off the map
        if (transform.position.y <= -10)
		{
            Death();
		}

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Slower"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine("SpeedDown");
        }
        else if (other.gameObject.CompareTag("Faster"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine("SpeedUp");
        }
        else if (other.gameObject.CompareTag("Invisible"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine("SetInvisible");
        }
    }

    void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Bullet") && active)
		{
            health -= 1;
            SetCountText();
		}

    }

    IEnumerator SpeedUp()
	{
        speed += 10;
        stateText.text = "Speed UP!";
        yield return new WaitForSeconds(6);
        speed -= 10;
        stateText.text = "";
    }

    IEnumerator SpeedDown()
    {
        speed -= 7;
        stateText.text = "Speed DOWN!";
        yield return new WaitForSeconds(6);
        speed += 7;
        stateText.text = "";
    }

    IEnumerator SetInvisible()
	{
        GetComponent<Renderer>().enabled = false;
        stateText.text = "Invisible!";
        yield return new WaitForSeconds(8);
        GetComponent<Renderer>().enabled = true;
        stateText.text = "";
    }

    void SetCountText()
	{
        countText.text = "Coins Collected: " + count.ToString() + "/14" + "\nHealth: " + health.ToString() + "/3";

        if (count >= 14)
		{
            winText.text = "You Win!";
            tipText.text = "Press Q to quit\nPress R to restart";
            active = false;
		}

        if (health <= 0)
		{
            Death();
        }
    }

    void Death()
	{
        winText.text = "You Died!";
        rb.constraints = RigidbodyConstraints.FreezeAll;
        tipText.text = "Press Q to quit\nPress R to restart";
        active = false;
    }
}
