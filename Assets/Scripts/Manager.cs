using UnityEngine;

public class Manager : MonoBehaviour
{
    // Playerプレハブ
    public GameObject player;

    // タイトル
    private GameObject title;

    void Start()
    {
        // Titleゲームオブジェクトを検索し取得する
        title = GameObject.Find("Title");
    }

    void Update()
    {
        // ゲーム中ではなく、Xキーが押されたらtrueを返す
        if (IsPlaying() == false && Input.GetKeyDown(KeyCode.X)) {
            GameStart();
        }
    }

    void GameStart()
    {
        // ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        title.SetActive(false);
        Instantiate(player, player.transform.position, player.transform.rotation);
    }

    public void GameOver()
    {
        // ハイスコアの保存
        FindObjectOfType<Score>().Save();

        //vゲームオーバー時に、タイトルを表示する
        title.SetActive(true);
    }

    public bool IsPlaying()
    {
        // タイトルの表示状態によってゲーム中か判定する
        return title.activeSelf == false;
    }
}

