using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �}�E�X�N���b�N�ł̃I�u�W�F�N�g�̎擾
/// </summary>
public class FindBoxes : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //�N���b�N�����I�u�W�F�N�g���擾���A���O��\������
        Debug.Log(name + " ���N���b�N����");
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
