using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehav : MonoBehaviour, ICollision
{
    [SerializeField]
    private Material _mat;
    [SerializeField]
    private AudioClip _collisionSound;
    [SerializeField]
    GameObject _explosionPrefab;
    [SerializeField]
    AudioClip _explosionSound;
    [SerializeField]
    ParticleSystem _particle;

    private Rigidbody _rb;
    private MeshRenderer _mr;
    private UIManager _uiManager;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public bool onBall()
    {

        StartCoroutine(Sequence());
        return false;
    }

    IEnumerator Sequence()
    {
        _rb.useGravity = true;
        _mr.material = _mat;
        AudioSource.PlayClipAtPoint(_collisionSound, transform.position);
        _uiManager.UpdateScore();

        yield return new WaitForSeconds(5f);

        Instantiate(_particle, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        Destroy(this.gameObject, 0.2f);
    }
}
