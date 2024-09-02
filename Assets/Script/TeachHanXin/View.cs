using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] backgrounds; // 存放所有需要视差滚动的背景层
    public float smoothing = 1f;    // 视差效果的平滑度。数值越大，背景移动越平滑

    private float[] parallaxScales; // 记录每个背景相对于摄像机移动的比例
    private Transform cam;          // 主摄像机的transform组件
    private Vector3 previousCamPos; // 上一帧摄像机的位置

    void Awake()
    {
        // 设置摄像机的引用
        cam = Camera.main.transform;
    }

    void Start()
    {
        // 记录上一帧摄像机的位置
        previousCamPos = cam.position;

        // 赋值每个背景的parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        // 对每个背景进行视差滚动的处理
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // 视差是摄像机移动的反方向乘以背景的相对速度
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // 设置一个目标的x位置，是当前位置加上视差
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // 创建一个目标位置，是背景当前的位置与目标x位置的线性插值
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // 使用Lerp平滑过渡到目标位置
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // 设置上一帧的摄像机位置
        previousCamPos = cam.position;
    }
}
