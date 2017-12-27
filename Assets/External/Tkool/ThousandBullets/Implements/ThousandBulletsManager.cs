
using System;
using System.Collections.Generic;
using Stool.CSharp;
using Stool.SceneManagement;
using UnityEngine;

namespace Tkool.ThousandBullets
{
    class ThousandBulletsManager : MonoBehaviour
    {
        public GameObject DefaultPrefab;

        public Func<float> GetDeltaTime { get; private set; }

        private RecycleInstanciateManager _recycle;
        private LinkedList<BulletRecycleController> _controllers;
        private Dictionary<ThousandBulletBehaviour, LinkedListNode<BulletRecycleController>> _dictionary;

        public void Awake()
        {
            _recycle = new RecycleInstanciateManager();
            _controllers = new LinkedList<BulletRecycleController>();
            _dictionary = new Dictionary<ThousandBulletBehaviour, LinkedListNode<BulletRecycleController>>();

            GetDeltaTime = () => Time.deltaTime;
        }

        public void Update()
        {
            foreach (var controller in _controllers)
            {
                controller.BulletBehaviour.OnUpdateBullet(GetDeltaTime());
            }
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

        public void RemoveIf(
            Func<ThousandBulletBehaviour, bool> judgeFunc,
            Action<ThousandBulletBehaviour> callBack = null)
        {
            _controllers.RemoveNodeIf(
                x => judgeFunc(x.BulletBehaviour),
                x =>
                {
                    callBack(x.BulletBehaviour);
                    _dictionary.Remove(x.BulletBehaviour);
                    _recycle.Remove(x);
                });
        }

        public void ForeachBullets(Action<ThousandBulletBehaviour> callback)
        {
            foreach (var controller in _controllers)
            {
                callback(controller.BulletBehaviour);
            }
        }

        public void SetDeltaTimeFunction(Func<float> deltaTimeFunction)
        {
            GetDeltaTime = deltaTimeFunction;
        }


        class BulletRecycleController : IRecycleInstanceController
        {
            public ThousandBulletPrefab BulletPrefab { get; private set; }
            public ThousandBulletBehaviour BulletBehaviour { get; private set; }

            public BulletRecycleController(ThousandBulletBehaviour behaviour)
            {
                BulletBehaviour = behaviour;
            }

            public void SetData(GameObject gameObject)
            {
                BulletPrefab = gameObject.GetComponent<ThousandBulletPrefab>();
                BulletPrefab.SerBehaviour(BulletBehaviour);
            }

            public void ClearData(GameObject gameObject)
            {
                BulletPrefab.ClearBehaviour();
            }
        }
    }
}
