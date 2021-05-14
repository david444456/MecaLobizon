
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class ControlCoinInGame : MonoBehaviour, IControlCoinInGame
    {
        [Header("UI")]
        [SerializeField] Text textCoin;

        private int _actualCoin = 0;

        private void Start()
        {
            textCoin.text = _actualCoin.ToString();
        }

        public int GetActualCoin() => _actualCoin;

        public void SetCoinRewardEvent(int count)
        {
            _actualCoin += count;
            textCoin.text = _actualCoin.ToString();
        }
    }
}
