using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using UnityEngine;
using JetBrains.Annotations;

namespace SpecialRelativity
{
    public class Matrix44
    {
        // memory layout:
        //
        //                column no (=horiz)
        //               |  0   1   2   3
        //            ---+----------------
        //            0  | m00 m10 m20 m30
        // row no     1  | m01 m11 m21 m31
        // (=vert)    2  | m02 m12 m22 m32
        //            3  | m03 m13 m23 m33

        // Column 0
        public float m00;
        public float m10;
        public float m20;
        public float m30;

        // Column 1
        public float m01;
        public float m11;
        public float m21;
        public float m31;

        // Column 2
        public float m02;
        public float m12;
        public float m22;
        public float m32;

        // Column 3
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

        public float this[int row, int column]
        {
            get
            {
                return this[row + column * 4]; // row + col * 4 index all 0..15 elements of Matrix44 sequentially; just mind that you index by [row, col]
            }

            set
            {
                this[row + column * 4] = value; 
            }
        }
        
        // Access element at sequential index (0..15 inclusive)
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    //Vector4 indexed as [w, x, y, z] where w = ct 
                    //column 0
                    case 0: return m00;
                    case 1: return m10;
                    case 2: return m20;
                    case 3: return m30;

                    //column 1 
                    case 4: return m01;
                    case 5: return m11;
                    case 6: return m21;
                    case 7: return m31;

                    //column 2
                    case 8: return m02;
                    case 9: return m12;
                    case 10: return m22;
                    case 11: return m32;

                    //column 3
                    case 12: return m03;
                    case 13: return m13;
                    case 14: return m23;
                    case 15: return m33;
                    default: throw new IndexOutOfRangeException("Invalid matrix index! :3 sillyass");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: m00 = value; break;
                    case 1: m10 = value; break;
                    case 2: m20 = value; break;
                    case 3: m30 = value; break;
                    case 4: m01 = value; break;
                    case 5: m11 = value; break;
                    case 6: m21 = value; break;
                    case 7: m31 = value; break;
                    case 8: m02 = value; break;
                    case 9: m12 = value; break;
                    case 10: m22 = value; break;
                    case 11: m32 = value; break;
                    case 12: m03 = value; break;
                    case 13: m13 = value; break;
                    case 14: m23 = value; break;
                    case 15: m33 = value; break;
                    default: throw new IndexOutOfRangeException("Invalid matrix index! :3 sillyass");
                }
            }
        }

        // various methods in the matrix4x4 implementation in Unity 
        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Matrix4x4.cs
        
        // public override int GetHashCode()? (line 166)
        
        // public override bool Equals(object other)? (line 173)

        // public bool Equals(Matrix44 other) (line 181)

        // public static Matrix44 operator*(Matrix44 lhs, MAtrix44 rhs)? (line 190)

        // public static Vector4 operator*(Matrix44 lhs, Vector4 vector)? (line 217)

        // public static bool operator==(Matrix44 lhs, Matrix44 rhs)? (line 228)

        // public static bool operator!=(Matrix44 lhs, Matrix44 rhs)? (line 238)



        // Column get and set
        public Vector4 GetColumn(int index)
        {
            switch (index)
            {
                case 0: return new Vector4(m00, m10, m20, m30);
                case 1: return new Vector4(m01, m11, m21, m31);
                case 2: return new Vector4(m02, m12, m22, m32);
                case 3: return new Vector4(m03, m13, m23, m33);
                default: throw new IndexOutOfRangeException("Invalid column index! :3");
            }
        }
        
        // !!!! wxyz instead of default unity matrix4x4 which is xyzw for some godforsaken reason
        public void SetColumn(int index, Vector4 column)
        {
            this[0, index] = column.w;
            this[1, index] = column.x;
            this[2, index] = column.y;
            this[3, index] = column.w;
        }

        // row get and set
        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0: return new Vector4(m00, m01, m02, m03);
                case 1: return new Vector4(m10, m11, m12, m13);
                case 2: return new Vector4(m20, m21, m22, m23);
                case 3: return new Vector4(m30, m31, m32, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid row index! :3");
            }
        }

        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.w;
            this[index, 1] = row.x;
            this[index, 2] = row.y;
            this[index, 3] = row.z;
        }



        // use the metric eta to write the Lorentzian inner product conveniently
        // it is unrigorously a diagonal matrix where the first diagonal element is -1 and the rest are 1
        static readonly Matrix44 etaMetric = new Matrix44(
            new Vector4(-1, 0, 0, 0),
            new Vector4( 0, 1, 0, 0),
            new Vector4( 0, 0, 1, 0),
            new Vector4( 0, 0, 0, 1));
        public static Matrix44 eta { get { return etaMetric; } }







    }


}