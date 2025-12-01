using UnityEngine;

namespace DZ_11
{
    public interface IDirectionalMovable : ITransformPosition
    {
        Vector3 CurrentVelocity { get; }

        void SetMoveDirection(Vector3 inputDirection);

        void StopMove();
    }
}