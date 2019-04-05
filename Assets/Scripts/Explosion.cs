using UnityEngine;

public class Explosion : MonoBehaviour
{
    void OnAnimationFinish ()
    {
        // 爆発アニメーションが終了後、爆発を消す
        Destroy (gameObject);
    }
}

