using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// �}�E�X�N���b�N�ł̃I�u�W�F�N�g�̎擾
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
        //�N���b�N�����I�u�W�F�N�g���擾���A���O��\������
        if (gameObject.tag == "Takarabako") 
        {
            SceneManager.LoadScene("FailScene");
            Debug.Log("�Ă΂ꂽ");
        }

        _searchManager.BlockSearch((int)eventData.position.x, (int)eventData.position.y);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
