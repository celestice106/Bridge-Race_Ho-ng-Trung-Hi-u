using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public const float BRICK_SPAWN_TIME = 10f;
    public const float CHAR_MOVE_SPEED = 10f;
    public const float RAYCAST_RANGE = 2f;
    public const float BRICK_FLY_SPEED = 200f;
    public const float BRICK_FLY_TIME = .01f;
    public const float BRICK_THICKNESS = .2f;
    public const int MAX_BRICK_CARRIED = 10;
    public const float GRAVITY = -9.8f;
}

public static class GameTag
{
    public const string CHARACTER = "Character";
    public const string BRICK = "Brick";
    public const string STEP = "Step";
    public const string NEXT_FLOOR = "Floor";
}

