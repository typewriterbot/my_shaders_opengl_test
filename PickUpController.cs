using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //DotTween animation asset package
public class PickUpController : MonoBehaviour
{
    public GameObject objectToDisable; // weapon going to enable after pickup
    // animate the gun to emphasize it needs to be picked up.
    public Transform cubeTransform;
    public float animDuration;
    public Ease animEase; 
     // Start is called before the first frame update
    void Start()
    {
        //animate up
        cubeTransform
             .DOMoveY(3f, animDuration)
             .SetEase(animEase)
             .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) // Player picks up weapon
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objectToDisable.SetActive(true); // turn on weapon
            Destroy(gameObject); // gone
        }
        
    }
}
