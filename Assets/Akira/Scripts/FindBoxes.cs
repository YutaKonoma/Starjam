using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// マウスクリックでのオブジェクトの取得
/// </summary>
public class FindBoxes : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //クリックしたオブジェクトを取得し、名前を表示する
        Debug.Log(name + " をクリックした");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
