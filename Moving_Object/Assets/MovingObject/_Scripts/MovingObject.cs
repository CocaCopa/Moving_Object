using UnityEngine;
using CocaCopa;
using System.Collections.Generic;
using System.Linq;

public class MovingObject : MonoBehaviour {

    private enum MovementType { Circle, PingPong };
    [Tooltip("The transform of the moving object")]
    [SerializeField] private Transform objectTransform;
    [Space(10)]
    [Tooltip("Select between different movement types")]
    [SerializeField] private MovementType movementType;
    [Tooltip("The motion curve of the object")]
    [SerializeField] private AnimationCurve motionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [Tooltip("Speed of the platform in m/s")]
    [SerializeField] private float platformSpeed;
    [Tooltip("How long should the object wait once it reaches a checkpoint")]
    [SerializeField] private float waitTime;
    [Tooltip("Checkpoints of the platform")]
    [SerializeField] private List<Transform> points;

    private float motionCurvePoints;

    private List<Transform> pointsArray;
    private List<Transform> reversedPointsArray;
    private bool useReversedPointsArray = false;

    private float waitTimer;
    private int pointIndex;
    private int indexFromPosition;
    private int indexToPosition;

    public List<Transform> Points { get { return points; } set { points = value; } }
    public Transform ObjectTransform { get { return objectTransform; } set { objectTransform = value; } }

    private void Start() {
        Initialize();
    }

    private void Update() {
        InterpolateObjectPosition();
        RestartMotionBasedOnMovementType();
    }

    private void Initialize() {
        waitTimer = waitTime;
        pointIndex = 0;
        pointsArray = points;
        reversedPointsArray = ReverseArrayPoints();
        
    }

    private List<Transform> ReverseArrayPoints() {
        List<Transform> list = points.ToList();
        list.Reverse();
        return list;
    }

    private void InterpolateObjectPosition() {
        float distance = Vector3.Distance(pointsArray[indexFromPosition].position, pointsArray[indexToPosition].position);
        float lerpTime = Utilities.EvaluateAnimationCurve(motionCurve, ref motionCurvePoints, platformSpeed, distance);
        objectTransform.position = Vector3.Lerp(pointsArray[indexFromPosition].position, pointsArray[indexToPosition].position, lerpTime);
    }

    private void RestartMotionBasedOnMovementType() {
        if (motionCurvePoints != 1)
            return;

        if (Utilities.TickTimer(ref waitTimer, waitTime)) {
            if (pointIndex + 1 < points.Count) {
                indexFromPosition = pointIndex;
                indexToPosition = pointIndex + 1;
                pointIndex++;
            }
            else {
                switch (movementType) {
                    case MovementType.Circle:
                    MovementType_Circle();
                    break;
                    case MovementType.PingPong:
                    MovementType_PingPong();
                    break;
                }
            }
            motionCurvePoints = 0;
        }
    }

    private void MovementType_Circle() {
        pointIndex = 0;
        indexFromPosition = points.Count - 1;
        indexToPosition = 0;
    }

    private void MovementType_PingPong() {
        pointIndex = 0;
        indexFromPosition = pointIndex;
        indexToPosition = pointIndex + 1;
        pointIndex++;

        useReversedPointsArray = !useReversedPointsArray;
        if (useReversedPointsArray) {
            pointsArray = reversedPointsArray;
        }
        else {
            pointsArray = points;
        }
    }
}
