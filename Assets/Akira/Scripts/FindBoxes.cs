using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// マウスクリックでのオブジェクトの取得
/// </summary>
public class FindBoxes : MonoBehaviour, IPointerClickHandler
{
     SearchManager _searchManager;

    void Start() 
    {
        _searchManager = GameObject.Find("SearchManager").GetComponent<SearchManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //クリックしたオブジェクトを取得し、名前を表示する
        Debug.Log($"xは{(int)eventData.position.x}ｙは{(int)eventData.position.y}をクリックした");

        _searchManager.BlockSearch((int)eventData.position.x, (int)eventData.position.y);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
