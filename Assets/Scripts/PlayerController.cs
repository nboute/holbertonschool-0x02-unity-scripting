using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Threading.Tasks;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;
    public int health = 5;
    private bool canTeleport = true;
 
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            Debug.Log($"Score: {score}");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            Debug.Log($"Health: {health}");
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
        else if (other.gameObject.CompareTag("Teleporter") && canTeleport)
        {
            var teleporter = GameObject.FindGameObjectsWithTag("Teleporter").First(obj => obj != other.gameObject);
            transform.position = teleporter.transform.position;
            canTeleport = false;
            Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => canTeleport = true);
        }
    }

    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
