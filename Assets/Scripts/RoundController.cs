using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    #region Delegates

    public static Action<Transform> onTreeSpawn;
    public static Action<bool> updateButtonState;
    public static Action initRound;
    public static Action<int, int> onNextLevel;
    public static Action updateHudIndicator;

    #endregion
    
    #region Serialized variables

    [Range(1, 10)] [SerializeField] 
    private int starterLevel;
    [Range(1, 3)] [SerializeField] 
    private int baseNumberOFTrees;
    [Range(2, 20)] [SerializeField]
    private int treeHeightMax;
    [SerializeField] private Vector3 spaceBetweenTrees;
    [SerializeField] private Transform treesParent;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private RoundAnimation animation;

    #endregion

    #region Private variables

    private int currentTreeIndex;
    private int currentLevel;
    private int treesAmount;
    private Queue<GameObject> trees;

    #endregion

    #region Lifecycle methods

    private void Start()
    {
        currentLevel = starterLevel;
        treesAmount = Random.Range(baseNumberOFTrees, baseNumberOFTrees * currentLevel);
        trees = new Queue<GameObject>();
        initTree();
        onNextLevel?.Invoke(currentLevel, treesAmount);
    }

    #endregion
    
    public void translateToNextTree()
    {
        manageRound();
        Destroy(trees.Dequeue());
        initTree();
    }
    
    private void initTree()
    {
        updateButtonState?.Invoke(false);
        GameObject go = Instantiate(treePrefab, treesParent);
        TreeController tree = go.GetComponent<TreeController>();
        tree.init(spaceBetweenTrees * currentTreeIndex, Random.Range(1, treeHeightMax));
        tree.onDestroy += translateToNextTree;
        tree.onAnimationEnd += translateCamera;
        trees.Enqueue(go);
        currentTreeIndex++;
    }

    #region UI methods

    public void onHitClick()
    {
        trees.Peek().GetComponent<TreeController>().dequeueTrunk();
    }

    public void onSpawnClick()
    {
        translateToNextTree();
    }

    #endregion
    
    private void translateCamera(Transform target)
    {
        onTreeSpawn?.Invoke(target);
        updateButtonState?.Invoke(true);
    }

    private void manageRound()
    {
        updateHudIndicator?.Invoke();
        if (currentTreeIndex >= treesAmount)
        {
            StartCoroutine(animation.roundTransition());
            currentTreeIndex = 0;
            currentLevel++;
            treesAmount = Random.Range(baseNumberOFTrees, baseNumberOFTrees * currentLevel);
            initRound?.Invoke();
            onNextLevel?.Invoke(currentLevel, treesAmount);
        }
    }
}
