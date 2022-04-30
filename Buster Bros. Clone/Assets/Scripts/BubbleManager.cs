using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public struct BubblePoolAmount
{
    public int tier1Bubbles;
    public int tier2Bubbles;
    public int tier3Bubbles;
    public int tier4Bubbles;
    public int tier5Bubbles;

    public int TotalBubbles()
    {
        int total = tier1Bubbles + tier2Bubbles + tier3Bubbles + tier4Bubbles + tier5Bubbles;
        return total;
    }
}

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance { get; private set; }
    
    public ObjectPool<Bubble> tier1BubblePool;
    public ObjectPool<Bubble> tier2BubblePool;
    public ObjectPool<Bubble> tier3BubblePool;
    public ObjectPool<Bubble> tier4BubblePool;
    public ObjectPool<Bubble> tier5BubblePool;

    [SerializeField] private Bubble _tier1BubblePrefab;
    [SerializeField] private Bubble _tier2BubblePrefab;
    [SerializeField] private Bubble _tier3BubblePrefab;
    [SerializeField] private Bubble _tier4BubblePrefab;
    [SerializeField] private Bubble _tier5BubblePrefab;

    [SerializeField] private int _totalBubbles;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        var bubbleArray = GameObject.FindObjectsOfType(typeof(Bubble));
        BubblePoolAmount bubbleAmount = new BubblePoolAmount();
        foreach (Bubble bubble in bubbleArray)
        {
            switch (bubble.tier)
            {
                case 1:
                    bubbleAmount.tier1Bubbles += 1;
                    bubbleAmount.tier2Bubbles += 2;
                    bubbleAmount.tier3Bubbles += 4;
                    bubbleAmount.tier4Bubbles += 8;
                    bubbleAmount.tier5Bubbles += 16;
                    break;
                case 2: 
                    bubbleAmount.tier3Bubbles += 4;
                    bubbleAmount.tier4Bubbles += 8;
                    bubbleAmount.tier5Bubbles += 16;
                    break;
                case 3:
                    bubbleAmount.tier4Bubbles += 8;
                    bubbleAmount.tier5Bubbles += 16;
                    break;
                case 4:
                    bubbleAmount.tier5Bubbles += 16;
                    break;
                case 5: 
                    Debug.Log("Tiniest bubble doesn't spawn any other bubbles");
                    break;
            }
        }

        _totalBubbles = bubbleAmount.TotalBubbles();
        CreatePool(bubbleAmount);
    }

    public void DecreaseBubble()
    {
        _totalBubbles -= 1;
        if (_totalBubbles <= 0) Debug.Log("ALL BUBBLES POPPED!");
    }

    public void CreatePool(BubblePoolAmount bubblesToAdd)
    {
        Debug.Log(
            "Tier 1 " + bubblesToAdd.tier1Bubbles +  
                  " Tier 2 " + bubblesToAdd.tier2Bubbles +
                  " Tier3 " + bubblesToAdd.tier3Bubbles +
                  " Tier 4 " + bubblesToAdd.tier4Bubbles +
                  " Tier 5 " + bubblesToAdd.tier5Bubbles
            );
        
        tier2BubblePool = new ObjectPool<Bubble>(() =>
        {
            return Instantiate(_tier2BubblePrefab, transform, true);
        }, bubble =>
        {
            bubble.gameObject.SetActive(true);
        }, bubble =>
        {
            
        }, bubble =>
        {
                
        }, true, bubblesToAdd.tier2Bubbles);
        
        ///////////////////////////////////
        tier3BubblePool = new ObjectPool<Bubble>(() =>
        {
            return Instantiate(_tier3BubblePrefab, transform, true);
        }, bubble =>
        {
                    
        }, bubble =>
        {
                
        }, bubble =>
        {
                
        }, true, bubblesToAdd.tier3Bubbles);
        
        ///////////////////////////////////
        tier4BubblePool = new ObjectPool<Bubble>(() =>
        {
            return Instantiate(_tier4BubblePrefab, transform, true);
        }, bubble =>
        {
                    
        }, bubble =>
        {
                
        }, bubble =>
        {
                
        }, true, bubblesToAdd.tier4Bubbles);
        
        ////////////////////////////////////
        tier5BubblePool = new ObjectPool<Bubble>(() =>
        {
            return Instantiate(_tier5BubblePrefab, transform, true);
        }, bubble =>
        {
                    
        }, bubble =>
        {
                
        }, bubble =>
        {
                
        }, true, bubblesToAdd.tier5Bubbles);
        
    }
}
