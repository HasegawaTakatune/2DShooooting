using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class myBaseship : MonoBehaviour
{

    // 速度
    protected  float _speed;
    public float speed
    { get { return _speed; } set { _speed = value; } }

    // 発射間隔
    protected  float _shotDelay;
    public float shotDelay { get { return _shotDelay; } set { _shotDelay = value; } }

    // 弾丸
    protected  GameObject _bullet;
    public GameObject bullet { get { return _bullet; } set { _bullet = value; } }

    // 発砲許可
    protected  bool _safety;
    public bool safety { get { return _safety; } set { _safety = value; } }

    // 発射位置
    protected  List<Transform> _shotposition = new List<Transform>();
    public void AddShotPosition(Transform trns) { _shotposition.Add(trns); }

    // 初期化
    public virtual void Start()
    {
    }
    public virtual void Init(float spd, float sDly, GameObject blt, bool sfty)
    {
        _speed = spd;
        _shotDelay = sDly;
        _bullet = blt;
        _safety = sfty;
    }

    // 発射
    public virtual void Shot()
    {
        for (int i = 0; i < _shotposition.Count; i++)
            Instantiate(_bullet, _shotposition[i].position, _shotposition[i].rotation);
    }
}