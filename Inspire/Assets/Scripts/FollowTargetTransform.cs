using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetTransform : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    public static FollowTargetTransform singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            this.transform.position = targetTransform.position;
        }
    }

    public void SetTarget(Transform targetToSet)
    {
        targetTransform = targetToSet;
    }
}
