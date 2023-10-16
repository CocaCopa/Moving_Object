using UnityEngine;

namespace CocaCopa {
    public class Utilities {

        /// <summary>
        /// Start a timer with the specified values
        /// </summary>
        /// <param name="waitTimer">Current timer value</param>
        /// <param name="waitTime">Maximum timer value</param>
        /// <returns>True, when the timer value is 0</returns>
        public static bool TickTimer(ref float waitTimer, float waitTime) {
            if (waitTimer == 0) {
                waitTimer = waitTime;
                return true;
            }
            else {
                waitTimer -= Time.deltaTime;
                waitTimer = Mathf.Clamp(waitTimer, 0, waitTime);
            }
            return false;
        }

        /// <summary>
        /// Interpolate an animation curve based on distance and speed.
        /// </summary>
        /// <param name="curve">The animation curve to interpolate.</param>
        /// <param name="animationPoints">The progression along the curve.</param>
        /// <param name="speed">The speed of interpolation in meters per second (m/s).</param>
        /// <param name="distance">The total distance covered by the animation.</param>
        /// <param name="increment">Set to 'false' to interpolate in reverse.</param>
        /// <returns>The value on the curve corresponding to the progression.
        /// </returns>
        public static float EvaluateAnimationCurve(AnimationCurve curve, ref float animationPoints, float speed, float distance, bool increment = true) {
            if (increment)
                animationPoints += (speed / distance) * Time.deltaTime;
            else
                animationPoints -= (speed / distance) * Time.deltaTime;
            animationPoints = float.IsNaN(animationPoints) ? 0 : Mathf.Clamp01(animationPoints);
            return curve.Evaluate(animationPoints);
        }

        /// <summary>
        /// Swaps the values between the 2 given Vectors
        /// </summary>
        /// <param name="vector_1"></param>
        /// <param name="vector_2"></param>
        public static void SwapVectorValues(ref Vector3 vector_1, ref Vector3 vector_2) {
            Vector3 temp = vector_1;
            vector_1 = vector_2;
            vector_2 = temp;
        }
    }
}