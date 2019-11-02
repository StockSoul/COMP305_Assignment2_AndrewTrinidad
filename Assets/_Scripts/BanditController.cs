using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;


public class BanditController : MonoBehaviour
{
    public BanditAnimState banditAnimState;

    public Animator banditAnimator;

    public SpriteRenderer banditSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        banditAnimState = BanditAnimState.WALK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
