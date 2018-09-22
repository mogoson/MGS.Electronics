/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Interval.cs
 *  Description  :  Interval form min to max.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/22/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace Mogoson.Mathematics
{
    /// <summary>
    /// Interval form min to max.
    /// </summary>
    [Serializable]
    public struct Interval
    {
        /// <summary>
        /// Min value of interval.
        /// </summary>
        public float min;

        /// <summary>
        /// Max value of interval.
        /// </summary>
        public float max;

        /// <summary>
        /// Length of interval.
        /// </summary>
        public float Length
        {
            get { return max - min; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="min">Min value of interval.</param>
        /// <param name="max">Max value of interval.</param>
        public Interval(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}