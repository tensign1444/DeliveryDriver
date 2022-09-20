using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Delivery : MonoBehaviour
{

    [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);

    [SerializeField] GameObject package;
    [SerializeField] GameObject delivery;

    public GameObject quad;
    [SerializeField] float spawnTime = 1.0f;

    private bool hasPackage;
    [SerializeField] float destroyDelay = 0.05f;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      //  Debug.Log("Hit me");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Package") && !hasPackage)
        {
            Destroy(collision.gameObject, destroyDelay);
            Debug.Log("Picked up package");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Spawn(delivery);
        }          
        else if (collision.tag.Equals("Customer") && hasPackage)
        {
            Destroy(collision.gameObject, destroyDelay);

            Debug.Log("Delievered Package");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
            Spawn(package);
        }            
        else
        {
            Debug.Log("Nothing");
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
