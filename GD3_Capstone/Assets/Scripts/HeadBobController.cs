using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform cameraHolder = null;

    private float toggleSpeed = 0.1f;
    private Vector3 startPos;
    private PlayerMovement controller;
    void Start()
    {
        controller = GetComponent<PlayerMovement>();
        startPos = _camera.localPosition;
    }
    void Update()
    {
        if (!_enable) return;

        CheckMotion();
        ResetPosition();
    }
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(controller.GetMovementVector().x, 0, controller.GetMovementVector().z).magnitude;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void ResetPosition()
    {
        if (_camera.localPosition == startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, 1 * Time.deltaTime);
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }
}
