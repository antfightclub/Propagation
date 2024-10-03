using UnityEngine;

namespace SpecialRelativity
{
    public class Matrix44
    {
        // memory layout:
        //
        //                row no (=vertical)
        //               |  0   1   2   3
        //            ---+----------------
        //            0  | m00 m10 m20 m30
        // column no  1  | m01 m11 m21 m31
        // (=horiz)   2  | m02 m12 m22 m32
        //            3  | m03 m13 m23 m33

        public float m00;
        public float m10;
        public float m20;
        public float m30;

        public float m01;
        public float m11;
        public float m21;
        public float m31;

        public float m02;
        public float m12;
        public float m22;
        public float m32;

        public float m03;
        public float m13;
        public float m23;
        public float m33;

        /// <summary>
        /// takes four Vector4 column vectors with indices w, x, y, z
        /// </summary>
        /// <param name="column0"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <param name="column3"></param>
        public Matrix44(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            this.m00 = column0.w;   this.m01 = column1.w;   this.m02 = column2.w;   this.m03 = column3.w;
            this.m10 = column0.x;   this.m11 = column1.x;   this.m12 = column2.x;   this.m13 = column3.x;
            this.m20 = column0.y;   this.m21 = column1.y;   this.m22 = column2.y;   this.m23 = column3.y;
            this.m20 = column0.z;   this.m31 = column1.z;   this.m32 = column2.z;   this.m33 = column3.z;
        }
        
        
        
        
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
