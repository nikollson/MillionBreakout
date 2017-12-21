
using System.Collections.Generic;
using Stool.CSharp;
using Stool.SceneManagement;
using UnityEngine;

namespace StoolKit.ThousandBullets
{
    class ThousandBulletsManager : MonoBehaviour
    {
        public GameObject DefaultPrefab;

        private RecycleInstanciateManager _recycle;
        private LinkedList<BulletRecycleController> _controllers;
        private Dictionary<ThousandBulletBehaviour, LinkedListNode<BulletRecycleController>> _dictionary;

        public void Awake()
        {
            _recycle = new RecycleInstanciateManager();
            _controllers = new LinkedList<BulletRecycleController>();
            _dictionary = new Dictionary<ThousandBulletBehaviour, LinkedListNode<BulletRecycleController>>();
        }

        public void AddBullet(ThousandBulletBehaviour bulletBehaviour, Vector3 position, Quaternion rotation)
        {
            var controller = new BulletRecycleController(bulletBehaviour);

            var obj = _recycle.Instanciate(controller, DefaultPrefab, position, rotation);
            obj.transform.parent = transform;

            var node = _controllers.AddLast(controller);
            _dictionary.Add(bulletBehaviour, node);
        }

        public void Remove(ThousandBulletBehaviour bulletBehaviour)
        {
            var node = _dictionary[bulletBehaviour];
            _controllers.Remove(node);
            _recycle.Remove(node.Value);
            _dictionary.Remove(bulletBehaviour);
        }

        class BulletRecycleController : IRecycleInstanceController
        {
            private ThousandBulletPrefab _bulletPrefab;
            private ThousandBulletBehaviour _behaviour;

            public BulletRecycleController(ThousandBulletBehaviour behaviour)
            {
                _behaviour = behaviour;
            }

            public void SetData(GameObject gameObject)
            {
                _bulletPrefab = gameObject.GetComponent<ThousandBulletPrefab>();
                _bulletPrefab.SerBehaviour(_behaviour);
            }

            public void ClearData(GameObject gameObject)
            {
                _bulletPrefab.ClearBehaviour();
            }
        }
    }
}
