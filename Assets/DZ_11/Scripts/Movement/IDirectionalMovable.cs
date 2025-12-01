using UnityEngine;

namespace DZ_11
{
    public interface IDirectionalMovable
    {
        Vector3 CurrentPosition { get; }
        Vector3 CurrentVelocity { get; }

        void SetMoveDirection(Vector3 inputDirection);

        void StopMove();
    }
}