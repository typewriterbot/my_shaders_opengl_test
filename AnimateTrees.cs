using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // this script will animate trees and bouncy effect
public class AnimateTrees : MonoBehaviour
{
    public Transform catcusTransform;
    public float animDuration;
    public Ease animEase;
    // Start is called before the first frame update
   // var sequence = DOTween.Sqeuence(); 
    void Start()
    {
        // animates the tree
        catcusTransform
           //ShakeScale(float duration, float/Vector3 strength, int vibrato, float randomness, bool fadeOut)
           .DOShakeScale(1.2f, 0.5f, 2, 1f, false)
           .SetEase(Ease.InBounce)
           .SetLoops(-1, LoopType.Yoyo);
       /* catcusTransform
            .DORotate(Vector3 0.2f, 0.2f, LocalAxisAdd)
            .SetEase(animEase)
            .SetLoops(-1, LoopType.Yoyo);*/ 
}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I am alive!");
    }
}
