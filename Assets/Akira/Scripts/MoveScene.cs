using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移処理
/// </summary>
public class MoveScene : MonoBehaviour
{
    public float _moveTime = 1f;

    //ボタン(UI)をクリックした時にシーン遷移(関数名は任意)
    public void OnClickButton()
    {
        //次の処理を _moveTime 秒遅らせる命令
        Invoke("Main", _moveTime);
    }

    public void Main()
    {
        SceneManager.LoadScene("Test");
    }
}
