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

    [Header("マテリアルの色の種類")]
    [SerializeField] List<Color> _materialColor = new List<Color>();

    [Tooltip("マップデータの配列")]
    BlockData[,] mapData;

    [Tooltip("既に探索した座標")]
    int[,] mapOld;

    [Tooltip("探索が終了したかどうか")]
    bool _clear = false;

    [Tooltip("探索する場所がもうない場合")]
    bool _end = false;

    [Tooltip("同じ色だったBlokDataを格納しておく")]
    List<BlockData> _sameColorList = new List<BlockData>();



    void Start()
    {
        //マップの大きさで初期化
        mapData = new BlockData[_height, _width];
        mapOld = new int[_height, _width];
        CreateMap();

        BlockSearch(10, -10);
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
                var a = Random.Range(0, _materialColor.Count);
                blockObj.GetComponent<SpriteRenderer>().color = _materialColor[a];
                //ブロックデータを配列に入れる
                mapData[y, x] = new BlockData(_materialColor[a], blockObj);
            }
        }
    }

    /// <summary>
    /// 隣り合った色が同じブロックを削除する
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    public void BlockSearch(int x, int y)
    {
        //配列で使えるように?1をかける
        var Y = y * -1;
        BlockData FirstBlock = mapData[x, Y];
        List<int> BlockXPos = new List<int>();
        List<int> BlockYPos = new List<int>();

        BlockXPos.Add(x);
        BlockYPos.Add(Y);

        List<int> MoveX = new List<int>();
        List<int> MoveY = new List<int>();

        //最初に探索する場所を追加する
        _sameColorList.Add(mapData[x, Y]);

        while (true)
        {
            for (int i = 0; i < BlockYPos.Count; i++)
            {
                //配列の要素外を指定したとき
                if (mapData.Length / _width <= BlockYPos[i] + 1 || BlockYPos[i] - 1 < 0)
                {
                    Debug.Log("探索が終了しました");
                    _clear = true;
                    break;
                }
                if (mapData.Length / _height <= BlockXPos[i] + 1 || BlockXPos[i] - 1 < 0)
                {
                    Debug.Log("探索が終了しました");
                    _clear = true;
                    break;
                }

                //色が同じだった時そして探索済みではない場合
                if (mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] + 1, BlockXPos[i]] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] + 1);

                    mapOld[BlockYPos[i] + 1, BlockXPos[i]] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i] - 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] - 1, BlockXPos[i]] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] - 1);

                    mapOld[BlockYPos[i] - 1, BlockXPos[i]] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i], BlockXPos[i] + 1].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i], BlockXPos[i] + 1] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i] + 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] + 1] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i], BlockXPos[i] - 1].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i], BlockXPos[i] - 1] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i] - 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] - 1] = 1;
                    _end = true;
                }

            }
            
            BlockXPos.Clear();
            BlockYPos.Clear();

            BlockXPos = new List<int>(MoveX);
            BlockYPos = new List<int>(MoveY);
            //エラーが出たらー１消す
            //同じ色だったものをリストにまとめる
            for(int i = 0; i < MoveY.Count; i++) 
            {
                _sameColorList.Add(mapData[MoveX[i], MoveY[i]]); 
            }

            MoveX.Clear();
            MoveY.Clear();

            if (_clear)
            {
                _end = false;
                _clear = false;
                BlockXPos.Clear();
                BlockYPos.Clear();
                break;
            }

            if (!_end) 
            {
                _end = false;
                _clear = false;
                BlockXPos.Clear();
                BlockYPos.Clear();
                break;
            }

            _end = false; 

        }

        BlockDestroy();
    }

    /// <summary>
    /// 同じ色のオブジェクトを消去する
    /// </summary>
    /// <returns></returns>
    public int BlockDestroy() 
    {
        var BlockCount = _sameColorList.Count;

        foreach (var i in _sameColorList) 
        {
            Debug.Log(i.MyColor);
            i.MyGameObject.SetActive(false);
        }

       _sameColorList.Clear();

        return BlockCount;
    }
}

public struct BlockData
{
    public GameObject MyGameObject;

    public Color MyColor;
    public BlockData(Color color, GameObject gameObject)
    {
        MyColor = color;
        MyGameObject = gameObject;
    }

}
