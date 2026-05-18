using UnityEngine;

namespace Utils
{
    public static class MathOperator
    {
        public static float GetIntersectionTime(
            float interceptorSpeed,
            Vector3 interceptorPosition,
            Vector3 targetVelocity,
            Vector3 targetPosition)
        {
            float a = (interceptorSpeed * interceptorSpeed) - targetVelocity.sqrMagnitude;

            Vector3 distance = interceptorPosition - targetPosition;
            float b = 2 * targetVelocity.magnitude * distance.magnitude
                * Mathf.Cos(Vector3.Angle(distance, targetVelocity));

            float c = -distance.sqrMagnitude;

            float d = (b * b) - (4 * a * c);

            if (d < 0f || a == 0f)
            {
                return 0f;
            }

            float sqrtD = Mathf.Sqrt(d);
            float time1 = (-b + sqrtD) / (2 * a);
            float time2 = (-b - sqrtD) / (2 * a);

            if (time1 < 0.0001f)
            {
                if (time2 < 0.0001f)
                {
                    return 0f;
                }
                else
                {
                    return time2;
                }
            }
            else if (time2 < 0.0001f)
            {
                return time1;
            }
            else if (time1 < time2)
            {
                return time2;
            }
            else
            {
                return time1;
            }
        }
    }
}
