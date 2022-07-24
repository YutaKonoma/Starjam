using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの移動
/// </summary>
public class MoveScene : MonoBehaviour
{
    //ボタン(UI)をクリックした時にシーン遷移(関数名は任意)
    public void OnClickButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
