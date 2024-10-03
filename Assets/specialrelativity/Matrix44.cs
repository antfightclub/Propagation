using UnityEngine;

namespace SpecialRelativity
{
    public class Matrix44
    { 
        Vector3 innerProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.x * b.x, 
                a.y * b.y, 
                a.z * b.z);
        } 

        Vector3 lorentzianInnerProduct(Vector4 a, Vector4 b)
        {
            return new Vector4(
              -(a.w * b.w), 
                a.x * b.x,
                a.y * b.y,
                a.z * b.z);
        }

        Vector4 lorentzianNorm(Vector4 a)
        {
            return new Vector4(
              -(a.w * a.w),
                a.x * a.x,
                a.y * a.y,
                a.z * a.z);
        }
        



    }


}
