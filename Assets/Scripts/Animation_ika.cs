using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ika : MonoBehaviour
{
    //�΂̃A�j���[�V������z��ɂ���
    public List<Sprite> Tables;
    //�΂̃A�j���[�V����
    public SpriteRenderer Renderer;

    int AnimeIndex = 0;
    int WaitFrame = 0;

    List<int> AnimationTable = null;

    int iWaitMaxTime = 10;//10


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AnimationTable == null)
        {
            PlayAnimationBlack();

        }
        WhiteBlackStone();
    }

    //�΂̃A�j���[�V�����֐� 
    void WhiteBlackStone()
    {
        //�΂̃A�j���[�V����
        if (AnimationTable != null)
        {
            Renderer.sprite = Tables[AnimationTable[AnimeIndex]];

            if (iWaitMaxTime > 0) iWaitMaxTime--;

            if (iWaitMaxTime <= 0)
            {
                ++WaitFrame;
                if (WaitFrame > 100)//10
                {
                    ++AnimeIndex;
                    if (AnimationTable.Count <= AnimeIndex)
                    {
                        AnimationTable = null;
                    }
                    WaitFrame = 0;
                }

            }

        }

    }

    public void PlayAnimationBlack()
    {
        iWaitMaxTime = 10;

        WaitFrame = 0;
        AnimeIndex = 0;
        AnimationTable = new List<int>();

        AnimationTable.Add(0);
        AnimationTable.Add(1);
    }

}
