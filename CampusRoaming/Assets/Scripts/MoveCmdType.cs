using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public enum UserMoveType
{
    None,
    AdjustToMove,  
    MoveForward,
    MoveBack,

    //不同操作引起的旋转
    MouseRotateLeft,
    MouseRotateRight,
    RotateLeft,
    RotateRight,
}

/// <summary>
/// 相机镜头的移动方式
/// </summary>
public enum CameraMove
{
    None,
    MoveNear,
    MoveFar,
}