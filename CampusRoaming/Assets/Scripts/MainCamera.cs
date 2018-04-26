using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// function:控制玩家运动的主脚本，以及菜单显示
/// date:2018-4-20 23:13:20
/// </summary>
public class MainCamera : MonoBehaviour
{
    public static MainCamera instance = null;
    public BoxCollider colliderRoomArea;
    public Texture2D textureMenuBg;
    /// <summary>
    /// 相机跟随Box的组件
    /// </summary>
    public FollowPlayer followCom;
    public Transform tranCube;//用来做测试的人偶
    public Rigidbody cubeRigibody;
    /// <summary>
    /// 行走速度
    /// </summary>
    private float moveSpeed = 4f;//鼠标2f;//1.5f;//1f;//0.1f;//0.05f;//3f;//0.5f;
    /// <summary>
    /// 行走的加速度
    /// </summary>
    private float moveSpeedAddBase = 1f;

    /// <summary>
    /// 左右的转向速度
    /// </summary>
    private float rotateSpeed = 1f;
    private float mouseRotateRate = 0.5f;//鼠标滑动旋转是 键盘旋转的50%幅度

    private static Vector2 VecMax = new Vector2(float.MaxValue, float.MaxValue);
    //判定鼠标滑动屏幕相关的
    private Vector2 first = VecMax;
    private Vector2 second = VecMax;

    /// <summary>
    /// 当前运动方式,前进后退
    /// </summary>
    private UserMoveType _cmdMove = UserMoveType.None;
    /// <summary>
    /// 还是左右转
    /// </summary>
    private UserMoveType _cmdRotate = UserMoveType.None;
   
    /// <summary>
    /// 当前是否正常可行走状态，否的话，先走到最近距离 再允许移动
    /// </summary>
    private bool _cmdMoveToNormal = true;

    private GUIStyle _guiStyle;
    private string strGUITip = "";
    private string paramStateTip = "";

    // Use this for initialization
    void Awake()
    {
        strGUITip  += "键盘映射长按：W:前进，S：后退。Q：向左转。E：向右转。";
        instance = this;
    }
     
    // Update is called once per frame
    void Update()
    {
        float rotateSpeeedUse = rotateSpeed;

        if (Input.GetKeyDown(KeyCode.W))
        {
            TestLog.Log("向前走");
            _cmdMove = UserMoveType.MoveForward;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TestLog.Log("往后退");
            _cmdMove = UserMoveType.MoveBack;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestLog.Log("向左转");
            _cmdRotate = UserMoveType.RotateLeft;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TestLog.Log("向右转");
            _cmdRotate = UserMoveType.RotateRight;
        }

        //判定是否终止标记
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            TestLog.Log("松开前后操作键，终止位移");
            _cmdMove = UserMoveType.None;
            moveSpeedAddBase = 1f;
        }
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            TestLog.Log("松开左右操作键，终止旋转");
            _cmdRotate = UserMoveType.None;
            first = VecMax;
            second = VecMax;
        }

        //最后根据当前的指令决定怎么运动！
        DoMove(moveSpeed * getActionSpeed());//23点47分刚刚加入，远距离就改为较快的移动速度
        DORotate(rotateSpeeedUse);
         
        //刷新当前参数：
        paramStateTip = "当前视距:" + followCom.distance + ",当前视野高度:" + followCom.height + "\n";
        UITootMono.Instance.UpdateLog(paramStateTip + strGUITip);
    }

    /// <summary>
    /// 根据我当前距离的远近决定操控的速度
    /// </summary>
    /// <returns></returns>
    private float getActionSpeed()
    {
        return followCom.distance / FollowPlayer.MINDISTANCE;
    }

    /// <summary>
    /// 修改相机的距离
    /// </summary>
    /// <param name="subValue"></param>
    private void setCamereMoveFar(float subValue)
    {
        followCom.distance += subValue;
        followCom.distance = Mathf.Min(followCom.distance, FollowPlayer.MAXDISTANCE);
        followCom.distance = Mathf.Max(followCom.distance, FollowPlayer.MINDISTANCE);
    }

    /// <summary>
    /// 检查玩家执行运动
    /// </summary>
    /// <param name="moveSpeedValue"></param>
    private void DoMove(float moveSpeedValue)
    {
        //向前或后位移
        switch (_cmdMove)
        {
            case UserMoveType.MoveForward:
                {
                    //Logger.Log("执行运动");
                    //cubeRigibody.AddForce(Vector3.forward*10000, ForceMode.Acceleration);
                    tranCube.Translate(0, 0, moveSpeedValue * Time.deltaTime * moveSpeedAddBase);
                }
                break;
            case UserMoveType.MoveBack:
                {
                    tranCube.Translate(0, 0, -moveSpeedValue * Time.deltaTime * moveSpeedAddBase);
                }
                break;
            default:
                return;
        }
        //tranCube.Translate(nextMove);// 0, 0, moveSpeedValue * Time.deltaTime * moveSpeedAddBase);

        moveSpeedAddBase *= 1.01f;
        moveSpeedAddBase = Mathf.Min(moveSpeedAddBase, 1.8f);
    }

    private void DORotate(float rotateSpeedValue)
    {
        //向左或向右旋转
        switch (_cmdRotate)
        {
            case UserMoveType.MouseRotateLeft:
            case UserMoveType.RotateLeft:
                {
                    tranCube.Rotate(new Vector3(0, -rotateSpeedValue, 0));
                }
                break;
            case UserMoveType.MouseRotateRight:
            case UserMoveType.RotateRight:
                {
                    tranCube.Rotate(new Vector3(0, rotateSpeedValue, 0));
                }
                break;
        }
    }
}
