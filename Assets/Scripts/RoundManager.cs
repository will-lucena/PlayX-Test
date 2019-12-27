using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private GameObject trunkPrefab;
    [SerializeField] private float trunkOffset;
    [SerializeField] private int amountOfTrunks;
    [SerializeField] private TrunkAnimation treeParent;
    
    // Start is called before the first frame update
    private void Start()
    {
        //createTree(amountOfTrunks);
    }

    public void createTree(int height)
    {
        for (int i = 0; i < height; i++)
        {
            Instantiate(trunkPrefab, new Vector3(0, i * trunkOffset, 0), Quaternion.Euler(90, 0, 0), treeParent.transform);
        }

        treeParent.updateInitialPosition(new Vector3(0, -trunkOffset * amountOfTrunks, 0));
        treeParent.startAnimation();
    }
}
