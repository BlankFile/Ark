
namespace System
{
	/// <summary>
	/// 数値の拡張メソッドを定義します。
	/// </summary>
    public static partial class NumericT4Extension
    {
		#region SByte

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this sbyte value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this sbyte value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this sbyte value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static sbyte Half(this sbyte value)
        {
		    return Convert.ToSByte(value / 2);
        }

		#endregion

		#region Byte

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this byte value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this byte value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this byte value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static byte Half(this byte value)
        {
		    return Convert.ToByte(value / 2);
        }

		#endregion

		#region Short

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this short value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this short value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this short value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static short Half(this short value)
        {
		    return Convert.ToInt16(value / 2);
        }

		#endregion

		#region UShort

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this ushort value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this ushort value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this ushort value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static ushort Half(this ushort value)
        {
		    return Convert.ToUInt16(value / 2);
        }

		#endregion

		#region Int

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this int value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this int value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this int value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static int Half(this int value)
        {
		    return Convert.ToInt32(value / 2);
        }

		#endregion

		#region UInt

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this uint value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this uint value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this uint value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static uint Half(this uint value)
        {
		    return Convert.ToUInt32(value / 2);
        }

		#endregion

		#region Long

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this long value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this long value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this long value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static long Half(this long value)
        {
		    return Convert.ToInt64(value / 2);
        }

		#endregion

		#region ULong

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this ulong value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this ulong value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this ulong value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static ulong Half(this ulong value)
        {
		    return Convert.ToUInt64(value / 2);
        }

		#endregion

		#region Float

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this float value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this float value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this float value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static float Half(this float value)
        {
		    return Convert.ToSingle(value / 2);
        }

		#endregion

		#region Double

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this double value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this double value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this double value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static double Half(this double value)
        {
		    return Convert.ToDouble(value / 2);
        }

		#endregion

		#region Decimal

		/// <summary>
        /// 0かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：0　False：0でない</returns>
		public static bool IsZero(this decimal value)
        {
            return value == 0;
        }

        /// <summary>
        /// 正かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：正　False：0または負</returns>
        public static bool IsPlus(this decimal value)
        {
            return value > 0;
        }

        /// <summary>
        /// 負かどうか判断します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>True：負　False：0または正</returns>
        public static bool IsMinus(this decimal value)
        {
            return value < 0;
        }

        /// <summary>
        /// 1/2 の値を返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>1/2 の値</returns>
		public static decimal Half(this decimal value)
        {
		    return Convert.ToDecimal(value / 2);
        }

		#endregion

    }
}