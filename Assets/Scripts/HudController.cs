using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    #region Serialized variables
    
    [SerializeField] private Button destroyTrunk;
    [SerializeField] private Button destroyTree;
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private Transform treesIndicator;
    [SerializeField] private string levelString;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Color stepCompleted;

    #endregion

    #region Private variables

    private Queue<Image> uncompletedIndicators;
    private Queue<Image> completedIndicators;

    #endregion
    
    #region Lifecycle methods

    private void OnEnable()
    {
        RoundController.updateButtonState += updateButtonState;
        RoundController.onNextLevel += updateLevel;
        RoundController.updateHudIndicator += updateIndicator;
    }

    private void OnDisable()
    {
        RoundController.updateButtonState -= updateButtonState;
        RoundController.onNextLevel -= updateLevel;
        RoundController.updateHudIndicator -= updateIndicator;
    }

    private void Start()
    {
        uncompletedIndicators = new Queue<Image>();
        completedIndicators = new Queue<Image>();
    }

    #endregion

    #region Delegates response methods
    
    private void updateButtonState(bool state)
    {
        destroyTrunk.interactable = state;
        destroyTree.interactable = state;
    }

    //When level ends, reset the level label and the indicators
    private void updateLevel(int level, int treesAmount)
    {
        while (completedIndicators.Count > 0)
        {
            Destroy(completedIndicators.Dequeue().gameObject);
        }
        
        levelLabel.text = levelString + level;
        for (int i = 0; i < treesAmount; i++)
        {
            Image image = Instantiate(indicatorPrefab, treesIndicator).GetComponent<Image>();
            uncompletedIndicators.Enqueue(image);
        }
    }
    
    private void updateIndicator()
    {
        Image indicator = uncompletedIndicators.Dequeue();
        completedIndicators.Enqueue(indicator);
        indicator.color = stepCompleted;
    }
    
    #endregion
}
