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
    [SerializeField]BlockData[,] mapData;

    [Tooltip("既に探索した座標")]
    int[,] mapOld;

    [Tooltip("探索が終了したかどうか")]
    bool _clear = false;

    [Tooltip("探索する場所がもうない場合")]
    bool _end = false;

    [Tooltip("同じ色だったBlokDataを格納しておく")]
    List<BlockData> _sameColorList = new List<BlockData>();

    private int _nowJustification = -1;

    bool Test = false;

    public int BlockCount { get;  }

    public ChangeScore changeScore { get; }

    void Start()
    {
        //マップの大きさで初期化
        mapData = new BlockData[_height, _width];
        mapOld = new int[_height, _width];
        CreateMap();

       // BlockSearch(10, -10);
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

                //Debug.Log($"yは{y}xは{x}{mapData[y, x].MyColor}");
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
        BlockData FirstBlock = mapData[x, y];
        List<int> BlockXPos = new List<int>();
        List<int> BlockYPos = new List<int>();

        BlockXPos.Add(y);
        BlockYPos.Add(x);

        List<int> MoveX = new List<int>();
        List<int> MoveY = new List<int>();

        //最初に探索する場所を追加する
        _sameColorList.Add(mapData[x, y]);

        Debug.Log(FirstBlock.MyColor);

        while (true)
        {
            for (int i = 0; i < BlockYPos.Count; i++)
            {

                //色が同じだった時そして探索済みではない場合。要素外を指定していないとき
                if (mapData.Length / _width <= BlockYPos[i] + 1) 
                {
                }
                else if (mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] + 1, BlockXPos[i]] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] + 1);

                    mapOld[BlockYPos[i] + 1, BlockXPos[i]] = 1;
                    _end = true;
                    //Debug.Log($"色は{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
                }

                if (BlockYPos[i] - 1 < 0) 
                {

                }
                else if (mapData[BlockYPos[i] - 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] - 1, BlockXPos[i]] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] - 1);

                    mapOld[BlockYPos[i] - 1, BlockXPos[i]] = 1;
                    _end = true;
                    //Debug.Log($"色は{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
                }

                if (mapData.Length / _height <= BlockXPos[i] + 1) 
                {

                }
                else if (mapData[BlockYPos[i], BlockXPos[i] + 1].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i], BlockXPos[i] + 1] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i] + 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] + 1] = 1;
                    _end = true;
                }

                if (BlockXPos[i] - 1 < 0) 
                {

                }
                else if (mapData[BlockYPos[i], BlockXPos[i] - 1].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i], BlockXPos[i] - 1] != 1)
                {
                    //動いた方向を保存する
                    MoveX.Add(BlockXPos[i] - 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] - 1] = 1;
                    _end = true;
                    //Debug.Log($"色は{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
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
                mapData[MoveY[i], MoveX[i]].MyColor = Color.white;
                mapData[MoveY[i], MoveX[i]].MyGameObject.GetComponent<SpriteRenderer>().color = Color.white;
               

                _sameColorList.Add(mapData[MoveY[i], MoveX[i]]); 
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

         var a =  BlockDestroy();

        if (a != 0) 
        {
            VerticalChack();

        }
    }

    /// <summary>
    /// 同じ色のオブジェクトを消去する
    /// </summary>
    /// <returns></returns>
    public int BlockDestroy() 
    {
        GameObject.FindObjectOfType<ChangeScore>().ScoreCount();
        var BlockCount = _sameColorList.Count;
        //Debug.Log(BlockCount);
        if(BlockCount == 1) 
        {
            Debug.Log("返しました");
            _sameColorList.Clear();
            return 0;
           
        }
       _sameColorList.Clear();

        return BlockCount;
    }

    private void VerticalChack() 
    {
        Debug.Log("呼ばれた");
        BlockData[] stackData = new BlockData[mapData.Length / _width];
        for (int y = 0; y < mapData.Length / _width + _nowJustification; y++)
        {
            for (int x = 0; x < mapData.Length / _height; x++)
            {
              stackData[y] = mapData[x, y];
            }

            foreach (var i in stackData) 
            {
                var Count = 0;
                if (i.MyColor == Color.white) 
                {
                    Count++;
                }

               Debug.Log($"Countは{Count}");   
                if (Count == mapData.Length / _width) 
                {
                    Debug.Log("縦全て空白です");
                    leftJustification(y);
                    return;
                }
            }
        }
    }


    private void leftJustification(int y) 
    {
        for (int i = y; y < mapData.Length / _width; i++) 
        {
            for (int j = y; y < mapData.Length / _height; j++) 
            {
                mapData[i, j] = mapData[i + 1, j];
            }
        }

        _nowJustification++;
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
