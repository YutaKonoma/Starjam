using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleScene : MonoBehaviour
{
    public void OnClickButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
