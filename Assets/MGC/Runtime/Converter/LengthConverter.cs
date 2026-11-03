using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides length unit conversion utilities for Unity projects.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and is designed to bridge
    /// Unity-friendly numeric types (<see cref="System.Single"/>, extension methods)
    /// with physically correct unit conversions.
    /// </para>
    ///
    /// <para>Supported unit systems:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Metric system (millimeter, centimeter, meter, kilometer)
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Imperial system (inch, foot, yard, mile)
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Design principles:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>All methods are static and stateless.</description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       No dependency on scene objects or <c>MonoBehaviour</c> lifecycle.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Explicit unit conversions only (no implicit or automatic conversions).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>Intended for runtime use.</description>
    ///   </item>
    /// </list>
    ///
    /// <para>Typical usage examples:</para>
    /// <code>
    /// float meters = LengthConverter.MetricConvert(
    ///     1500f,
    ///     LengthMetric.mm,
    ///     LengthMetric.m);
    ///
    /// float feet = 2f.M2Foot();
    /// </code>
    /// </summary>

    public static class LengthConverter
    {
        /// <summary>
        /// Metric length units based on the SI system.
        /// </summary>
        public enum LengthMetric
        {
            /// <summary>Millimeter (1e-3 meter).</summary>
            mm,

            /// <summary>Centimeter (1e-2 meter).</summary>
            cm,

            /// <summary>Meter (base SI unit of length).</summary>
            m,

            /// <summary>Kilometer (1e3 meters).</summary>
            km,
        }
        /// <summary>
        /// Imperial (US customary) length units.
        /// </summary>
        public enum LengthImperial
        {
            /// <summary>Inch.</summary>
            inch,

            /// <summary>Foot (12 inches).</summary>
            foot,

            /// <summary>Yard (3 feet).</summary>
            yard,

            /// <summary>Mile (1760 yards).</summary>
            mile,
        }

        /// <summary>
        /// Converts a length value between metric units.
        /// </summary>
        /// <param name="value">Input value expressed in the source unit.</param>
        /// <param name="from">Source metric unit.</param>
        /// <param name="to">Target metric unit.</param>
        /// <returns>Converted value expressed in the target metric unit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an unsupported unit is provided.
        /// </exception>
        public static float MetricConvert(float value, LengthMetric from, LengthMetric to)
        {
            switch (from)
            {
                case LengthMetric.mm:
                    {
                        switch (to)
                        {
                            case LengthMetric.mm:
                                {
                                    return value;
                                }
                            case LengthMetric.cm:
                                {
                                    return value * 0.1f;
                                }
                            case LengthMetric.m:
                                {
                                    return value * 0.001f;
                                }
                            case LengthMetric.km:
                                {
                                    return value * 0.000001f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: MetricConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthMetric.cm:
                    {
                        switch (to)
                        {
                            case LengthMetric.mm:
                                {
                                    return value * 10f;
                                }
                            case LengthMetric.cm:
                                {
                                    return value;
                                }
                            case LengthMetric.m:
                                {
                                    return value * 0.01f;
                                }
                            case LengthMetric.km:
                                {
                                    return value * 0.00001f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: MetricConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthMetric.m:
                    {
                        switch (to)
                        {
                            case LengthMetric.mm:
                                {
                                    return value * 1000f;
                                }
                            case LengthMetric.cm:
                                {
                                    return value * 100f;
                                }
                            case LengthMetric.m:
                                {
                                    return value;
                                }
                            case LengthMetric.km:
                                {
                                    return value * 0.001f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: MetricConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthMetric.km:
                    {
                        switch (to)
                        {
                            case LengthMetric.mm:
                                {
                                    return value * 1000000f;
                                }
                            case LengthMetric.cm:
                                {
                                    return value * 100000f;
                                }
                            case LengthMetric.m:
                                {
                                    return value * 1000f;
                                }
                            case LengthMetric.km:
                                {
                                    return value;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: MetricConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                default:
                    {
                        Debug.LogError($"{from} is incorrect unit. Method: MetricConvert");
                        throw new ArgumentOutOfRangeException(nameof(from));
                    }
            }
        }
        /// <summary>
        /// Converts a length value between imperial units.
        /// </summary>
        /// <param name="value">Input value expressed in the source unit.</param>
        /// <param name="from">Source imperial unit.</param>
        /// <param name="to">Target imperial unit.</param>
        /// <returns>Converted value expressed in the target imperial unit.</returns>
        public static float ImperialConvert(float value, LengthImperial from, LengthImperial to)
        {
            switch (from)
            {
                case LengthImperial.inch:
                    {
                        switch (to)
                        {
                            case LengthImperial.inch:
                                {
                                    return value;
                                }
                            case LengthImperial.foot:
                                {
                                    return value / 12f;
                                }
                            case LengthImperial.yard:
                                {
                                    return value / 36f;
                                }
                            case LengthImperial.mile:
                                {
                                    return value / 63360f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: ImperialConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthImperial.foot:
                    {
                        switch (to)
                        {
                            case LengthImperial.inch:
                                {
                                    return value * 12f;
                                }
                            case LengthImperial.foot:
                                {
                                    return value;
                                }
                            case LengthImperial.yard:
                                {
                                    return value / 3f;
                                }
                            case LengthImperial.mile:
                                {
                                    return value / 5280f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: ImperialConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthImperial.yard:
                    {
                        switch (to)
                        {
                            case LengthImperial.inch:
                                {
                                    return value * 36f;
                                }
                            case LengthImperial.foot:
                                {
                                    return value * 3f;
                                }
                            case LengthImperial.yard:
                                {
                                    return value;
                                }
                            case LengthImperial.mile:
                                {
                                    return value / 1760f;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: ImperialConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                case LengthImperial.mile:
                    {
                        switch (to)
                        {
                            case LengthImperial.inch:
                                {
                                    return value * 63360f;
                                }
                            case LengthImperial.foot:
                                {
                                    return value * 5280f;
                                }
                            case LengthImperial.yard:
                                {
                                    return value * 1760f;
                                }
                            case LengthImperial.mile:
                                {
                                    return value;
                                }
                            default:
                                {
                                    Debug.LogError($"{to} is incorrect unit. Method: ImperialConvert");
                                    throw new ArgumentOutOfRangeException(nameof(to));
                                }
                        }
                    }
                default:
                    {
                        Debug.LogError($"{from} is incorrect unit. Method: ImperialConvert");
                        throw new ArgumentOutOfRangeException(nameof(from));
                    }
            }
        }

        /// <summary>
        /// Converts a length value from the imperial system to the metric system.
        ///
        /// Conversion path:
        /// Imperial → inch → millimeter → target metric unit.
        /// </summary>
        /// <param name="value">Input value expressed in an imperial unit.</param>
        /// <param name="imperial">Source imperial unit.</param>
        /// <param name="metric">Target metric unit.</param>
        /// <returns>Converted value expressed in the target metric unit.</returns>
        public static float ImperialToMetric(float value, LengthImperial imperial, LengthMetric metric)
        {
            float inchValue = ImperialConvert(value, imperial, LengthImperial.inch);
            float mmValue = inchValue * 25.4f;
            return MetricConvert(mmValue, LengthMetric.mm, metric);
        }
        /// <summary>
        /// Converts a length value from the metric system to the imperial system.
        ///
        /// Conversion path:
        /// Metric → millimeter → inch → target imperial unit.
        /// </summary>
        /// <param name="value">Input value expressed in a metric unit.</param>
        /// <param name="metric">Source metric unit.</param>
        /// <param name="imperial">Target imperial unit.</param>
        /// <returns>Converted value expressed in the target imperial unit.</returns>
        public static float MetricToImperial(float value, LengthMetric metric, LengthImperial imperial)
        {
            float mmValue = MetricConvert(value, metric, LengthMetric.mm);
            float inchValue = mmValue / 25.4f;
            return ImperialConvert(inchValue, LengthImperial.inch, imperial);
        }

        /// <summary>
        /// Converts millimeters to centimeters.
        /// </summary>
        public static float Mm2Cm(this float value)
        {
            return MetricConvert(value, LengthMetric.mm, LengthMetric.cm);
        }
        /// <summary>
        /// Converts millimeters to meters.
        /// </summary>
        public static float Mm2M(this float value)
        {
            return MetricConvert(value, LengthMetric.mm, LengthMetric.m);
        }
        /// <summary>
        /// Converts millimeters to kilometers.
        /// </summary>
        public static float Mm2Km(this float value)
        {
            return MetricConvert(value, LengthMetric.mm, LengthMetric.km);
        }

        /// <summary>
        /// Converts centimeters to millimeters.
        /// </summary>
        public static float Cm2Mm(this float value)
        {
            return MetricConvert(value, LengthMetric.cm, LengthMetric.mm);
        }
        /// <summary>
        /// Converts centimeters to meters.
        /// </summary>
        public static float Cm2M(this float value)
        {
            return MetricConvert(value, LengthMetric.cm, LengthMetric.m);
        }
        /// <summary>
        /// Converts centimeters to kilometers.
        /// </summary>
        public static float Cm2Km(this float value)
        {
            return MetricConvert(value, LengthMetric.cm, LengthMetric.km);
        }

        /// <summary>
        /// Converts meters to millimeters.
        /// </summary>
        public static float M2Mm(this float value)
        {
            return MetricConvert(value, LengthMetric.m, LengthMetric.mm);
        }
        /// <summary>
        /// Converts meters to centimeters.
        /// </summary>
        public static float M2Cm(this float value)
        {
            return MetricConvert(value, LengthMetric.m, LengthMetric.cm);
        }
        /// <summary>
        /// Converts meters to kilometers.
        /// </summary>
        public static float M2Km(this float value)
        {
            return MetricConvert(value, LengthMetric.m, LengthMetric.km);
        }

        /// <summary>
        /// Converts kilometers to millimeters.
        /// </summary>
        public static float Km2Mm(this float value)
        {
            return MetricConvert(value, LengthMetric.km, LengthMetric.mm);
        }
        /// <summary>
        /// Converts kilometers to centimeters.
        /// </summary>
        public static float Km2Cm(this float value)
        {
            return MetricConvert(value, LengthMetric.km, LengthMetric.cm);
        }
        /// <summary>
        /// Converts kilometers to meters.
        /// </summary>
        public static float Km2M(this float value)
        {
            return MetricConvert(value, LengthMetric.km, LengthMetric.m);
        }

        /// <summary>
        /// Converts inches to feet.
        /// </summary>
        public static float Inch2Foot(this float value)
        {
            return ImperialConvert(value, LengthImperial.inch, LengthImperial.foot);
        }
        /// <summary>
        /// Converts inches to yards.
        /// </summary>
        public static float Inch2Yard(this float value)
        {
            return ImperialConvert(value, LengthImperial.inch, LengthImperial.yard);
        }
        /// <summary>
        /// Converts inches to miles.
        /// </summary>
        public static float Inch2Mile(this float value)
        {
            return ImperialConvert(value, LengthImperial.inch, LengthImperial.mile);
        }

        /// <summary>
        /// Converts feet to inches.
        /// </summary>
        public static float Foot2Inch(this float value)
        {
            return ImperialConvert(value, LengthImperial.foot, LengthImperial.inch);
        }
        /// <summary>
        /// Converts feet to yards.
        /// </summary>
        public static float Foot2Yard(this float value)
        {
            return ImperialConvert(value, LengthImperial.foot, LengthImperial.yard);
        }
        /// <summary>
        /// Converts feet to miles.
        /// </summary>
        public static float Foot2Mile(this float value)
        {
            return ImperialConvert(value, LengthImperial.foot, LengthImperial.mile);
        }

        /// <summary>
        /// Converts yards to inches.
        /// </summary>
        public static float Yard2Inch(this float value)
        {
            return ImperialConvert(value, LengthImperial.yard, LengthImperial.inch);
        }
        /// <summary>
        /// Converts yards to feet.
        /// </summary>
        public static float Yard2Foot(this float value)
        {
            return ImperialConvert(value, LengthImperial.yard, LengthImperial.foot);
        }
        /// <summary>
        /// Converts yards to miles.
        /// </summary>
        public static float Yard2Mile(this float value)
        {
            return ImperialConvert(value, LengthImperial.yard, LengthImperial.mile);
        }

        /// <summary>
        /// Converts miles to inches.
        /// </summary>
        public static float Mile2Inch(this float value)
        {
            return ImperialConvert(value, LengthImperial.mile, LengthImperial.inch);
        }
        /// <summary>
        /// Converts miles to feet.
        /// </summary>
        public static float Mile2Foot(this float value)
        {
            return ImperialConvert(value, LengthImperial.mile, LengthImperial.foot);
        }
        /// <summary>
        /// Converts miles to yards.
        /// </summary>
        public static float Mile2Yard(this float value)
        {
            return ImperialConvert(value, LengthImperial.mile, LengthImperial.yard);
        }

        /// <summary>
        /// Converts millimeters to inches.
        /// </summary>
        public static float Mm2Inch(this float value)
        {
            return MetricToImperial(value, LengthMetric.mm, LengthImperial.inch);
        }
        /// <summary>
        /// Converts millimeters to feet.
        /// </summary>
        public static float Mm2Foot(this float value)
        {
            return MetricToImperial(value, LengthMetric.mm, LengthImperial.foot);
        }
        /// <summary>
        /// Converts millimeters to yards.
        /// </summary>
        public static float Mm2Yard(this float value)
        {
            return MetricToImperial(value, LengthMetric.mm, LengthImperial.yard);
        }
        /// <summary>
        /// Converts millimeters to miles.
        /// </summary>
        public static float Mm2Mile(this float value)
        {
            return MetricToImperial(value, LengthMetric.mm, LengthImperial.mile);
        }

        /// <summary>
        /// Converts centimeters to inches.
        /// </summary>
        public static float Cm2Inch(this float value)
        {
            return MetricToImperial(value, LengthMetric.cm, LengthImperial.inch);
        }
        /// <summary>
        /// Converts centimeters to feet.
        /// </summary>
        public static float Cm2Foot(this float value)
        {
            return MetricToImperial(value, LengthMetric.cm, LengthImperial.foot);
        }
        /// <summary>
        /// Converts centimeters to yards.
        /// </summary>
        public static float Cm2Yard(this float value)
        {
            return MetricToImperial(value, LengthMetric.cm, LengthImperial.yard);
        }
        /// <summary>
        /// Converts centimeters to miles.
        /// </summary>
        public static float Cm2Mile(this float value)
        {
            return MetricToImperial(value, LengthMetric.cm, LengthImperial.mile);
        }

        /// <summary>
        /// Converts meters to inches.
        /// </summary>
        public static float M2Inch(this float value)
        {
            return MetricToImperial(value, LengthMetric.m, LengthImperial.inch);
        }
        /// <summary>
        /// Converts meters to feet.
        /// </summary>
        public static float M2Foot(this float value)
        {
            return MetricToImperial(value, LengthMetric.m, LengthImperial.foot);
        }
        /// <summary>
        /// Converts meters to yards.
        /// </summary>
        public static float M2Yard(this float value)
        {
            return MetricToImperial(value, LengthMetric.m, LengthImperial.yard);
        }
        /// <summary>
        /// Converts meters to miles.
        /// </summary>
        public static float M2Mile(this float value)
        {
            return MetricToImperial(value, LengthMetric.m, LengthImperial.mile);
        }

        /// <summary>
        /// Converts kilometers to inches.
        /// </summary>
        public static float Km2Inch(this float value)
        {
            return MetricToImperial(value, LengthMetric.km, LengthImperial.inch);
        }
        /// <summary>
        /// Converts kilometers to feet.
        /// </summary>
        public static float Km2Foot(this float value)
        {
            return MetricToImperial(value, LengthMetric.km, LengthImperial.foot);
        }
        /// <summary>
        /// Converts kilometers to yards.
        /// </summary>
        public static float Km2Yard(this float value)
        {
            return MetricToImperial(value, LengthMetric.km, LengthImperial.yard);
        }
        /// <summary>
        /// Converts kilometers to miles.
        /// </summary>
        public static float Km2Mile(this float value)
        {
            return MetricToImperial(value, LengthMetric.km, LengthImperial.mile);
        }

        /// <summary>
        /// Converts inches to millimeters.
        /// </summary>
        public static float Inch2Mm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.inch, LengthMetric.mm);
        }
        /// <summary>
        /// Converts inches to centimeters.
        /// </summary>
        public static float Inch2Cm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.inch, LengthMetric.cm);
        }
        /// <summary>
        /// Converts inches to meters.
        /// </summary>
        public static float Inch2M(this float value)
        {
            return ImperialToMetric(value, LengthImperial.inch, LengthMetric.m);
        }
        /// <summary>
        /// Converts inches to kilometers.
        /// </summary>
        public static float Inch2Km(this float value)
        {
            return ImperialToMetric(value, LengthImperial.inch, LengthMetric.km);
        }

        /// <summary>
        /// Converts feet to millimeters.
        /// </summary>
        public static float Foot2Mm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.foot, LengthMetric.mm);
        }
        /// <summary>
        /// Converts feet to centimeters.
        /// </summary>
        public static float Foot2Cm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.foot, LengthMetric.cm);
        }
        /// <summary>
        /// Converts feet to meters.
        /// </summary>
        public static float Foot2M(this float value)
        {
            return ImperialToMetric(value, LengthImperial.foot, LengthMetric.m);
        }
        /// <summary>
        /// Converts feet to kilometers.
        /// </summary>
        public static float Foot2Km(this float value)
        {
            return ImperialToMetric(value, LengthImperial.foot, LengthMetric.km);
        }

        /// <summary>
        /// Converts yards to millimeters.
        /// </summary>
        public static float Yard2Mm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.yard, LengthMetric.mm);
        }
        /// <summary>
        /// Converts yards to centimeters.
        /// </summary>
        public static float Yard2Cm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.yard, LengthMetric.cm);
        }
        /// <summary>
        /// Converts yards to meters.
        /// </summary>
        public static float Yard2M(this float value)
        {
            return ImperialToMetric(value, LengthImperial.yard, LengthMetric.m);
        }
        /// <summary>
        /// Converts yards to kilometers.
        /// </summary>
        public static float Yard2Km(this float value)
        {
            return ImperialToMetric(value, LengthImperial.yard, LengthMetric.km);
        }

        /// <summary>
        /// Converts miles to millimeters.
        /// </summary>
        public static float Mile2Mm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.mile, LengthMetric.mm);
        }
        /// <summary>
        /// Converts miles to centimeters.
        /// </summary>
        public static float Mile2Cm(this float value)
        {
            return ImperialToMetric(value, LengthImperial.mile, LengthMetric.cm);
        }
        /// <summary>
        /// Converts miles to meters.
        /// </summary>
        public static float Mile2M(this float value)
        {
            return ImperialToMetric(value, LengthImperial.mile, LengthMetric.m);
        }
        /// <summary>
        /// Converts miles to kilometers.
        /// </summary>
        public static float Mile2Km(this float value)
        {
            return ImperialToMetric(value, LengthImperial.mile, LengthMetric.km);
        }

    }
}
