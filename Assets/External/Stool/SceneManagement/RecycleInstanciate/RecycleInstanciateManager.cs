
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Stool.SceneManagement
{
    class RecycleInstanciateManager
    {
        private List<GameObject> _gameObjects;
        private Queue<int> _emptyObjectIndex;
        private Dictionary<IRecycleInstanceController, int> _usedIndexDictionary;

        public RecycleInstanciateManager()
        {
            _emptyObjectIndex = new Queue<int>();
            _gameObjects = new List<GameObject>();
            _usedIndexDictionary = new Dictionary<IRecycleInstanceController, int>();
        }

        public GameObject Instanciate(
            IRecycleInstanceController controller, GameObject defaultPrefab,
            Vector3 position, Quaternion rotation)
        {

            int index;

            if (_emptyObjectIndex.Count != 0)
            {
                index = _emptyObjectIndex.Peek();
                _emptyObjectIndex.Dequeue();
            }
            else
            {
                GameObject obj = Object.Instantiate(defaultPrefab);
                _gameObjects.Add(obj);
                index = _gameObjects.Count - 1;
            }

            _gameObjects[index].transform.position = position;
            _gameObjects[index].transform.rotation = defaultPrefab.transform.rotation * rotation;

            controller.SetData(_gameObjects[index]);
            _usedIndexDictionary.Add(controller, index);

            return _gameObjects[index];
        }

        public void Remove(IRecycleInstanceController controller)
        {
            int index = _usedIndexDictionary[controller];
            controller.ClearData(_gameObjects[index]);

            _usedIndexDictionary.Remove(controller);
            _emptyObjectIndex.Enqueue(index);
        }

        public void RemoveIf(Func<IRecycleInstanceController, bool> judgeFunc,
            Action<IRecycleInstanceController> callBack = null)
        {
            var removeList = new List<IRecycleInstanceController>();
            foreach (var i in _usedIndexDictionary)
            {
                if (judgeFunc(i.Key) == true)
                {
                    removeList.Add(i.Key);
                }
            }
            foreach (var controller in removeList)
            {
                callBack(controller);
                Remove(controller);
            }
        }
    }
}
