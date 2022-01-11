using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    public Transform cloudTransform;
    public float animDuration;
    public Ease animEase;
    // Start is called before the first frame update
    void Start()
    {
        // animates the tree
        cloudTransform
           .DOMoveX(0.5f, 1f, false).SetEase(Ease.InBounce)
           .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
