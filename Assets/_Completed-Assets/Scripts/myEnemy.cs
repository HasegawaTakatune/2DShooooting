using System.Collections;
using UnityEngine;

public class myEnemy : MonoBehaviour
{
    // 敵のタイプ一覧
    public enum EnemyType { NORMAL = 0, CIRCLE, RANDATTACK, MAXLENGTH }

    // ヒットポイント
    public int hp = 1;

    // スコア
    public int point = 100;

    // SpaceShipコンポーネント
    [SerializeField]
    myBaseship spaceship;

    // 弾丸
    [SerializeField]
    GameObject Bullet;

    // 爆発
    [SerializeField]
    GameObject explosion;

    // アニメーター
    private Animator animator;

    IEnumerator Start()
    {
        // アニメーター取得
        animator = GetComponent<Animator>();

        // spaceshipコンポネントの取得・初期化
        switch (Random.Range(0, (int)EnemyType.MAXLENGTH))
        {
            case (int)EnemyType.NORMAL: spaceship = new myBaseship(); break;
            case (int)EnemyType.CIRCLE: spaceship = new myCircleship(); break;
            case (int)EnemyType.RANDATTACK: spaceship = new myRandomAttackship(); break;
            default: spaceship = new myBaseship(); break;
        }
        spaceship.Init(0.5f, 0.3f, Bullet, true);
        for (int i = 0; i < transform.childCount; i++) spaceship.AddShotPosition(transform.GetChild(i).transform);

        // 使用済みの物は逐次捨てる
        Bullet = null;

        // ローカル座標のY軸のマイナス方向に移動する
        Move(transform.up * -1);

        // safetyが解除されていれば攻撃開始
        if (!spaceship.safety) yield break;

        while (true)
        {
            // shotPositionの位置/角度で弾を撃つ
            spaceship.Shot();

            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    // 移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        // レイヤー名がBullet(Player)以外は無視
        if (layerName != "Bullet (Player)") return;

        // PlayerBulletのTransformを取得
        Transform playerBulletTransform = collision.transform.parent;

        // Bulletomponentを取得a
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        // ライフ減
        hp = hp - 1;

        // 弾の削除
        Destroy(collision.gameObject);

        // ライフが0以下なら
        if (hp <= 0)
        {
            // スコアの加算
            FindObjectOfType<CompletedAssets.Score>().AddPoint(point);

            // 爆発
            Instantiate(explosion, transform.position, transform.rotation);

            // Enemy削除
            Destroy(gameObject);
        }
        else
        {
            // Damage
            animator.SetTrigger("Damage");
        }
    }
}
