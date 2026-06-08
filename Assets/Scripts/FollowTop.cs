using UnityEngine;

public class FollowTop : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _height = 40f;

    [SerializeField] private bool _followRotationf = false;

    private void LateUpdate()
    {
        if (_player == null)
        {
            return;
        }

        transform.position = _player.position + Vector3.up * _height;

        if (_followRotationf)
        {
            transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
        }
    }
}
