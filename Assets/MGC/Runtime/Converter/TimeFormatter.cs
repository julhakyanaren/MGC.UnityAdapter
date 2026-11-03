using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides utilities for decomposing a time duration expressed in seconds
    /// into human-readable components such as hours, minutes, and seconds.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and is intended primarily for:
    /// </para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       UI display (timers, countdowns, clocks)
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Debug output and logging
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Simple gameplay logic where structured time components are required
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Design notes:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       The input time is expressed in seconds (Unity standard).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Internally, the value is floored to whole seconds.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Negative values are supported and preserved via sign propagation.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <c>NaN</c> or <c>Infinity</c> inputs are handled gracefully with a warning and zero output.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>
    /// This class does <b>not</b> perform unit conversion
    /// (see <see cref="TimeConverter"/> for that).
    /// </para>
    /// </summary>
    public static class TimeFormatter
    {
        /// <summary>
        /// Specifies the decomposition format for a time duration.
        /// </summary>
        public enum TimeFormatType
        {
            /// <summary>
            /// Format into hours, minutes, and seconds:
            /// H : M : S, where minutes and seconds are in the range [0, 59].
            /// </summary>
            HourMinSec,

            /// <summary>
            /// Format into minutes and seconds:
            /// M : S, where minutes may exceed 59.
            /// </summary>
            MinSec
        }

        /// <summary>
        /// Formats a time duration (in seconds) into hours, minutes, and seconds
        /// according to the specified decomposition type.
        ///
        /// The decomposition uses whole seconds obtained via floor rounding.
        /// For negative inputs, the sign is preserved in the resulting components.
        /// </summary>
        /// <param name="totalSeconds">
        /// Total time duration in seconds.
        /// May be negative. Must not be NaN or Infinity.
        /// </param>
        /// <param name="type">
        /// Specifies how the time should be decomposed
        /// (hours/minutes/seconds or minutes/seconds).
        /// </param>
        /// <returns>
        /// A tuple containing:
        /// - hours   (may be zero depending on <paramref name="type"/>)
        /// - minutes
        /// - seconds
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="type"/> is not a supported decomposition type.
        /// </exception>
        public static (int hours, int minutes, int seconds) FormatSeconds(float totalSeconds, TimeFormatType type)
        {
            if (float.IsNaN(totalSeconds) || float.IsInfinity(totalSeconds))
            {
                Debug.LogWarning($"TimeFormatr: input is {totalSeconds}. Returning zeros.");
                return (0, 0, 0);
            }

            int sign = 1;

            if (totalSeconds < 0f)
            {
                sign = -1;
                totalSeconds = -totalSeconds;
            }

            int wholeSeconds = Mathf.FloorToInt(totalSeconds);

            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            switch (type)
            {
                case TimeFormatType.HourMinSec:
                    {
                        hours = wholeSeconds / 3600;
                        int remainder = wholeSeconds % 3600;

                        minutes = remainder / 60;
                        seconds = remainder % 60;
                        break;
                    }
                case TimeFormatType.MinSec:
                    {
                        minutes = wholeSeconds / 60;
                        seconds = wholeSeconds % 60;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{type} is incorrect type. Method: FormatSeconds");
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }

            hours *= sign;
            minutes *= sign;
            seconds *= sign;

            return (hours, minutes, seconds);
        }

        /// <summary>
        /// Converts a time duration (in seconds) into a human-readable string
        /// according to the specified decomposition type.
        ///
        /// This method is intended for debugging and simple UI output.
        /// </summary>
        /// <param name="totalSeconds">Total time duration in seconds.</param>
        /// <param name="type">Desired decomposition format.</param>
        /// <returns>
        /// A formatted string such as:
        /// - "2 hour 20 min 40 sec"
        /// - "140 min 40 sec"
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="type"/> is not supported.
        /// </exception>
        public static string ToStringSeconds(float totalSeconds, TimeFormatType type)
        {
            var (h, m, s) = FormatSeconds(totalSeconds, type);

            switch (type)
            {
                case TimeFormatType.HourMinSec:
                    {
                        return $"{h} hour {m} min {s} sec";
                    }
                case TimeFormatType.MinSec:
                    {
                        return $"{m} min {s} sec";
                    }
                default:
                    {
                        Debug.LogError($"{type} is incorrect type. Method: ToStringSeconds");
                        throw new ArgumentOutOfRangeException(nameof(type));
                    }
            }
        }
    }
}
