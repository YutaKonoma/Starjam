using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    SearchManager _searchManager;

    void Start()
    {
        _searchManager = GameObject.Find("SearchManager").GetComponent<SearchManager>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                //Debug.Log($"x‚Í{(int)hit2d.transform.gameObject.transform.position.x}‚™‚Í{ (int)hit2d.transform.gameObject.transform.position.y}‚ðƒNƒŠƒbƒN‚µ‚½");
                _searchManager.BlockSearch((int)hit2d.transform.gameObject.transform.position.y * -1, (int)hit2d.transform.gameObject.transform.position.x);
            }


        }
    }
}
