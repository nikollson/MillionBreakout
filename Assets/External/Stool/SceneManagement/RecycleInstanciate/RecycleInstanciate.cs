

using System.Collections.Generic;
using UnityEngine;

namespace Stool.SceneManagement
{
    class RecycleInstanciate
    {
        private Queue<int> emptyIndexQueue = new Queue<int>();
        private List<RecycleObjectData> DataList = new List<RecycleObjectData>();

        public GameObject Instanciate(IRecycleInstanciateSetting setting, Vector3 position, Quaternion rotation)
        {
            int index;
            if (emptyIndexQueue.Count != 0)
            {
                index = emptyIndexQueue.Peek();
                emptyIndexQueue.Dequeue();
            }
            else
            {
                index = DataList.Count;
                DataList.Add(new RecycleObjectData());
            }

            DataList[index].SetData(setting, position, rotation);

            return DataList[index].GameObject;
        }

        public void Update()
        {
            for(int i=0;i<DataList.Count;i++)
            {
                if (DataList[i].IsLiving != true)
                {
                    continue;
                }

                DataList[i].Update();

                if (DataList[i].IsLiving == false)
                {
                    emptyIndexQueue.Enqueue(i);
                    DataList[i].ClearData();
                }
            }
        }

        class RecycleObjectData
        {
            public IRecycleInstanciateSetting Setting { get; private set; }
            public GameObject GameObject { get; private set; }
            public bool IsLiving { get; private set; }

            public void SetData(IRecycleInstanciateSetting setting, Vector3 position, Quaternion rotation)
            {
                Setting = setting;

                if (GameObject == null)
                {
                    GameObject = (GameObject) Object.Instantiate(setting.GetDefaltPrefab(), position, rotation);
                }
                else
                {
                    GameObject.transform.position = position;
                    GameObject.transform.rotation = rotation;
                }
                
                IsLiving = true;
                Setting.SetData(GameObject);
            }

            public void ClearData()
            {
                IsLiving = false;
                Setting.ClearData(GameObject);
            }

            public void Update()
            {
                if (Setting.IsEnable() == false)
                {
                    IsLiving = false;
                }
            }
        }
    }
}
