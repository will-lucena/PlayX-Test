using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    /*
    [SerializeField] private GameObject trunkPrefab;
    [SerializeField] private float trunkOffset;
    [SerializeField] private int amountOfTrunks;
    [SerializeField] private TrunkAnimation treeParent;
    [SerializeField] private ParticleSystem particles;

    public static Action callParticles;
    
    // Start is called before the first frame update
    private void Start()
    {
        //createTree(amountOfTrunks);
        callParticles += enableParticles;
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
    
    public void enableParticles()
    {
        if (particles.isEmitting)
        {
            particles.Stop();
        }
        particles.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            callParticles();
        }
    }
    
    /**/
    public static Action<Transform> onTreeDestroy;

    [Range(1, 10)] [SerializeField] 
    private int currentLevel;
    [Range(1, 3)] [SerializeField] 
    private int baseNumberOFTrees;
    [Range(2, 20)] [SerializeField]
    private int treeHeightRange;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Vector3 spaceBetweenTrees;
    [SerializeField] private Transform treesParent;
    [SerializeField] private GameObject treePrefab;

    private int currentTreeIndex;
    private Queue<GameObject> trees;

    private void Start()
    {
        trees = new Queue<GameObject>();
        initTree();
    }

    public void enableParticles()
    {
        if (particles.isEmitting)
        {
            particles.Stop();
        }
        particles.Play();
    }

    public void translateToNextTree()
    {
        Destroy(trees.Dequeue());
        initTree();
    }
    
    private void initTree()
    {
        GameObject go = Instantiate(treePrefab, treesParent);
        TreeController tree = go.GetComponent<TreeController>();
        tree.initTree(spaceBetweenTrees * currentTreeIndex, Random.Range(1, treeHeightRange));
        tree.onDestroy += translateToNextTree;
        tree.onAnimationEnd += translateCamera;
        trees.Enqueue(go);
        currentTreeIndex++;
    }

    public void onHitClick()
    {
        trees.Peek().GetComponent<TreeController>().dequeueTrunk();
    }

    public void onSpawnClick()
    {
        initTree();
    }

    private void translateCamera(Transform target)
    {
        onTreeDestroy?.Invoke(target);
    }
    
    
}
