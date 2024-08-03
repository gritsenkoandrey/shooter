using UnityEngine;

namespace Game.Utils
{
    public static class Mathematics
    {
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
        {
            return Mathf.Lerp(oMin, oMax, Mathf.InverseLerp(iMin, iMax, value));
        }
    }
}