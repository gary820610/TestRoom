using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviMapData
{
    public NaviMapData(Vector3 topRight, Vector3 botLeft)
    {
        _topR = topRight;
        _botL = botLeft;
    }
    
    public float LeftBorder { get { return _botL.x; } }
    public float RightBorder { get { return _topR.x; } }
    public float TopBorder { get { return _topR.z; } }
    public float BotBorder { get { return _botL.z; } }

    Vector3 _topR;
    Vector3 _botL;

    Vector3 _playerSpawnPos;
    List<Vector3> _enemySpawnPos;

}
