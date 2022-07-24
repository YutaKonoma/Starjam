using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeScore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ScoreText;

    [SerializeField] SearchManager _searchManager;
    
    int Count;

    public void Start()
    {
        _searchManager = GameObject.FindObjectOfType<SearchManager>();
    }
    public  void ScoreCount(int score = 2)
    {

        score *= _searchManager.BlockCount;

        Debug.Log("Score:0" + score * _searchManager.BlockCount) ;
    }
    

}
