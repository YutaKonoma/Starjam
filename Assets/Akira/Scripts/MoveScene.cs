using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���̈ړ�
/// </summary>
public class MoveScene : MonoBehaviour
{
    //�{�^��(UI)���N���b�N�������ɃV�[���J��(�֐����͔C��)
    public void OnClickButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
