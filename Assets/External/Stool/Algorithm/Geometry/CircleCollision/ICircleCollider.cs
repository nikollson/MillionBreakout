
using UnityEngine;

namespace Stool.Algorithm.Geometry
{
    public interface ICircleCollider
    {
        Vector2 GetColliderCenter();
        float GetColliderRadius();
    }
}
