using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڏ���
/// </summary>
public class ToGameScene : MonoBehaviour
{
    public float _moveTime = 1f;

    //�{�^��(UI)���N���b�N�������ɃV�[���J��(�֐����͔C��)
    public void OnClickButton()
    {
        //���̏����� _moveTime �b�x�点�閽��
        Invoke("Load", _moveTime);
    }

    public void Load()
    {
        SceneManager.LoadScene("Test");
    }
}
