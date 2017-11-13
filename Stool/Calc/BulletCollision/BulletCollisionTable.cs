using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Stool.Calc.BulletCollision
{
    public class BulletCollisionDetecter
    {
        public BulletCollisionDetecter(float cellSize, Rect coverArea)
        {

        }

        public void AddBody(BulletCollionBody ballCollisionBody)
        {
            ballCollisionBody.OnDestroyAsObservable().Subscribe(_ => OnDestroyBody(ballCollisionBody));
        }

        public void OnDestroyBody(BulletCollionBody ballCollisionBody)
        {
            Debug.Log("Destroy " + ballCollisionBody.name);
        }
    }
}
