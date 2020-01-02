using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    //To avoid script imports I used some delegates, so it won't crash if the other scripts doesn't exist
    #region Delegates

    public static Action<Transform> onTreeSpawn;
    public static Action<bool> updateButtonState;
    public static Action initRound;
    public static Action<int, int> onNextLevel;
    public static Action updateHudIndicator;
    public static Action onDestroyTrunk;

    #endregion
    
    #region Serialized variables

    [Range(1, 10)] [SerializeField] 
    private int starterLevel;
    [Range(1, 3)] [SerializeField] 
    private int baseNumberOfTrees;
    [Range(2, 20)] [SerializeField]
    private int treeHeightMax;
    [SerializeField] private Vector3 spaceBetweenTrees;
    [SerializeField] private Transform treesParent;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private RoundAnimation animationController;

    #endregion

    #region Private variables

    private int currentTreeIndex;
    private int currentLevel;
    private int treesAmount;
    private Queue<TreeController> _trees;

    #endregion

    #region Lifecycle methods

    private void Start()
    {
        currentLevel = starterLevel;
        _trees = new Queue<TreeController>();
        // The number of trees in each round is randomized between the base value and the base value multiplied by the current level
        treesAmount = Random.Range(baseNumberOfTrees, baseNumberOfTrees * currentLevel);
        initTree();
        onNextLevel?.Invoke(currentLevel, treesAmount);
    }

    #endregion
    
    private void initTree()
    {
        updateButtonState?.Invoke(false);
        GameObject go = Instantiate(treePrefab, treesParent);
        TreeController tree = go.GetComponent<TreeController>();
        // The tree height will be a random value between 1 and the max height
        tree.init(spaceBetweenTrees * currentTreeIndex, Random.Range(1, treeHeightMax));
        tree.onDestroy += translateToNextTree;
        // To move the camera only after the other tree completely spawn
        tree.onAnimationEnd += translateCamera;
        _trees.Enqueue(tree);
        // Increase the level progression
        currentTreeIndex++;
    }
    
    // If all trees had being destroyed, the scenario is reset while a transition animation occurs
    // The reset upgrade the level and reset progression level variables
    private void manageRound()
    {
        updateHudIndicator?.Invoke();
        if (currentTreeIndex >= treesAmount)
        {
            animationController.startAnimation();
            currentTreeIndex = 0;
            currentLevel++;
            treesAmount = Random.Range(baseNumberOfTrees, baseNumberOfTrees * currentLevel);
            initRound?.Invoke();
            onNextLevel?.Invoke(currentLevel, treesAmount);
        }
    }

    #region Deletages response methods

    private void translateCamera(Transform target)
    {
        onTreeSpawn?.Invoke(target);
        updateButtonState?.Invoke(true);
    }
        
    public void translateToNextTree()
    {
        while (_trees.Count > 0)
        {
            Destroy(_trees.Dequeue().gameObject);
        }
        manageRound();
        initTree();
    }

    #endregion

    #region UI methods
    
    public void onHitClick()
    {
        onDestroyTrunk?.Invoke();
    }

    public void onSpawnClick()
    {
        translateToNextTree();
    }

    public void quit()
    {
        Application.Quit();
    }

    #endregion
    
}
