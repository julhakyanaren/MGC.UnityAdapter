using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides utilities for converting between different time units
    /// and for decomposing time durations expressed in seconds.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and follows Unity conventions:
    /// </para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       All time values are <see cref="System.Single"/>-based.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Seconds are treated as the canonical internal unit.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Responsibilities:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Convert time values between milliseconds, seconds, minutes, and hours.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Provide extension methods for common conversions.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Decompose seconds into structured components for UI or gameplay use.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Notes:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Zero and negative values are allowed.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <c>NaN</c> and <c>Infinity</c> are handled with warnings, not exceptions.
    ///     </description>
    ///   </item>
    /// </list>
    /// </summary>
    public static class TimeConverter
    {
        /// <summary>
        /// Supported time units for conversion.
        /// </summary>
        public enum TimeUnit
        {
            /// <summary>
            /// Milliseconds.
            /// </summary>
            ms,

            /// <summary>
            /// Seconds (Unity standard).
            /// </summary>
            s,

            /// <summary>
            /// Minutes (60 seconds).
            /// </summary>
            min,

            /// <summary>
            /// Hours (3600 seconds).
            /// </summary>
            hour,
        }
        /// <summary>
        /// Specifies how a time duration should be decomposed into components.
        /// </summary>
        public enum TimeDecomposeType
        {
            /// <summary>
            /// Hours, minutes, seconds.
            /// </summary>
            HourMinSec,

            /// <summary>
            /// Minutes and seconds.
            /// </summary>
            MinSec
        }

        /// <summary>
        /// Converts a time value from one unit to another.
        ///
        /// Internally, the conversion is performed via seconds:
        /// source unit -> seconds -> target unit.
        /// </summary>
        /// <param name="value">Input time value.</param>
        /// <param name="from">Source time unit.</param>
        /// <param name="to">Target time unit.</param>
        /// <returns>Converted time value expressed in the target unit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an unsupported unit is provided.
        /// </exception>
        public static float TimeConvert(float value, TimeUnit from, TimeUnit to)
        {
            if (from == to)
            {
                return value;
            }

            float seconds = 0f;

            switch (from)
            {
                case TimeUnit.ms:
                    {
                        seconds = value / 1000f;
                        break;
                    }
                case TimeUnit.s:
                    {
                        seconds = value;
                        break;
                    }
                case TimeUnit.min:
                    {
                        seconds = value * 60f;
                        break;
                    }
                case TimeUnit.hour:
                    {
                        seconds = value * 3600f;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{from} is incorrect unit. Method: TimeConvert");
                        throw new ArgumentOutOfRangeException(nameof(from));
                    }
            }

            float result = 0f;

            switch (to)
            {
                case TimeUnit.ms:
                    {
                        result = seconds * 1000f;
                        break;
                    }
                case TimeUnit.s:
                    {
                        result = seconds;
                        break;
                    }
                case TimeUnit.min:
                    {
                        result = seconds / 60f;
                        break;
                    }
                case TimeUnit.hour:
                    {
                        result = seconds / 3600f;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{to} is incorrect unit. Method: TimeConvert");
                        throw new ArgumentOutOfRangeException(nameof(to));
                    }
            }

            if (float.IsNaN(result) || float.IsInfinity(result))
            {
                Debug.LogWarning(
                    $"TimeConverter: conversion produced {result} (value={value}, from={from}, to={to}).");
            }

            if (value != 0f && result == 0f)
            {
                Debug.LogWarning(
                    $"TimeConverter: possible underflow (value={value}, from={from}, to={to}) -> result=0.");
            }

            return result;
        }

        /// <summary>
        /// Converts milliseconds to seconds.
        /// </summary>
        public static float Ms2S(this float value)
        {
            return TimeConvert(value, TimeUnit.ms, TimeUnit.s);
        }
        /// <summary>
        /// Converts milliseconds to minutes.
        /// </summary>
        public static float Ms2Min(this float value)
        {
            return TimeConvert(value, TimeUnit.ms, TimeUnit.min);
        }
        /// <summary>
        /// Converts milliseconds to hours.
        /// </summary>
        public static float Ms2Hour(this float value)
        {
            return TimeConvert(value, TimeUnit.ms, TimeUnit.hour);
        }

        /// <summary>
        /// Converts seconds to milliseconds.
        /// </summary>
        public static float S2Ms(this float value)
        {
            return TimeConvert(value, TimeUnit.s, TimeUnit.ms);
        }
        /// <summary>
        /// Converts seconds to minutes.
        /// </summary>
        public static float S2Min(this float value)
        {
            return TimeConvert(value, TimeUnit.s, TimeUnit.min);
        }
        /// <summary>
        /// Converts seconds to hours.
        /// </summary>
        public static float S2Hour(this float value)
        {
            return TimeConvert(value, TimeUnit.s, TimeUnit.hour);
        }

        /// <summary>
        /// Converts minutes to milliseconds.
        /// </summary>
        public static float Min2Ms(this float value)
        {
            return TimeConvert(value, TimeUnit.min, TimeUnit.ms);
        }
        /// <summary>
        /// Converts minutes to seconds.
        /// </summary>
        public static float Min2S(this float value)
        {
            return TimeConvert(value, TimeUnit.min, TimeUnit.s);
        }
        /// <summary>
        /// Converts minutes to hours.
        /// </summary>
        public static float Min2Hour(this float value)
        {
            return TimeConvert(value, TimeUnit.min, TimeUnit.hour);
        }

        /// <summary>
        /// Converts hours to milliseconds.
        /// </summary>
        public static float Hour2Ms(this float value)
        {
            return TimeConvert(value, TimeUnit.hour, TimeUnit.ms);
        }
        /// <summary>
        /// Converts hours to seconds.
        /// </summary>
        public static float Hour2S(this float value)
        {
            return TimeConvert(value, TimeUnit.hour, TimeUnit.s);
        }
        /// <summary>
        /// Converts hours to minutes.
        /// </summary>
        public static float Hour2Min(this float value)
        {
            return TimeConvert(value, TimeUnit.hour, TimeUnit.min);
        }
    }
}
