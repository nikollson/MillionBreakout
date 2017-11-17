
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Stool.MilllionBullets
{
    class MillionBulletsBlocksCollider : MillionBulletsBoxCollider
    {
        [SerializeField] private int _arrayWidth = 1;
        [SerializeField] private int _arrayHeight = 1;
        [SerializeField] private int _lifePoint = 1;
        [Multiline(10)]
        [SerializeField] private string _form;

        public ComputeBuffer BlockElementsBuffer { get; private set; }

        private void Awake()
        {
            var lifeArray = GetLifeArray();
            var elements = new BlockElement[lifeArray.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = new BlockElement(i%_arrayWidth, i/_arrayWidth, lifeArray[i]);
            }
            BlockElementsBuffer = new ComputeBuffer(elements.Length, Marshal.SizeOf(typeof(BlockElement)));
            BlockElementsBuffer.SetData(elements);
        }

        private void OnDestroy()
        {
            BlockElementsBuffer.Release();
        }

        public BlocksInfo GetBlocksInfo()
        {
            var box = GetBox();
            return new BlocksInfo(box, _arrayWidth, _arrayHeight);
        }

        private int[] GetLifeArray()
        {
            var array = new int[_arrayWidth * _arrayHeight];
            var splitForm = _form.Split(new char[] { '\n' });
            for (int i = 0; i < _arrayHeight; i++)
            {
                for (int j = 0; j < _arrayWidth; j++)
                {
                    var value = _lifePoint;
                    if (_form != "" && splitForm[_arrayHeight - i - 1][j] == '0')
                    {
                        value = 0;
                    }
                    array[i * _arrayWidth + j] = value;
                }
            }
            return array;
        }
    }

    class BlocksInfo
    {
        public BoxData Box;
        public int ArrayWidth;
        public int ArrayHeight;

        public BlocksInfo(BoxData box, int arrayWidth, int arrayHeight)
        {
            Box = box;
            ArrayHeight = arrayHeight;
            ArrayWidth = arrayWidth;
        }
    }

    struct BlockElement
    {
        public int X;
        public int Y;
        public int LifePoint;

        public BlockElement(int x, int y, int lifePoint)
        {
            X = x;
            Y = y;
            LifePoint = lifePoint;
        }
    }
}
