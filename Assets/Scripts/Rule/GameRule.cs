using UnityEngine;
using UnityEngine.Events;

namespace Rule
{
    public class GameRule : MonoBehaviour
    {
        [SerializeField] private UnityEvent failGameViewEvent;
        [SerializeField] private UnityEvent passGameViewEvent;

        public void FailGame()
        {
            failGameViewEvent?.Invoke();
        }

        public void PassGame()
        {
            passGameViewEvent?.Invoke();
        }
    }
}