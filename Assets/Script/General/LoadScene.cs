using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace General {
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] GameObject gameObjectProgress;
        [SerializeField] Slider sliderProgress;

        public void LoadSceneByIndex(int index) {
            SceneManager.LoadScene(index);
        }

        public void ExitApp() {
            Application.Quit();
        }

        public void loadSceneByNumberAsync(int numberScene)
        {
            StartCoroutine(LoadAsyncScene(numberScene));
        }


        IEnumerator LoadAsyncScene(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);


            gameObjectProgress.SetActive(true);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                sliderProgress.value = progress;

                yield return null;
            }

        }

    }
}
