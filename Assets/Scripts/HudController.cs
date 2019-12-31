using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] private Button destroyTrunk;
    [SerializeField] private Button destroyTree;
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private Transform treesIndicator;
    [SerializeField] private string levelString;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private Color stepCompleted;

    private Queue<Image> uncompletedIndicators;
    private Queue<Image> completedIndicators;
    
    private void OnEnable()
    {
        RoundController.updateButtonState += updateButtonState;
        RoundController.onNextLevel += updateLevel;
        RoundController.updateHudIndicator += dequeueImage;
    }

    private void OnDisable()
    {
        RoundController.updateButtonState -= updateButtonState;
        RoundController.onNextLevel -= updateLevel;
        RoundController.updateHudIndicator -= dequeueImage;
    }

    private void Start()
    {
        uncompletedIndicators = new Queue<Image>();
        completedIndicators = new Queue<Image>();
    }

    private void updateButtonState(bool state)
    {
        destroyTrunk.interactable = state;
        destroyTree.interactable = state;
    }

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

    private void dequeueImage()
    {
        Image indicator = uncompletedIndicators.Dequeue();
        completedIndicators.Enqueue(indicator);
        indicator.color = stepCompleted;
    }
}
