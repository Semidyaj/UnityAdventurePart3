using UnityEngine;

namespace DZ_11
{
    public class BottleSpawnPoint : MonoBehaviour
    {
        private Bottle _bottle;

        public Vector3 Position => transform.position;

        public bool IsEmpty
        {
            get
            {
                if (_bottle == null)
                    return true;

                if (_bottle.gameObject == null)
                    return true;

                return false;
            }
        }

        public void Occupy(Bottle bottle) => _bottle = bottle;
    }
}