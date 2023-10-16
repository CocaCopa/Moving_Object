#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MovingObject))]
public class MovingObjectGizmos : MonoBehaviour {

    [Tooltip("This helps the editor script to automatically assign any new created checkpoints to the 'Points' list")]
    [SerializeField] private Transform checkpointsHolderTransform;

    private MovingObject movingObject;
    private int pointsHolderChildCount;
    private int pointsListCount;

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

        //ManagePlatform();
        AddCheckpointsToList();
        AutoAssignMovingObject();
    }

    private void AutoAssignCheckpointsToList() {
        if (checkpointsHolderTransform != null) {
            pointsHolderChildCount = checkpointsHolderTransform.childCount;
            pointsListCount = movingObject.Points.Count;
            if (pointsHolderChildCount != pointsListCount) {
                AddCheckpointsToList();
            }
        }
    }

    private void AddCheckpointsToList() {
        movingObject.Points.Clear();
        foreach (Transform checkpoint in checkpointsHolderTransform) {
            movingObject.Points.Add(checkpoint);
        }
    }

    private void AutoAssignMovingObject() {
        if (movingObject.transform.childCount == 2) {
            movingObject.ObjectTransform = movingObject.transform.GetChild(1);
        }
        else {
            movingObject.ObjectTransform = null;
        }

        if (movingObject.transform.childCount > 2) {
            for (int i = 2; i < movingObject.transform.childCount; i++) {
                GameObject excessiveChild = movingObject.transform.GetChild(i).gameObject;
                DestroyImmediate(excessiveChild);
            }
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
