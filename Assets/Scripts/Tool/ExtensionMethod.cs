using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    private const float dotThreshold = 0.5f;
    /// <summary>
    /// 对现有的类进行扩展，判断自己位置前方范围为dotThreshold的范围是否有目标
    /// </summary>
    /// <param name="transform">需要扩张的类，使用时无需写</param>
    /// <param name="target">目标位置</param>
    /// <returns></returns>
    public static bool IsFacingTarget(this Transform transform, Transform target)
    {
        var vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();

        float dot = Vector3.Dot(transform.forward, vectorToTarget);

        return dot >= dotThreshold;
    }
}
