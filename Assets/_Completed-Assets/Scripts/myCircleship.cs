using UnityEngine;

public class myCircleship : myBaseship
{
    // myBaseshipを継承しているので初期設定が不要

    // 発射
    public override void Shot()
    {        
        for (int i = 0; i < 18; i++)
            Instantiate(_bullet, _shotposition[0].position, Quaternion.Euler(0, 0, i * 20 + Random.Range(0, 19)));
    }
}