using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    [Header("�}�b�v�̍���")]
    [SerializeField] int _height = 18;
    [Header("�}�b�v�̉���")]
    [SerializeField] int _width = 18;

    [Header("��������I�u�W�F�N�g��Prefab")]
    [SerializeField] GameObject _mapPreab;

    [Header("�}�e���A���̐F�̎��")]
    [SerializeField] List<Color> _materialColor = new List<Color>();

    [Tooltip("�}�b�v�f�[�^�̔z��")]
    [SerializeField]BlockData[,] mapData;

    [Tooltip("���ɒT���������W")]
    int[,] mapOld;

    [Tooltip("�T�����I���������ǂ���")]
    bool _clear = false;

    [Tooltip("�T������ꏊ�������Ȃ��ꍇ")]
    bool _end = false;

    [Tooltip("�����F������BlokData���i�[���Ă���")]
    List<BlockData> _sameColorList = new List<BlockData>();

    private int _nowJustification = -1;

    bool Test = false;

    public int BlockCount { get;  }

    public ChangeScore changeScore { get; }

    void Start()
    {
        //�}�b�v�̑傫���ŏ�����
        mapData = new BlockData[_height, _width];
        mapOld = new int[_height, _width];
        CreateMap();

       // BlockSearch(10, -10);
    }

    /// <summary>
    /// �}�b�v�𐶐�����
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

                //�u���b�N�f�[�^��z��ɓ����
                mapData[y, x] = new BlockData(_materialColor[a], blockObj);

                //Debug.Log($"y��{y}x��{x}{mapData[y, x].MyColor}");
            }
        }
    }

    /// <summary>
    /// �ׂ荇�����F�������u���b�N���폜����
    /// </summary>
    /// <param name="x">x���W</param>
    /// <param name="y">y���W</param>
    public void BlockSearch(int x, int y)
    {
        BlockData FirstBlock = mapData[x, y];
        List<int> BlockXPos = new List<int>();
        List<int> BlockYPos = new List<int>();

        BlockXPos.Add(y);
        BlockYPos.Add(x);

        List<int> MoveX = new List<int>();
        List<int> MoveY = new List<int>();

        //�ŏ��ɒT������ꏊ��ǉ�����
        _sameColorList.Add(mapData[x, y]);

        Debug.Log(FirstBlock.MyColor);

        while (true)
        {
            for (int i = 0; i < BlockYPos.Count; i++)
            {

                //�F�������������������ĒT���ς݂ł͂Ȃ��ꍇ�B�v�f�O���w�肵�Ă��Ȃ��Ƃ�
                if (mapData.Length / _width <= BlockYPos[i] + 1) 
                {
                }
                else if (mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] + 1, BlockXPos[i]] != 1)
                {
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] + 1);

                    mapOld[BlockYPos[i] + 1, BlockXPos[i]] = 1;
                    _end = true;
                    //Debug.Log($"�F��{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
                }

                if (BlockYPos[i] - 1 < 0) 
                {

                }
                else if (mapData[BlockYPos[i] - 1, BlockXPos[i]].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i] - 1, BlockXPos[i]] != 1)
                {
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] - 1);

                    mapOld[BlockYPos[i] - 1, BlockXPos[i]] = 1;
                    _end = true;
                    //Debug.Log($"�F��{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
                }

                if (mapData.Length / _height <= BlockXPos[i] + 1) 
                {

                }
                else if (mapData[BlockYPos[i], BlockXPos[i] + 1].MyColor == FirstBlock.MyColor && mapOld[BlockYPos[i], BlockXPos[i] + 1] != 1)
                {
                    //������������ۑ�����
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
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i] - 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] - 1] = 1;
                    _end = true;
                    //Debug.Log($"�F��{mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor}");
                }


            }
            
            BlockXPos.Clear();
            BlockYPos.Clear();

            BlockXPos = new List<int>(MoveX);
            BlockYPos = new List<int>(MoveY);
            //�G���[���o����[�P����
            //�����F���������̂����X�g�ɂ܂Ƃ߂�
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
    /// �����F�̃I�u�W�F�N�g����������
    /// </summary>
    /// <returns></returns>
    public int BlockDestroy() 
    {
        GameObject.FindObjectOfType<ChangeScore>().ScoreCount();
        var BlockCount = _sameColorList.Count;
        //Debug.Log(BlockCount);
        if(BlockCount == 1) 
        {
            Debug.Log("�Ԃ��܂���");
            _sameColorList.Clear();
            return 0;
           
        }
       _sameColorList.Clear();

        return BlockCount;
    }

    private void VerticalChack() 
    {
        Debug.Log("�Ă΂ꂽ");
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

               Debug.Log($"Count��{Count}");   
                if (Count == mapData.Length / _width) 
                {
                    Debug.Log("�c�S�ċ󔒂ł�");
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
