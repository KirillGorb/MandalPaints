using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class DrawLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineDraw;
        [SerializeField] private LineRenderer lineDrawColl;
        [SerializeField] private Sound.SoundPlayOnAction actionSound;

        private void Start()
        {
            lineDraw.startWidth = 0.05f;
            lineDraw.endWidth = 0.05f;
            lineDraw.positionCount = 0;

            lineDrawColl.startWidth = 0.05f;
            lineDrawColl.endWidth = 0.05f;
            lineDrawColl.positionCount = 0;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) actionSound.ClickSound();
            if (Input.GetMouseButton(0))
            {
                lineDraw.positionCount++;
                lineDraw.SetPosition(lineDraw.positionCount - 1, GetWorldCoordinateMouse);
                lineDrawColl.positionCount++;
                lineDrawColl.SetPosition(lineDraw.positionCount - 1, -GetWorldCoordinateMouse);
            }
        }

        private Vector2 GetWorldCoordinateMouse =>
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}