using UnityEngine;
using System;

namespace Extensions
{
    /// <summary>
    /// Extension methods to compare equality of double types within 3..8 digits of precision
    /// </summary>
    public static class DoubleExtensions
    {
        const double _3 = 0.001;
        const double _4 = 0.0001;
        const double _5 = 0.00001;
        const double _6 = 0.000001;
        const double _7 = 0.0000001;
        const double _8 = 0.00000001;

        /// <summary>
        /// Compare whether left is equal to right to within 3 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals3DigitPrecision(this double left, double right) => Math.Abs(left - right) < _3;

        /// <summary>
        /// Compare whether left is equal to right to within 4 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals4DigitPrecision(this double left, double right) => Math.Abs(left - right) < _4;

        /// <summary>
        /// Compare whether left is equal to right to within 5 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals5DigitPrecision(this double left, double right) => Math.Abs(left - right) < _5;

        /// <summary>
        /// Compare whether left is equal to right to within 6 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals6DigitPrecision(this double left, double right) => Math.Abs(left - right) < _6;

        /// <summary>
        /// Compare whether left is equal to right to within 7 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals7DigitPrecision(this double left, double right) => Math.Abs(left - right) < _7;

        /// <summary>
        /// Compare whether left is equal to right to within 8 digits of precision
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>bool; true if numbers are equal within precision</returns>
        public static bool Equals8DigitPrecision(this double left, double right) => Math.Abs(left - right) < _8;

        
    }

}