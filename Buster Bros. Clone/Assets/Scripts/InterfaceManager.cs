using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;
    [SerializeField] private Animator _anim;
    private static readonly int Play = Animator.StringToHash("Play");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void LevelClearAnim()
    {
        _anim.SetTrigger(Play);
    }
}
