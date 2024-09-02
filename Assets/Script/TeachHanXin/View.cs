using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] backgrounds; // ���������Ҫ�Ӳ�����ı�����
    public float smoothing = 1f;    // �Ӳ�Ч����ƽ���ȡ���ֵԽ�󣬱����ƶ�Խƽ��

    private float[] parallaxScales; // ��¼ÿ�����������������ƶ��ı���
    private Transform cam;          // ���������transform���
    private Vector3 previousCamPos; // ��һ֡�������λ��

    void Awake()
    {
        // ���������������
        cam = Camera.main.transform;
    }

    void Start()
    {
        // ��¼��һ֡�������λ��
        previousCamPos = cam.position;

        // ��ֵÿ��������parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        // ��ÿ�����������Ӳ�����Ĵ���
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // �Ӳ���������ƶ��ķ�������Ա���������ٶ�
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // ����һ��Ŀ���xλ�ã��ǵ�ǰλ�ü����Ӳ�
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // ����һ��Ŀ��λ�ã��Ǳ�����ǰ��λ����Ŀ��xλ�õ����Բ�ֵ
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // ʹ��Lerpƽ�����ɵ�Ŀ��λ��
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // ������һ֡�������λ��
        previousCamPos = cam.position;
    }
}
