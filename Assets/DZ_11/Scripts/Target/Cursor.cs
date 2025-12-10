using UnityEngine;

namespace DZ_11
{
    public class Cursor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AgentPlayer>() != null)
                Destroy(gameObject);
        }
    }
}