using UnityEngine;

public class myRandomAttackship : myBaseship
{
    // myBaseshipを継承しているので初期設定が不要

    // 発射
    public override void Shot()
    {
        for (int i = 0; i < _shotposition.Count; i++)
            Instantiate(_bullet, _shotposition[i].position, Quaternion.Euler(0, 0, Random.Range(115, 225)));
    }
}
