using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڏ���
/// </summary>
public class MoveScene : MonoBehaviour
{
    //�{�^��(UI)���N���b�N�������ɃV�[���J��(�֐����͔C��)
    public void OnClickButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
        print($"Move to {scenename}");
    }
}
