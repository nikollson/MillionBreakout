
using System.Collections.Generic;
using Stool.SceneManagement;
using UnityEngine;

namespace StoolKit.ThousandBullets
{
    class ThousandBulletsManager : MonoBehaviour
    {
        public GameObject DefaultPrefab;

        private RecycleInstanciateManager _recycle;
        private LinkedList<BulletRecycleController> _controllers;

        public void Awake()
        {
            _recycle = new RecycleInstanciateManager();
            _controllers = new LinkedList<BulletRecycleController>();
        }

        public void AddBullet(ThousandBulletBehaviour bulletBehaviour, Vector3 position, Quaternion rotation)
        {
            var controller = new BulletRecycleController(bulletBehaviour);

            var obj = _recycle.Instanciate(controller, DefaultPrefab, position, rotation);
            obj.transform.parent = transform;

            _controllers.AddLast(controller);
        }

        public void Update()
        {
            var current = _controllers.First;

            while(current!=null)
            {
                var next = current.Next;

                if (current.Value.IsEnable() == false)
                {
                    _recycle.Remove(current.Value);
                    _controllers.Remove(current);
                }

                current = next;
            }
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

            public bool IsEnable()
            {
                return _bulletPrefab.IsEnable();
            }
        }
    }
}
