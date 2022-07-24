using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    [Header("マップの高さ")]
    [SerializeField] int _height = 18;
    [Header("マップの横幅")]
    [SerializeField] int _width = 18;

    [Header("生成するオブジェクトのPrefab")]
    [SerializeField] GameObject _mapPreab;

    [Tooltip("マップデータの配列")]
    BlockData[,] mapData;

    void Start()
    {
        mapData = new BlockData[_height, _width];
        CreateMap();
    }

    /// <summary>
    /// マップを生成する
    /// </summary>
    void CreateMap() 
    {
        for (int y = 0; y < mapData.Length / _width; y++) 
        {
            for (int x = 0; x < mapData.Length / _height; x++)
            {
                GameObject blockObj = Instantiate(_mapPreab, new Vector3(x, -1 * y, 0), _mapPreab.transform.rotation);
            }
        }
    }
}

public struct BlockData
{
    public BlockData(Color color) 
    {
        Color MyColor = color;
    }

}
