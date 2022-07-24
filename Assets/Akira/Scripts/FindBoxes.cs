using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log($"x��{(int)eventData.position.x}����{(int)eventData.position.y}���N���b�N����");

        _searchManager.BlockSearch((int)eventData.position.x, (int)eventData.position.y);

        //var _selectBlocklockPos = eventData.pointerCurrentRaycast.gameObject.transform.position;

        //Debug.Log("�I�񂾃u���b�N�� " + _selectBlocklockPos + " �ɂ���܂�");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
