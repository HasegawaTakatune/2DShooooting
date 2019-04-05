using UnityEngine;
using System.Collections;

public class Enemy : Spaceship
{
    // ヒットポイント
    public int hp = 1;

    // スコアのポイント
    public int point = 100;

    IEnumerator Start ()
    {
        // ローカル座標のY軸のマイナス方向に移動する
        Move(transform.up * -1);

        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (canShot == false) {
            yield break;
        }

        while (true) {
            // 子要素を全て取得する
            for (int i = 0; i < transform.childCount; i++) {

                Transform shotPosition = transform.GetChild(i);
                
                // ShotPositionの位置・角度で弾を撃つ
                Shot(shotPosition);
            }

            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);
        }
    }

    // 機体の移動
    override protected void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
    
    void OnTriggerEnter2D (Collider2D c)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // レイヤー名がBullet (Player)以外の時は何も行わない
        if (layerName != "Bullet (Player)") return;

        // 弾の削除
        Destroy(c.gameObject);

        // PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;

        // Bulletコンポーネントを取得
        Bullet playerBullet = playerBulletTransform.GetComponent<Bullet>();

        // ヒットポイントを減らす
        hp -= playerBullet.power;

        if (hp > 0) {

            // ダメージ描写
            GetAnimator().SetTrigger("Damage");
        } else {

            // ポイント加点
            FindObjectOfType<Score>().AddPoint(point);

            // 爆発
            Explosion();

            // エネミーの削除
            Destroy(gameObject);
        }
    }
}

