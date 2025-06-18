using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncestralPotatoes.Scene
{
    public class StartNextScene : MonoBehaviour
    {
        [SerializeField] private int delay = 500;
        [SerializeField] private int scene = 1;

        private async void Start()
        {
            await UniTask.Delay(delay);
            SceneManager.LoadScene(scene);
        }
    }
}