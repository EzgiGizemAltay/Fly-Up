using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        [SerializeField] private GameObject _pinktext, _bluetext;
        private MonoBehaviour _mb;
       

        private void Start()
        {
            _mb = GameObject.FindObjectOfType<MonoBehaviour>();
            _mb.StartCoroutine(DestroyText());
        }

        public void OnPausePressed()
        {
            Time.timeScale = 0;
            PauseMenu.Open();
            
        }
        public void OnRestartPressed()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            base.OnBackPressed();
        }
        public void OnNextLevelPressed()
        {
            base.OnBackPressed();
            LevelLoader.LoadNextLevel();
        }

        public void DieOp()
        {
            DieScreen.Open();
        }
        private IEnumerator DestroyText()
        {
            yield return new WaitForSeconds(4);
            _pinktext.GetComponent<Text>().DOFade(0, 2f);
            _bluetext.GetComponent<Text>().DOFade(0, 2f);

        }
    }
}