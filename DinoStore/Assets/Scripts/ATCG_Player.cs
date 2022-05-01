using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATCG_Player : MonoBehaviour
{

    public GameObject[] projectiles;
    public Rigidbody2D rb;
    public float force;
    private bool onGround;
    public AudioClip jumpSound;
    public AudioSource audioSource;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(projectiles[0], transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(projectiles[1], transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(projectiles[2], transform.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(projectiles[3], transform.position, Quaternion.identity);
        }

        if (transform.position.y <= 0)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            audioSource.PlayOneShot(jumpSound);
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
    }
}
