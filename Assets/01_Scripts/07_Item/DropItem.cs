using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : InteractableObject
{
    void Awake()
    {
        // 줍는 아이템이라 무조건 e키
        interactionType = InteractionMarkType.Interaction;
    }

    public override void Interaction()
    {
        if ( objectData is ItemData item )
        {
            GameManager.Instance.Player.Inventory.AddItem( item.NewItem(), out int remainCount );
            SoundManager.Instance?.PlaySfxOnce(ESfxName.PickUp);

            if ( remainCount > 0 )
            {
                // 번들 아이템 주웠을 때, 아이템창 꽉차고 남은거 처리하는거    
                // 언젠가 시간날때.. 지금 여기까진 필요없엉
            }
        }

        // 테스트를 위해 지우지 않기! 당근 쌓기!!
        //Destroy( this.gameObject );
        
    }
}