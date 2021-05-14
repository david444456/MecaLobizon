using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public interface IWayBoard 
    {
        List<Vector2> GetTheRealWayMovePlayer();

    }
}
