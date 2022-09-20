using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float destroyDelay = 0.05f;


    [SerializeField] GameObject boost;
    public GameObject quad;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, moveAmount, 0);
        transform.Rotate(0, 0, -steerAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boost"))
        {
            moveSpeed = boostSpeed;
            Destroy(collision.gameObject, destroyDelay);
            Spawn(boost);
        }
        else if (collision.tag.Equals("Enviroment"))
        {
            moveSpeed = slowSpeed;
        }
    }


    private void Spawn(GameObject objectToSpawn)
    {
        MeshCollider c = quad.GetComponent<MeshCollider>();
        float screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
        float screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
        Vector2 pos = new Vector2(screenX, screenY);

        Instantiate(objectToSpawn, pos, objectToSpawn.transform.rotation);

    }
}
