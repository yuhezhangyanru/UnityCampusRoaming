using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于处理显示UI相关
/// </summary>
public class UITootMono : MonoBehaviour
{
    public Text tip_choose_start_tip;
    public GameObject optionRoot;

    public GameObject LightObject;
    public Text txt_tip_mode;
    public Text txt_tip_light;

    public Button btn_mode_walk;
    public Button btn_mode_light;

    public Button btn_start;

    public Button btn_pos_a;
    public Button btn_pos_b;
    public Button btn_pos_c;
    public Button btn_pos_d;

    public static UITootMono Instance = null;
    public RectTransform rect_info;
    public Text txt_tip;

    private bool _isShowMenu = true;

    public bool isWalk = true;
    public bool isDay = true;

    public Vector3 startPos = new Vector3();
    public Vector3 startAngle = new Vector3();

    // Use this for initialization
    void Awake()
    {
        optionRoot.SetActive(true);
        Instance = this;

        UpdateMenuState();
        UpdateLog("");
        btn_mode_light.onClick.AddListener(() =>
        {
            isDay = !isDay;
            UpdateLightMode();
        });
        btn_mode_walk.onClick.AddListener(() =>
        {
            isWalk = !isWalk;
            UpdateWalkMoode();
        });
        UpdateWalkMoode();
        UpdateLightMode();

        startPos = new Vector3(-115, 8, 38);
        startAngle = new Vector3(0, 132, 0);
        tip_choose_start_tip.text = "当前的选择是A";
        btn_pos_a.onClick.AddListener(() =>
        {
            TestLog.Log("选择第1个出发点");
            tip_choose_start_tip.text = "当前的选择是A";
            startPos = new Vector3(-115, 8, 38);
            startAngle = new Vector3(0, 132, 0);
        });
        btn_pos_b.onClick.AddListener(() =>
        {
            TestLog.Log("选择第2个出发点");
            tip_choose_start_tip.text = "当前的选择是B";
            startPos = new Vector3(-110,8,-9);
            startAngle = new Vector3(0, 123, 0);

        });
        btn_pos_c.onClick.AddListener(() =>
        {
            TestLog.Log("选择第3个出发点");
            tip_choose_start_tip.text = "当前的选择是C";
            startPos = new Vector3(-11, 7, -224);
            startAngle = new Vector3(0,-7,0);
        });
        btn_pos_d.onClick.AddListener(() =>
        {
            TestLog.Log("选择第4个出发点");
            tip_choose_start_tip.text = "当前的选择是D";
            startPos = new Vector3(56, 7, -194);
            startAngle = new Vector3(0, -23, 0);
        });

        btn_start.onClick.AddListener(()=>{

            optionRoot.SetActive(false);
            MainCamera.instance.tranCube.eulerAngles = startAngle;
            MainCamera.instance.tranCube.position = startPos;

            if (isWalk)
            {
                MainCamera.instance.followCom.distance = FollowPlayer.MINDISTANCE;
                MainCamera.instance.followCom.height = FollowPlayer.MINHEIGHT;
                MainCamera.instance.cubeRigibody.isKinematic = false;
            }
            else
            {

                MainCamera.instance.followCom.distance = FollowPlayer.MAXDISTANCE;
                MainCamera.instance.followCom.height = 40;
                MainCamera.instance.cubeRigibody.isKinematic = true;
            }
        });
    }

    private void UpdateMenuState()
    {
        rect_info.gameObject.SetActive(_isShowMenu);
    }

    public void UpdateWalkMoode()
    {
        TestLog.Log("更新步行方式 isWalk=" + isWalk);
        txt_tip_mode.text = "切换行走模式，当前为：" + (isWalk ? "步行模式" : "漫游模式");
        btn_mode_walk.transform.Find("text").GetComponent<Text>().text = (!isWalk ? "步行模式" : "漫游模式");
    }

    public void UpdateLightMode()
    {
        LightObject.SetActive(isDay);
        TestLog.Log("更新白天还是黑夜方式 isDay=" + isDay);
        txt_tip_light.text = "切换昼夜模式，当前为：" + ( isDay ? "白天模式" : "夜晚模式"); 
        btn_mode_light.transform.Find("text").GetComponent<Text>().text = (!isDay ? "白天模式" : "夜晚模式");
    } 

    public void UpdateLog(string log)
    {
        txt_tip.text = log;
    }
}
