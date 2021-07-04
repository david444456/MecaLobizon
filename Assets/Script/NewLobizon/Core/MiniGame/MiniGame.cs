using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobizon.Core
{
    public abstract class MiniGame : MonoBehaviour
    {
        public Sprite spriteThisMiniGame;

        public abstract void StartNewMiniGame();
    }

   /* public interface IStartMiniGame{
        void StartNewMiniGame(int index);
    }*/
}
