using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // reference player
    private Vector3 offset; // store offset distance between camera/ player

    // Start is called before the first frame update
    void Start()
    {
        // offset = transform.position - player.transform.position; // calculate & sotre the offset value by getting distance between players    
        offset = new Vector3(0, 1, -5);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;//set position of camera's transform to be same as player's , but offset claculated offset distance
    }

}
