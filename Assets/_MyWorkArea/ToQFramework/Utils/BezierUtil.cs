using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public static class BezierUtils
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p0">���</param>
        /// <param name="p1">�յ�</param>
        /// <param name="t">��0-1��</param>
        /// <returns></returns>
        public static Vector3 BezierPoint(Vector3 p0, Vector3 p1, float t)
        {
            return (1 - t) * p0 + t * p1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 BezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 result = (1 - t) * p0p1 + t * p1p2;
            return result;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 BezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 result;
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 p2p3 = (1 - t) * p2 + t * p3;
            Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
            Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;
            result = (1 - t) * p0p1p2 + t * p1p2p3;
            return result;
        }

        /// <summary>
        /// �������  �����Եݹ� �ж���������ϣ�
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector3 BezierPoint(float t, List<Vector3> p)
        {
            if (p.Count < 2)
                return p[0];
            List<Vector3> newP = new List<Vector3>();
            for (int i = 0; i < p.Count - 1; i++)
            {
                Vector3 p0p1 = (1 - t) * p[i] + t * p[i + 1];
                newP.Add(p0p1);
            }
            return BezierPoint(t, newP);
        }

        /// <summary>
        /// ��ȡ�洢���������ߵ������(����)
        /// </summary>
        /// <param name="startPoint">��ʼ��</param>
        /// <param name="controlPoint">���Ƶ�</param>
        /// <param name="endPoint">Ŀ���</param>
        /// <param name="segmentNum">�����������</param>
        /// <returns>�洢���������ߵ������</returns>
        public static Vector3[] GetBeizerPointList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
        {
            Vector3[] path = new Vector3[segmentNum];
            for (int i = 1; i <= segmentNum; i++)
            {
                float t = i / (float)segmentNum;
                Vector3 pixel = BezierPoint(startPoint, controlPoint, endPoint, t);
                path[i - 1] = pixel;
            }
            return path;
        }

        /// <summary>
        /// ��ȡ�洢���������ߵ������(���)
        /// </summary>
        /// <param name="segmentNum">�����������</param>
        /// <param name="p">���Ƶ㼯��</param>
        /// <returns></returns>
        public static Vector3[] GetBeizerPointList(int segmentNum, List<Vector3> p)
        {
            Vector3[] path = new Vector3[segmentNum];
            for (int i = 1; i <= segmentNum; i++)
            {
                float t = i / (float)segmentNum;
                Vector3 pixel = BezierPoint(t, p);
                path[i - 1] = pixel;
            }
            return path;
        }

    }

}