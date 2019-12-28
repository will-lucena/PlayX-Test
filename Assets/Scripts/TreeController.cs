using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public static Action<Vector3> onStartAnimate;
    
    [SerializeField] private GameObject trunkPrefab;
    [SerializeField] private float trunkOffset;

    private Stack<GameObject> trunks;
    private TreeAnimation _animation;

    private void OnEnable()
    {
        TreeAnimation.onEndAnimation += enableRigidBodies;
    }

    private void OnDisable()
    {
        TreeAnimation.onEndAnimation -= enableRigidBodies;
    }

    private void Start()
    {
        _animation = GetComponent<TreeAnimation>();
    }

    public void initTree(int height)
    {
        trunks = new Stack<GameObject>();
        for (int i = 0; i < height; i++)
        {
            GameObject trunk = Instantiate(trunkPrefab, new Vector3(0, i * trunkOffset, 0), Quaternion.Euler(90, 0, 0), transform);
            if (trunks.Count > 0)
            {
                trunk.GetComponent<Trunk>().linkToTrunkBelow(trunks.Peek()); 
            }
            trunks.Push(trunk);
        }
        callAnimation(height);
    }

    private void callAnimation(int height)
    {
        onStartAnimate?.Invoke(new Vector3(0, trunkOffset * height, 0));
    }

    private void enableRigidBodies()
    {
        while (trunks.Count > 0)
        {
            trunks.Pop().GetComponent<Rigidbody>().isKinematic = false;   
        }
    }
}
