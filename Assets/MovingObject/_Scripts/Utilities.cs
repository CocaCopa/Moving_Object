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
        /// Evaluate an animation curve based on the given distance and speed
        /// </summary>
        /// <param name="curve">Animation curve to be evaluated</param>
        /// <param name="animationPoints">Animation points value to evaluate the given curve</param>
        /// <param name="speed">Speed in m/s</param>
        /// <param name="distance">Distance between the start/end point</param>
        /// <param name="increment">Set to false, if you want the animation curve to be evaluated backwards</param>
        /// <returns>The given curve, evaluated</returns>
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

        /// <summary>
        /// Swaps the values between the 2 given floats
        /// </summary>
        /// <param name="vector_1"></param>
        /// <param name="vector_2"></param>
        public static void SwapFloatValues(ref float float_1, ref float float_2) {
            float temp = float_1;
            float_1 = float_2;
            float_2 = temp;
        }
    }
}