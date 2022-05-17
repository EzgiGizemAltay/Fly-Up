using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer.FSM;
using Sirenix.OdinInspector;
using SirketAdi.ProjeAdi.Utils;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace SirketAdi.ProjeAdi.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField, Tooltip("Order matters!")] private List<GameState> _states;

        private StateMachine<GameState> _gameStateMachine;
        private int _stateIndex = 0;

        private void Start()
        {
            if (_states.Count > 0)
            {
                ChangeState(_states[0]);
            }
        }
        private void Awake()
        {
            if (_states.Count > 0)
            {
                GameState initialState = _states[0];
                _gameStateMachine = new StateMachine<GameState>();
                Analytics.CustomEvent("LevelStarted", new Dictionary<string, object>
                {
                    { "Level Name", SceneManager.GetActiveScene().name},
                    //{ "Level Index", PlayerPrefs.GetInt(LevelIndexIncrease._levelIndexKey)}
                });
            }
            else
            {
                Debug.Log($"[GAMECONTROLLER] States not found!");
                this.enabled = false;
                return;
            }
        }

        private void ChangeState(GameState state)
        {
            _gameStateMachine.TrySetState(state);
        }

        public void CurrentStateFinished()
        {
            _stateIndex = (_stateIndex + 1) % _states.Count;

            if (_stateIndex == 0)
            {
                Analytics.CustomEvent("LevelFinished", new Dictionary<string, object>
                {
                    { "Level Name", SceneManager.GetActiveScene().name},
                    //{ "Level Index", PlayerPrefs.GetInt(LevelIndexIncrease._levelIndexKey)}
                });
                //LevelController.Instance.NextLevel();
            }

            ChangeState(_states[_stateIndex]);
        }

        public void RestartCurrentState()
        {
            //LevelController.Instance.ReloadLevel();
            _gameStateMachine.TryResetState(_gameStateMachine.CurrentState);
        }

    }

}
