using UnityEngine;
using UnityEngine.Events;

namespace RUI
{
    [AddComponentMenu("RUI/ActivityTracker")]
    public class ActivityTracker : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnable;
        [SerializeField] private UnityEvent onDisable;

        private void OnEnable()
        {
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }
    }
}
