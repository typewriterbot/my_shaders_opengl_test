using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject destroyEffect; // animation when bullet collides 
    public int damage; // for player to hit enemies
    public float distance;
    public LayerMask whatIsSolid; // collision detection then particle EXPLODE
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime); // erase
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, 
            whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy")) // fight enemy!
            {
                Debug.Log("ENEMY MUST TAKE DAMAGE!");
                hitInfo.collider.GetComponent<Enemy>().TakeHit(damage); // enemy must take damage
            }
            DestroyProjectile();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        //Destroy(gameObject);
    }
    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
