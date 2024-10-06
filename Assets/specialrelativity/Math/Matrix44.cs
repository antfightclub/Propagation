using System;
using UnityEngine;


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
        public double m00;
        public double m10;
        public double m20;
        public double m30;

        // Column 1
        public double m01;
        public double m11;
        public double m21;
        public double m31;

        // Column 2
        public double m02;
        public double m12;
        public double m22;
        public double m32;

        // Column 3
        public double m03;
        public double m13;
        public double m23;
        public double m33;

        /// <summary>
        /// takes four Vector4D column vectors with indices t, x, y, z
        /// </summary>
        /// <param name="column0"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <param name="column3"></param>
        public Matrix44(Vector4D column0, Vector4D column1, Vector4D column2, Vector4D column3)
        {
            this.m00 = column0.t;   this.m01 = column1.t;   this.m02 = column2.t;   this.m03 = column3.t;
            this.m10 = column0.x;   this.m11 = column1.x;   this.m12 = column2.x;   this.m13 = column3.x;
            this.m20 = column0.y;   this.m21 = column1.y;   this.m22 = column2.y;   this.m23 = column3.y;
            this.m30 = column0.z;   this.m31 = column1.z;   this.m32 = column2.z;   this.m33 = column3.z;
        }
        
        /// <summary>
        /// If no arguments are given, return zero matrix, otherwise takes four Vector4D column vectors
        /// </summary>
        public Matrix44()
        {
            this.m00 = 0.0d;    this.m01 = 0.0d;    this.m02 = 0.0d;    this.m03 = 0.0d;
            this.m10 = 0.0d;    this.m11 = 0.0d;    this.m12 = 0.0d;    this.m13 = 0.0d;
            this.m20 = 0.0d;    this.m21 = 0.0d;    this.m22 = 0.0d;    this.m23 = 0.0d;
            this.m30 = 0.0d;    this.m31 = 0.0d;    this.m32 = 0.0d;    this.m33 = 0.0d;
        }

        public double this[int row, int column]
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
        
        /// <summary>
        /// Access element at sequential index (0..15 inclusive)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    //Vector4 indexed as [t, x, y, z] where t = ct (light seconds)
                    //column 0
                    case 0:  return m00;
                    case 1:  return m10;
                    case 2:  return m20;
                    case 3:  return m30;

                    //column 1 
                    case 4:  return m01;
                    case 5:  return m11;
                    case 6:  return m21;
                    case 7:  return m31;

                    //column 2
                    case 8:  return m02;
                    case 9:  return m12;
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
                    case 0:  m00 = value; break;
                    case 1:  m10 = value; break;
                    case 2:  m20 = value; break;
                    case 3:  m30 = value; break;
                    case 4:  m01 = value; break;
                    case 5:  m11 = value; break;
                    case 6:  m21 = value; break;
                    case 7:  m31 = value; break;
                    case 8:  m02 = value; break;
                    case 9:  m12 = value; break;
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
        /// <summary>
        /// Returns column vector of index in {0, 1, 2, 3}
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Vector4D GetColumn(int index)
        {
            switch (index)
            {
                case 0: return new Vector4D(m00, m10, m20, m30);
                case 1: return new Vector4D(m01, m11, m21, m31);
                case 2: return new Vector4D(m02, m12, m22, m32);
                case 3: return new Vector4D(m03, m13, m23, m33);
                default: throw new IndexOutOfRangeException("Invalid column index! :3");
            }
        }
        
        // !!!! txyz instead of default unity matrix4x4 which is xyzw for some godforsaken reason
        /// <summary>
        /// Sets the column vector of index in {0, 1, 2, 3}
        /// </summary>
        /// <param name="index"></param>
        /// <param name="column"></param>
        public void SetColumn(int index, Vector4D column)
        {
            this[0, index] = column.t;
            this[1, index] = column.x;
            this[2, index] = column.y;
            this[3, index] = column.z;
        }

        // row get and set
        /// <summary>
        /// Returns the row of index in {0, 1, 2, 3} as a vector4D
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Vector4D GetRow(int index)
        {
            switch (index)
            {
                case 0: return new Vector4D(m00, m01, m02, m03);
                case 1: return new Vector4D(m10, m11, m12, m13);
                case 2: return new Vector4D(m20, m21, m22, m23);
                case 3: return new Vector4D(m30, m31, m32, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid row index! :3");
            }
        }

        /// <summary>
        /// Sets the row of index in {0, 1, 2, 3} with values in a Vector4D
        /// </summary>
        /// <param name="index"></param>
        /// <param name="row"></param>
        public void SetRow(int index, Vector4D row)
        {
            this[index, 0] = row.t;
            this[index, 1] = row.x;
            this[index, 2] = row.y;
            this[index, 3] = row.z;
        }

        /// <summary>
        /// Let M be the spatial part of Matrix44. This method rotates M with angle in the X axis.
        /// Angle is given in radians.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>Matrix44 where the spatial part is rotated by angle in the X axis</returns>
        public Matrix44 XRotation(double angle)
        {
            double cos_a = Math.Cos(angle);
            double sin_a = Math.Sin(angle);
            Matrix44 m = new Matrix44();
            m.m00 = 1.0d; m.m01 = 0.0d; m.m02 = 0.0d; m.m03 = 0.0d;
            m.m10 = 0.0d; m.m11 = 1.0d; m.m12 = 0.0d; m.m13 = 0.0d;
            m.m20 = 0.0d; m.m21 = 0.0d; m.m22 = cos_a; m.m23 = -sin_a;
            m.m30 = 0.0d; m.m31 = 0.0d; m.m32 = sin_a; m.m33 = cos_a;
            return m;
        }

        /// <summary>
        /// Let M be the spatial part of Matrix44. This method rotates M with angle in the Y axis.
        /// Angle is given in radians.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>Matrix44 where the spatial part is rotated by angle in the Y axis</returns>
        public Matrix44 YRotation(double angle)
        {
            double cos_a = Math.Cos(angle);
            double sin_a = Math.Sin(angle);
            Matrix44 m = new Matrix44();
            m.m00 = 1.0d; m.m01 = 0.0d; m.m02 = 0.0d; m.m03 = 0.0d;
            m.m10 = 0.0d; m.m11 = cos_a; m.m12 = 0.0d; m.m13 = sin_a;
            m.m20 = 0.0d; m.m21 = 0.0d; m.m22 = 1.0d; m.m23 = 0.0d;
            m.m30 = 0.0d; m.m31 = -sin_a; m.m32 = 0.0d; m.m33 = cos_a;
            return m;
        }

        /// <summary>
        /// Let M be the spatial part of Matrix44. This method rotates M with angle in the Z axis.
        /// Angle is given in radians.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>Matrix44 where the spatial part is rotated by angle in the Z axis</returns>
        public Matrix44 ZRotation(double angle)
        {
            double cos_a = Math.Cos(angle);
            double sin_a = Math.Cos(angle);
            Matrix44 m = new Matrix44();
            m.m00 = 1.0d; m.m01 = 0.0d; m.m02 = 0.0d; m.m03 = 0.0d;
            m.m10 = 0.0d; m.m11 = cos_a; m.m12 = -sin_a; m.m13 = 0.0d;
            m.m20 = 0.0d; m.m21 = sin_a; m.m22 = cos_a; m.m23 = 0.0d;
            m.m30 = 0.0d; m.m31 = 0.0d; m.m32 = 0.0d; m.m33 = 1.0d;
            return m;
        }

        /// <summary>
        /// Let M be the spatial part of Matrix44. This method returns a scaling matrix, leaving the first diagonal (t component) as 1.0d.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns>Diagonal Matrix44 where the 0th diagonal is 1.0d, and the diagonals 1..3 are set to scale </returns>
        public Matrix44 Scale(double scale)
        {
            Matrix44 m = new Matrix44();
            m.m00 = 1.0d; m.m01 =  0.0d; m.m02 =  0.0d; m.m03 =  0.0d;
            m.m10 = 0.0d; m.m11 = scale; m.m12 =  0.0d; m.m13 =  0.0d;
            m.m20 = 0.0d; m.m21 =  0.0d; m.m22 = scale; m.m23 =  0.0d;
            m.m30 = 0.0d; m.m31 =  0.0d; m.m32 =  0.0d; m.m33 = scale;
            return m;
        }

        /// <summary>
        /// Multiply two Matrix44 a and b. Note that b is transposed correctly
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix44 operator *(Matrix44 a, Matrix44 b)
        {
            Matrix44 m = new Matrix44();

            m.m00 =    a.m00 * b.m00   +   a.m01 * b.m10   +   a.m02 * b.m20   +   a.m03 * b.m30;
            m.m10 =    a.m00 * b.m01   +   a.m01 * b.m11   +   a.m02 * b.m21   +   a.m03 * b.m31;
            m.m20 =    a.m00 * b.m02   +   a.m01 * b.m12   +   a.m02 * b.m22   +   a.m03 * b.m32;
            m.m30 =    a.m00 * b.m03   +   a.m01 * b.m13   +   a.m02 * b.m23   +   a.m03 * b.m33;
                          
            m.m01 =    a.m10 * b.m00   +   a.m11 * b.m10   +   a.m12 * b.m20   +   a.m13 * b.m30;
            m.m11 =    a.m10 * b.m01   +   a.m11 * b.m11   +   a.m12 * b.m21   +   a.m13 * b.m31;
            m.m21 =    a.m10 * b.m02   +   a.m11 * b.m12   +   a.m12 * b.m22   +   a.m13 * b.m32;
            m.m31 =    a.m10 * b.m03   +   a.m11 * b.m13   +   a.m12 * b.m23   +   a.m13 * b.m33;

            m.m02 =    a.m20 * b.m00   +   a.m21 * b.m10   +   a.m22 * b.m20   +   a.m23 * b.m30;
            m.m12 =    a.m20 * b.m01   +   a.m21 * b.m11   +   a.m22 * b.m21   +   a.m23 * b.m31;
            m.m22 =    a.m20 * b.m02   +   a.m21 * b.m12   +   a.m22 * b.m22   +   a.m23 * b.m32;
            m.m32 =    a.m20 * b.m03   +   a.m21 * b.m13   +   a.m22 * b.m23   +   a.m23 * b.m33;
                          
            m.m03 =    a.m30 * b.m00   +   a.m31 * b.m10   +   a.m32 * b.m20   +   a.m33 * b.m30;
            m.m13 =    a.m30 * b.m01   +   a.m31 * b.m11   +   a.m32 * b.m21   +   a.m33 * b.m31;
            m.m23 =    a.m30 * b.m02   +   a.m31 * b.m12   +   a.m32 * b.m22   +   a.m33 * b.m32;
            m.m33 =    a.m30 * b.m03   +   a.m31 * b.m13   +   a.m32 * b.m23   +   a.m33 * b.m33;

            return m;
        }

        /// <summary>
        /// Returns the inverse of a Matrix44 that is a 3D rotation by transposing its 3D rotation part
        /// </summary>
        /// <returns></returns>
        public Matrix44 GetInverseRot()
        {
            Matrix44 m = new Matrix44();
            m.m00 = this.m00;   m.m01 = this.m01;   m.m02 = this.m02;   m.m03 = this.m03;
            m.m10 = this.m10;   m.m11 = this.m11;   m.m12 = this.m21;   m.m13 = this.m31;
            m.m20 = this.m20;   m.m21 = this.m12;   m.m22 = this.m22;   m.m23 = this.m32;
            m.m30 = this.m30;   m.m31 = this.m13;   m.m23 = this.m23;   m.m33 = this.m33;
            return m;
        }

        /// <summary>
        /// Let M bet the 3D rotation part of self. This method returns M*v 
        /// </summary>
        /// <returns></returns>
        public Vector3D GetRotate(Vector3D v)
        {
            double xx, yy, zz;
            xx = this.m11 * v.x + this.m12 * v.y + this.m13 * v.z;
            yy = this.m21 * v.x + this.m22 * v.y + this.m23 * v.z;
            zz = this.m31 * v.x + this.m32 * v.y + this.m33 * v.z;
            return new Vector3D(xx, yy, zz);
        }

        public Vector4D GetTransform(Vector4D v)
        {
            double t, x, y, z, tt, xx, yy, zz;
            t = v.t; x = v.x; y = v.y; z = v.z;
            tt = this.m00 * t + this.m01 * x + this.m02 * y + this.m03 * z;
            xx = this.m10 * t + this.m11 * x + this.m12 * y + this.m13 * z;
            yy = this.m20 * t + this.m21 * x + this.m22 * y + this.m23 * z;
            zz = this.m30 * t + this.m31 * x + this.m32 * y + this.m33 * z;
            return new Vector4D(tt, xx, yy, zz);
        }


        static readonly Matrix44 zeroMatrix = new Matrix44(
            new Vector4D(0, 0, 0, 0),
            new Vector4D(0, 0, 0, 0),
            new Vector4D(0, 0, 0, 0),
            new Vector4D(0, 0, 0, 0));

        public static Matrix44 Zero { get { return zeroMatrix; } }


        // use the metric eta to write the Lorentzian inner product conveniently
        // it is unrigorously a diagonal matrix where the first diagonal element is -1 and the rest are 1
        // .... Do I need to define Vector4 using explicitly 0f???
        static readonly Matrix44 etaMetric = new Matrix44(
            new Vector4D(-1, 0, 0, 0),
            new Vector4D( 0, 1, 0, 0),
            new Vector4D( 0, 0, 1, 0),
            new Vector4D( 0, 0, 0, 1));

        // Returns the eta metric as described en Sogebu et al (https://arxiv.org/pdf/1703.07063)
        public static Matrix44 Eta { get { return etaMetric; } }


        static readonly Matrix44 identityMatrix = new Matrix44(
            new Vector4D(1, 0, 0, 0),
            new Vector4D(0, 1, 0, 0),
            new Vector4D(0, 0, 1, 0),
            new Vector4D(0, 0, 0, 1));

        // Returns the ordinary 4-dimensional identity matrix 
        public static Matrix44 Identity { get { return identityMatrix; } }

        // Implement multiplying Matrix44 by Matrix44
        // Implement multiplying Vector4 by Matrix44
        // and variations depending on need
        // Implement multiplying a Vector4 by a float value


        /// <summary>
        /// This method returns the lorentz transform of a Vector4D
        /// </summary>
        /// <param name="u"></param>
        /// <returns>Matrix44</returns>
        public Matrix44 Lorentz(Vector4D u)
        {
            Matrix44 m = Matrix44.Zero;
            double x, y, z, x2, y2, z2, r, g, xy, yz, zx;
            x = u.x; y = u.y; z = u.z;
            x2 = x * x; y2 = y * y; z2 = z * z;
            r = x2 + y2 + z2;

            if (r > 0.0) 
            {
                g = Math.Sqrt(1.0d + r);
                r = 1.0d / r;
                xy = (g - 1.0d) * x * y * r;
                yz = (g - 1.0d) * y * z * r;
                zx = (g - 1.0d) * z * x * r;
                m.m00 =  g;     m.m01 =                     -x;     m.m02 =                     -y;     m.m03 =                     -z;
                m.m10 = -x;     m.m11 = (g * x2 + y2 + z2) * r;     m.m12 =                     xy;     m.m13 =                     zx;
                m.m20 = -y;     m.m21 =                     xy;     m.m22 = (x2 + g * y2 + z2) * r;     m.m23 =                     yz;
                m.m30 = -z;     m.m31 =                     zx;     m.m32 =                     yz;     m.m33 = (x2 + y2 + g * z2) * r;
            }

            else
            {
                m.m00 = 1.0d;   m.m01 = 0.0d;   m.m02 = 0.0d;   m.m03 = 0.0d;
                m.m10 = 0.0d;   m.m11 = 1.0d;   m.m12 = 0.0d;   m.m13 = 0.0d;
                m.m20 = 0.0d;   m.m12 = 0.0d;   m.m22 = 1.0d;   m.m23 = 0.0d;
                m.m30 = 0.0d;   m.m13 = 0.0d;   m.m32 = 0.0d;   m.m33 = 1.0d;
            }
            return m;
        }

    }

}
