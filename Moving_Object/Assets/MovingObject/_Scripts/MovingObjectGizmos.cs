#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MovingObject))]
[ExecuteInEditMode]
public class MovingObjectGizmos : MonoBehaviour {

    private MovingObject movingObject;
    private int pointsHolderChildCount;
    private int pointsListCount;
    private List<Transform> pointsList = new List<Transform>();

    private void Update() {
        if (movingObject == null)
            movingObject = GetComponent<MovingObject>();
        
        if (EditorApplication.isPlaying) {
            return;
        }

        ManagePlatform();
        AutoAssignCheckpointsToList();
        AutoAssignMovingObject();
    }

    private void OnValidate() {

        if (movingObject == null)
            movingObject = GetComponent<MovingObject>();

        ManagePlatform();
        movingObject.Points.Clear();
        
        foreach (Transform checkpoint in movingObject.CheckpointsHolderTransform) {
            movingObject.Points.Add(checkpoint);
        }
        AutoAssignMovingObject();
    }

    private void AutoAssignCheckpointsToList() {
        if (movingObject.CheckpointsHolderTransform != null) {
            pointsHolderChildCount = movingObject.CheckpointsHolderTransform.childCount;
            pointsListCount = movingObject.Points.Count;

            if (pointsHolderChildCount != pointsListCount) {
                movingObject.Points.Clear();
                foreach (Transform checkpoint in movingObject.CheckpointsHolderTransform) {
                    movingObject.Points.Add(checkpoint);
                }
            }
        }
    }

    private void AutoAssignMovingObject() {
        if (movingObject.transform.childCount == 2) {
            movingObject.ObjectTransform = movingObject.transform.GetChild(1);
        }
        else {
            movingObject.ObjectTransform = null;
        }
    }

    private void ManagePlatform() {
        List<Transform> points = movingObject.Points;
        Transform platformTransform = movingObject.ObjectTransform;
        if (points != null && points.Count != 0) {
            if (platformTransform != null && Selection.activeGameObject == platformTransform.gameObject) {
                points[0].localPosition = platformTransform.localPosition;
            }
            else if (platformTransform != null && Selection.activeGameObject == points[0].gameObject) {
                platformTransform.localPosition = points[0].localPosition;
            }
        }
    }

    private void OnDrawGizmos() {

        List<Transform> points = movingObject?.Points;
        if (points == null || points.Count == 0) return;

        for (int i = 0; i < points.Count; i++) {
            if (i + 1 < points.Count) {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(points[i].position, 0.2f);
        }
    }
}
#endif
