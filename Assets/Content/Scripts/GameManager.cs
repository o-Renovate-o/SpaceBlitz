using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject targets;
    [SerializeField] UnityEvent allTargetsShot;
    private int targetCount = 0;

    #region Singleton
    static GameManager instance;

    public static GameManager Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        targetCount = targets.transform.childCount;
    }
    #endregion
    
    public void TargetShot()
    {
        targetCount--;
        if (targetCount == 0)
            allTargetsShot.Invoke();
    }

    private static float score;
    public float Score { get { return score; } }

    public void PlayerScored(float targetValue)
    {
        score = score + targetValue;

        //Debug.Log(score);
    }

    public void GameStart()
    {
        //Debug.Log("<color>=greem>Game Started</color>");
    }
}
