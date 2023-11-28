using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LogoManeger : MonoBehaviour
{

    [SerializeField, Tooltip("隠すオブジェ")]
    private GameObject hideSquare;
    [SerializeField, Tooltip("ロゴイメージ")]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        //コルーチンを表示
        StartCoroutine(ChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //ロゴマークの表示
    IEnumerator Logo()
    {
        //色を取得
        var c = image.color;
        var ca = c.a;

        //色が付くまで繰り返す
        while (ca < 1f)
        {
            //色の付く速度
            ca += 0.7f * Time.deltaTime;
            //色を変える
            image.color = new Color(c.r, c.g, c.b, ca);

            yield return null;
        }
        // 指定された時間だけ待機
        yield return new WaitForSeconds(1.0f);
        //色が透明になるまで繰り返す
        while (ca > 0f)
        {
            //透明になっていく速度
            ca -= 0.7f * Time.deltaTime;
            //色を変える
            image.color = new Color(c.r, c.g, c.b, ca);
            yield return null;
        }

        //コルーチンを表示
        StartCoroutine(ChangeScene());
    }

    //タイトルシーンに移動
    IEnumerator ChangeScene()
    {
        // 指定された時間だけ待機
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i <= 224; i++)
        {
            hideSquare.transform.position += new Vector3(1, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("title");
    }
}
