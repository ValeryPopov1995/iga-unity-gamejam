using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private int steps = 30;
        [SerializeField] private float timeBetweenSteps = 0.1f;
        private Vector3 gravity;

        void Start()
        {
            gravity = Physics.gravity;
            lineRenderer.positionCount = steps;
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
        }

        public void DrawTrajectory(Vector3 startPoint, Vector3 force)
        {
            lineRenderer.enabled = force != default;
            if (force == default) return;

            var points = new Vector3[steps];
            for (var i = 0; i < steps; i++)
            {
                var t = i * timeBetweenSteps;
                var currentPosition = startPoint + force * t + 0.5f * gravity * t * t;
                points[i] = currentPosition;
            }

            lineRenderer.SetPositions(points);
        }
    }
}