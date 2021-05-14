using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General {
    public class LoadScene : MonoBehaviour
    {
        public void LoadSceneByIndex(int index) {
            SceneManager.LoadScene(index);
        }

    }
}
