using Rule;
using UnityEngine;

namespace EventHandler
{
    [RequireComponent(typeof(GameRule))]
    public class GameRuleEventHandler : MonoBehaviour
    {
        [Header("Event Poster")] [SerializeField]
        private Actor.Actor actor;

        private GameRule _gameRule;

        private void Start()
        {
            _gameRule = GetComponent<GameRule>();
            BindingEvent();
        }

        private void BindingEvent()
        {
            actor.actorDead.RegisterEvent(OnActorDead);
        }

        private void OnActorDead()
        {
            _gameRule.FailGame();
        }
    }
}