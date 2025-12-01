using UnityEngine;

namespace DZ_11
{
    public interface IDirectionalRotatable : ITransformPosition
    {
        Quaternion CurrentRotation { get; }

        void SetRotationDirection(Vector3 inputDirection);
    }
}