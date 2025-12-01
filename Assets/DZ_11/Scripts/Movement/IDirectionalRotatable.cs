using UnityEngine;

namespace DZ_11
{
    public interface IDirectionalRotatable
    {
        Quaternion CurrentRotation { get; }

        void SetRotationDirection(Vector3 inputDirection);
    }
}