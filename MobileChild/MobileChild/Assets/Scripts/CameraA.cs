using System;
using UnityEngine;

public class CameraA : MonoBehaviour
{

    //�����Ŀ��
    public Transform followTarget;
    //��������
    private Vector3 dir;
    //������ײ�����
    private RaycastHit hit;
    //������ƶ��ٶ�
    public float moveSpeed;
    //�������ת�ٶ�
    public float turnSpeed;
    //������۲�ĵ�λ����ѡ���ӽ�λ�õĸ�����
    public const int camera_watch_gear = 5;
    //�۲��������ƫ����
    public const float PLAYER_WATCHBODY_OFFSET = 1f;

    private void Start()
    {
        //���㷽�������������ָ����ҡ�
        dir = followTarget.position - transform.position;
    }

    private void Update()
    {
        FollowMethod();
    }

    /// <summary>
    /// �����㷨
    /// </summary>
    private void FollowMethod()
    {
        //ʱʱ�̼̿���������ĸ�������λ��
        Vector3 bestWatchPos = followTarget.position - dir;
        //�������Ŀ��ͷ���ĸ���λ�á����á������Ա�֤������ҡ�
        Vector3 badWatchPos = followTarget.position + Vector3.up *
        (dir.magnitude);
        //�������й۲������顾���鳤�Ⱦ�Ϊ��λ������
        Vector3[] watchPoints = new Vector3[camera_watch_gear];
        //�����������ʼ��
        watchPoints[0] = bestWatchPos;
        watchPoints[watchPoints.Length - 1] = badWatchPos;

        for (int i = 1; i <= watchPoints.Length - 2; i++)
        {
            //�����м�۲�������
            watchPoints[i] = Vector3.Lerp(bestWatchPos, badWatchPos,
            (float)i / (camera_watch_gear - 1));
        }

        //��������ʵĹ۲�㡾��ֵ������Ĺ۲�㡿
        Vector3 suitablePos = bestWatchPos;
        //�������еĹ۲��
        for (int i = 0; i < watchPoints.Length; i++)
        {
            //���õ��Ƿ���Կ������
            if (CanSeeTarget(watchPoints[i]))
            {
                //ѡ������ʵĵ�
                suitablePos = watchPoints[i];
                //����ѭ��
                break;
            }
        }
        //��ֵ�ƶ������ʵ�λ��
        transform.position = Vector3.Lerp(transform.position,
        suitablePos, Time.deltaTime * moveSpeed);

        //����õ�ָ����ҵķ�������
        Vector3 crtDir = followTarget.position +
        Vector3.up * PLAYER_WATCHBODY_OFFSET
        - suitablePos;
        //����������ת����Ԫ��
        Quaternion targetQua = Quaternion.LookRotation(crtDir);
        //Lerp��ȥ
        transform.rotation = Quaternion.Lerp(transform.rotation,
        targetQua, Time.deltaTime * turnSpeed);
        //ŷ��������
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
    }

    /// <summary>
    /// ���õ���Կ������
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool CanSeeTarget(Vector3 pos)
    {
        //�����ʱ�ķ�������
        Vector3 crtDir = followTarget.position +
        Vector3.up * PLAYER_WATCHBODY_OFFSET - pos;
        //������������
        if (Physics.Raycast(pos, crtDir, out hit))
        {
            //���ߴ򵽵Ķ�������ң�˵���õ���Կ������
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}