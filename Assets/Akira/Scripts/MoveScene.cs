using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移処理
/// </summary>
public class MoveScene : MonoBehaviour
{
    //ボタン(UI)をクリックした時にシーン遷移(関数名は任意)
    public void OnClickButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
        print($"Move to {scenename}");
    }
}
