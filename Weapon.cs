using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projectile; // shoots bullets
    public Transform shotPoint;
    private float timeShots;
    public float startTimeShots;
    
    bool isFacingRight = true;
   // for some reason JUMP is disabling my weapon!! find out why...
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeShots <= 0)
        {
            if (Input.GetMouseButtonDown(0)) // click?
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeShots = startTimeShots;
            }
          
        }
        else
        {
            timeShots -= Time.deltaTime; //dec remaing times so proctile can fade
        }
    
    }
   void fixedUpdate()
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z + offset);
      
        if (z <= 90 && isFacingRight)
        {
            Debug.Log("Facing right!");
            Flip();
            isFacingRight = false;
        }
        else
        {
            Debug.Log("Facing left!");
            Flip();
            isFacingRight = true;
        }

    }
    void Flip() // rotate gun by 180 degrees
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Start()  // Start is called before the first frame update
    {
        

    }

   
}
