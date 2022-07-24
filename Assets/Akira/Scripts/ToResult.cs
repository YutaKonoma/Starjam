using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ToResult : MonoBehaviour, IPointerClickHandler
{
    public int _mouseCount = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        _mouseCount++;

        if (_mouseCount >= 5)
        {
            _mouseCount = 0;
            SceneManager.LoadScene("FailScene");
        }
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
