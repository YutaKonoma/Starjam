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
    BlockData[,] mapData;

    [Tooltip("���ɒT���������W")]
    int[,] mapOld;

    [Tooltip("�T�����I���������ǂ���")]
    bool _clear = false;

    [Tooltip("�T������ꏊ�������Ȃ��ꍇ")]
    bool _end = false;



    void Start()
    {
        //�}�b�v�̑傫���ŏ�����
        mapData = new BlockData[_height, _width];
        mapOld = new int[_height, _width];
        CreateMap();
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
            }
        }
    }

    /// <summary>
    /// �ׂ荇�����F�������u���b�N���폜����
    /// </summary>
    /// <param name="x">x���W</param>
    /// <param name="y">y���W</param>
    void BlockDestroy(int x, int y)
    {
        //�z��Ŏg����悤��?1��������
        var Y = y * -1;
        BlockData FirstBlock = mapData[x, Y];
        List<int> BlockXPos = new List<int>();
        List<int> BlockYPos = new List<int>();

        BlockXPos.Add(x);
        BlockYPos.Add(Y);

        List<int> MoveX = new List<int>();
        List<int> MoveY = new List<int>();

        while (true)
        {
            for (int i = 0; i < BlockYPos.Count; i++)
            {
                //�z��̗v�f�O���w�肵���Ƃ�
                if (mapData.Length / _width <= BlockYPos[i] + 1 || BlockYPos[i] - 1 < 0)
                {
                    Debug.Log("�T�����I�����܂���");
                    _clear = true;
                    break;
                }
                if (mapData.Length / _height <= BlockXPos[i] + 1 || BlockXPos[i] - 1 < 0)
                {
                    Debug.Log("�T�����I�����܂���");
                    _clear = true;
                    break;
                }

                //�F�������������������ĒT���ς݂ł͂Ȃ��ꍇ
                if (mapData[BlockYPos[i] + 1, BlockXPos[i]].MyColor == mapData[BlockYPos[i], BlockXPos[i]].MyColor && mapOld[BlockYPos[i] + 1, BlockXPos[i]] != 1)
                {
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] + 1);

                    mapOld[BlockYPos[i] + 1, BlockXPos[i]] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i] - 1, BlockXPos[i]].MyColor == mapData[BlockYPos[i], BlockXPos[i]].MyColor && mapOld[BlockYPos[i] - 1, BlockXPos[i]] != 1)
                {
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i]);
                    MoveY.Add(BlockYPos[i] - 1);

                    mapOld[BlockYPos[i] - 1, BlockXPos[i]] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i], BlockXPos[i] + 1].MyColor == mapData[BlockYPos[i], BlockXPos[i]].MyColor && mapOld[BlockYPos[i], BlockXPos[i] + 1] != 1)
                {
                    //������������ۑ�����
                    MoveX.Add(BlockXPos[i] + 1);
                    MoveY.Add(BlockYPos[i]);

                    mapOld[BlockYPos[i], BlockXPos[i] + 1] = 1;
                    _end = true;
                }

                if (mapData[BlockYPos[i], BlockXPos[i] - 1].MyColor == mapData[BlockYPos[i], BlockXPos[i]].MyColor && mapOld[BlockYPos[i], BlockXPos[i] - 1] != 1)
                {
                    //������������ۑ�����
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

            MoveX.Clear();
            MoveY.Clear();

            if (_clear)
            {
                break;
            }

            if (!_end) 
            {
                break;
            }

            _end = false; 

        }



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
