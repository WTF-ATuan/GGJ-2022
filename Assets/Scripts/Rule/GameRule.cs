using UnityEngine;
using UnityEngine.Events;

namespace Rule
{
    public class GameRule : MonoBehaviour
    {
        [SerializeField] private UnityEvent failGameViewEvent;
        [SerializeField] private UnityEvent passGameViewEvent;

        private void Start()
        {
            Time.timeScale = 1;
        }

        public void FailGame()
        {
            failGameViewEvent?.Invoke();
            Time.timeScale = 0;
        }

        public void PassGame()
        {
            passGameViewEvent?.Invoke();
            Time.timeScale = 0;
        }
    }
}