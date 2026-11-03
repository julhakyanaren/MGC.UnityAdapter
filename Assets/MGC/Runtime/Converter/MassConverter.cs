using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides utilities for converting mass values between common metric and imperial units.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and is designed to bridge
    /// Unity-friendly <see cref="System.Single"/>-based values with physically meaningful mass units.
    /// </para>
    ///
    /// <para>Supported unit systems:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Metric: gram (g), kilogram (kg), tonne
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Imperial: pound (lb), ounce (oz)
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Design notes:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       All values are <see cref="System.Single"/>-based to match UnityEngine APIs
    ///       (e.g. <c>Rigidbody.mass</c>).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Kilogram (<c>kg</c>) is used as the internal base unit for conversions.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       The class is stateless and thread-safe.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Negative mass values are rejected.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <c>NaN</c> and <c>Infinity</c> results are handled with warnings.
    ///     </description>
    ///   </item>
    /// </list>
    /// </summary>

    public static class MassConverter
    {
        /// <summary>
        /// Supported mass units for conversion.
        /// </summary>
        public enum MassUnits
        {
            /// <summary>
            /// Gram.
            /// </summary>
            g,

            /// <summary>
            /// Kilogram (SI base unit of mass).
            /// </summary>
            kg,

            /// <summary>
            /// Metric tonne (1000 kilograms).
            /// </summary>
            tonne,

            /// <summary>
            /// Pound (avoirdupois).
            /// </summary>
            lb,

            /// <summary>Ounce (avoirdupois).</summary>
            oz
        }

        /// <summary>
        /// Converts a mass value from one unit to another.
        ///
        /// Internally, all conversions are performed via kilograms (kg):
        /// source unit -> kilograms -> target unit.
        /// </summary>
        /// <param name="value">
        /// Input mass value. Must be non-negative.
        /// </param>
        /// <param name="from">
        /// Unit of the input mass value.
        /// </param>
        /// <param name="to">
        /// Target unit for the converted mass value.
        /// </param>
        /// <returns>
        /// Mass value expressed in the target unit.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="value"/> is negative
        /// or if an unsupported unit is specified.
        /// </exception>
        public static float MassConvert(float value, MassUnits from, MassUnits to)
        {
            if (value < 0f)
            {
                Debug.LogError($"{value} must be non-negative. Method: MassConvert");
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (from == to)
            {
                return value;
            }

            float kilograms = 0f;

            switch (from)
            {
                case MassUnits.g:
                    {
                        kilograms = value * 0.001f;
                        break;
                    }
                case MassUnits.kg:
                    {
                        kilograms = value;
                        break;
                    }
                case MassUnits.tonne:
                    {
                        kilograms = value * 1000f;
                        break;
                    }
                case MassUnits.lb:
                    {
                        kilograms = value * 0.45359237f;
                        break;
                    }
                case MassUnits.oz:
                    {
                        kilograms = value * 0.028349523125f;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{from} is incorrect unit. Method: MassConvert");
                        throw new ArgumentOutOfRangeException(nameof(from));
                    }
            }

            float result = 0f;

            switch (to)
            {
                case MassUnits.g:
                    {
                        result = kilograms * 1000f;
                        break;
                    }
                case MassUnits.kg:
                    {
                        result = kilograms;
                        break;
                    }
                case MassUnits.tonne:
                    {
                        result = kilograms / 1000f;
                        break;
                    }
                case MassUnits.lb:
                    {
                        result = kilograms / 0.45359237f;
                        break;
                    }
                case MassUnits.oz:
                    {
                        result = kilograms / 0.028349523125f;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{to} is incorrect unit. Method: MassConvert");
                        throw new ArgumentOutOfRangeException(nameof(to));
                    }
            }

            if (float.IsNaN(result) || float.IsInfinity(result))
            {
                Debug.LogWarning(
                    $"MassConvert: conversion produced {result} (value={value}, from={from}, to={to}).");
            }

            if (value != 0f && result == 0f)
            {
                Debug.LogWarning(
                    $"MassConvert: possible underflow (value={value}, from={from}, to={to}) -> result=0.");
            }

            return result;
        }

        /// <summary>
        /// Converts grams to kilograms.
        /// </summary>
        public static float Gram2Kilogram(this float value)
        {
            return MassConvert(value, MassUnits.g, MassUnits.kg);
        }
        /// <summary>
        /// Converts grams to tonnes.
        /// </summary>
        public static float Gram2Tonne(this float value)
        {
            return MassConvert(value, MassUnits.g, MassUnits.tonne);
        }
        /// <summary>
        /// Converts grams to pounds.
        /// </summary>
        public static float Gram2Pound(this float value)
        {
            return MassConvert(value, MassUnits.g, MassUnits.lb);
        }
        /// <summary>
        /// Converts grams to ounces.
        /// </summary>
        public static float Gram2Ounce(this float value)
        {
            return MassConvert(value, MassUnits.g, MassUnits.oz);
        }

        /// <summary>
        /// Converts kilograms to grams.
        /// </summary>
        public static float Kilogram2Gram(this float value)
        {
            return MassConvert(value, MassUnits.kg, MassUnits.g);
        }
        /// <summary>
        /// Converts kilograms to tonnes.
        /// </summary>
        public static float Kilogram2Tonne(this float value)
        {
            return MassConvert(value, MassUnits.kg, MassUnits.tonne);
        }
        /// <summary>
        /// Converts kilograms to pounds.
        /// </summary>
        public static float Kilogram2Pound(this float value)
        {
            return MassConvert(value, MassUnits.kg, MassUnits.lb);
        }
        /// <summary>
        /// Converts kilograms to ounces.
        /// </summary>
        public static float Kilogram2Ounce(this float value)
        {
            return MassConvert(value, MassUnits.kg, MassUnits.oz);
        }

        /// <summary>
        /// Converts tonnes to grams.
        /// </summary>
        public static float Tonne2Gram(this float value)
        {
            return MassConvert(value, MassUnits.tonne, MassUnits.g);
        }
        /// <summary>
        /// Converts tonnes to kilograms.
        /// </summary>
        public static float Tonne2Kilogram(this float value)
        {
            return MassConvert(value, MassUnits.tonne, MassUnits.kg);
        }
        /// <summary>
        /// Converts tonnes to pounds.
        /// </summary>
        public static float Tonne2Pound(this float value)
        {
            return MassConvert(value, MassUnits.tonne, MassUnits.lb);
        }
        /// <summary>
        /// Converts tonnes to ounces.
        /// </summary>
        public static float Tonne2Ounce(this float value)
        {
            return MassConvert(value, MassUnits.tonne, MassUnits.oz);
        }

        /// <summary>
        /// Converts pounds to grams.
        /// </summary>
        public static float Pound2Gram(this float value)
        {
            return MassConvert(value, MassUnits.lb, MassUnits.g);
        }
        /// <summary>
        /// Converts pounds to kilograms.
        /// </summary>
        public static float Pound2Kilogram(this float value)
        {
            return MassConvert(value, MassUnits.lb, MassUnits.kg);
        }
        /// <summary>
        /// Converts pounds to tonnes.
        /// </summary>
        public static float Pound2Tonne(this float value)
        {
            return MassConvert(value, MassUnits.lb, MassUnits.tonne);
        }
        /// <summary>
        /// Converts pounds to ounces.
        /// </summary>
        public static float Pound2Ounce(this float value)
        {
            return MassConvert(value, MassUnits.lb, MassUnits.oz);
        }

        /// <summary>
        /// Converts ounces to grams.
        /// </summary>
        public static float Ounce2Gram(this float value)
        {
            return MassConvert(value, MassUnits.oz, MassUnits.g);
        }
        /// <summary>
        /// Converts ounces to kilograms.
        /// </summary>
        public static float Ounce2Kilogram(this float value)
        {
            return MassConvert(value, MassUnits.oz, MassUnits.kg);
        }
        /// <summary>
        /// Converts ounces to tonnes.
        /// </summary>
        public static float Ounce2Tonne(this float value)
        {
            return MassConvert(value, MassUnits.oz, MassUnits.tonne);
        }
        /// <summary>
        /// Converts ounces to pounds.
        /// </summary>
        public static float Ounce2Pound(this float value)
        {
            return MassConvert(value, MassUnits.oz, MassUnits.lb);
        }
    }
}
