
using System.Collections.Generic;
using UnityEngine;

namespace Stool.SceneManagement
{
    class RecycleInstanciateManager
    {
        private Queue<int> _emptyIndices;
        private Dictionary<IRecycleInstanceController, int> _indexDictionary;
        private List<GameObject> _gameObjects;


        public RecycleInstanciateManager()
        {
            _emptyIndices = new Queue<int>();
            _indexDictionary = new Dictionary<IRecycleInstanceController, int>();
            _gameObjects = new List<GameObject>();
        }

        public GameObject Instanciate(
            IRecycleInstanceController controller, GameObject defaultPrefab,
            Vector3 position, Quaternion rotation)
        {
            int index;

            if (_emptyIndices.Count != 0)
            {
                index = _emptyIndices.Peek();
                _emptyIndices.Dequeue();
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
            _indexDictionary.Add(controller, index);

            return _gameObjects[index];
        }

        public void Remove(IRecycleInstanceController controller)
        {
            int index = _indexDictionary[controller];
            controller.ClearData(_gameObjects[index]);

            _indexDictionary.Remove(controller);
            _emptyIndices.Enqueue(index);
        }
    }
}
