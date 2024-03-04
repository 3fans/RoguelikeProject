using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirection
{
    #region Direction Interface
    enum Direction8
    {
        E,
        NE,
        N,
        NW,
        W,
        SW,
        S,
        SE,
        Zero
    }

    Direction8 VectorToDirection(Vector2 vector);

    #endregion

    Direction8 direction8 { get; set; }
}
