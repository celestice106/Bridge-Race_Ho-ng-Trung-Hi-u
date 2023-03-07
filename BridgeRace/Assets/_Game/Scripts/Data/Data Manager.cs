using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public const float BRICK_SPAWN_TIME = 10f;
    public const float MOVE_SPEED = 450f;
    public const float RAYCAST_RANGE = 2f;
    public const float VALID_STEP_CHECKER_OFFSET = .5f;
}

public static class GameTag
{
    public const string PLAYER = "Player";
    public const string BRICK = "Brick";
    public const string STEP = "Step";
    public const string FLOOR = "Floor";
}

public static class GameLayer
{
    public const string STAIR = "Stair";
}