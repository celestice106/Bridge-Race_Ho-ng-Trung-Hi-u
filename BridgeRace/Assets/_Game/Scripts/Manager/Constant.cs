using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public const float BRICK_SPAWN_TIME = 10f;
    public const float CHAR_MOVE_SPEED = 10f;
    public const float RAYCAST_RANGE = 2f;
    public const float BRICK_SPREAD = 10f;
    public const float BRICK_FLY_SPEED = 50f;
    public const float BRICK_THICKNESS = .2f;
    public const int MAX_BRICK_CARRIED = 10;
    public const float GRAVITY = -9.8f;
    public const float STAND_UP_TIME = 3f;
}

public static class GameTag
{
    public const string CHARACTER = "Character";
    public const string BRICK = "Brick";
    public const string STEP = "Step";
    public const string NEXT_FLOOR = "Floor";
    public const string WIN_ZONE = "Win zone";
    public static string BRIDGE = "Bridge";
}

public static class AnimName
{
    public const string RUN = "Run";
    public const string IDLE = "Idle";
    public const string FALL = "Fall";
    public const string CHEER = "Cheer";
}


