using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class DeathCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [Inject] private readonly Player player;

        private void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            player.OnDeath += () =>
            {
                canvasGroup.DOFade(1, 1);
                canvasGroup.interactable = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Quit();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}