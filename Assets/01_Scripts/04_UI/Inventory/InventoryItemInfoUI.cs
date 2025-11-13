using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoUI : MonoBehaviour
{
    [ SerializeField ] GameObject infoPanel;

    [ Header( "Item Info" ) ]
    [ SerializeField ] TextMeshProUGUI itemNameText;

    [ SerializeField ] TextMeshProUGUI itemDescriptionText;

    [ Header( "Item Effect" ) ]
    [ Header( "Buttons" ) ]
    [ SerializeField ] private Button btnUse;

    [ SerializeField ] private Button btnEquip;
    [ SerializeField ] private Button btnUnequip;
    [ SerializeField ] private Button btnThrow;

    private Item nowItem;
    //private ItemData nowItemData;

    public event Action OnItemButtonAction ;

    [ SerializeField ] private InventoryItemInfoText[] effectInfoTexts;

    private void Awake()
    {
        btnUse.onClick.AddListener( OnClickUseItem );
        btnEquip.onClick.AddListener( OnClickEquipItem );
        btnUnequip.onClick.AddListener( OnClickUnequipItem );
        btnThrow.onClick.AddListener( OnClickThrowItem );
    }
    

    public void ShowInfo( Item item )
    {
        nowItem = item;
        //nowItemData = ItemManager.Instance.GetItemData( item.Name );

        infoPanel.SetActive( true );

        itemNameText.text = item.DisplayName;
        itemDescriptionText.text = item.Description;

        if ( item is EquipItem equipItem )
        {
            btnUse.gameObject.SetActive( false );

            if ( equipItem.IsEquip )
            {
                btnEquip.gameObject.SetActive( false );
                btnUnequip.gameObject.SetActive( true );
            }
            else
            {
                btnEquip.gameObject.SetActive( true );
                btnUnequip.gameObject.SetActive( false );
            }

            // 일단 공격력만!!! 띄워!! 그만해! 다른거 넘어가!!
            foreach ( InventoryItemInfoText effectInfoText in effectInfoTexts )
            {
                effectInfoText.HideInfoText();
            }

            effectInfoTexts[ 0 ]?.ShowInfoText( "공격력", equipItem.Atk.ToString() );
        }
        else
        {
            btnUnequip.gameObject.SetActive( false );
            btnEquip.gameObject.SetActive( false );

            if ( item is ConsumableItem consumableItem )
            {
                btnUse.gameObject.SetActive( true );

                ConsumableEffect[] consumable = consumableItem.Consumable;
                for ( int i = 0; i < effectInfoTexts.Length; ++i )
                {
                    if ( consumable.Length > i )
                    {
                        try
                        {
                            //키 없을 수도 있음~~
                            // 템창에 독있는건 안보여줄거임
                            effectInfoTexts[ i ].ShowInfoText( GameDefaultSettings.ConsumableText[ consumableItem.Consumable[ i ].consumableType ],
                                                               consumableItem.Consumable[ i ].amount.ToString() );
                        }
                        catch
                        {
                            effectInfoTexts[ i ].HideInfoText();    
                        }
                    }
                    else
                    {
                        effectInfoTexts[ i ].HideInfoText();
                    }
                }
            }
            else
            {
                for ( int i = 0; i < effectInfoTexts.Length; ++i )
                {
                    effectInfoTexts[i].HideInfoText();
                }
            }
        }
    }

    public void HideInfo()
    {
        infoPanel.SetActive( false );
    }

    public void OnClickUseItem()
    {
        if ( nowItem == null ) return;

        if ( nowItem is ConsumableItem consumableItem )
        {
            consumableItem.Use( GameManager.Instance.Player );
        }
        
        OnItemButtonAction?.Invoke();
    }

    public void OnClickEquipItem()
    {
        if ( nowItem == null ) return;

        if ( nowItem is EquipItem equipItem )
        {
            equipItem.Equip( GameManager.Instance.Player );
        }
        
        OnItemButtonAction?.Invoke();
    }

    public void OnClickUnequipItem()
    {
        if ( nowItem == null ) return;
        
        if ( nowItem is EquipItem equipItem )
        {
            equipItem.Equip( GameManager.Instance.Player );
        }
        OnItemButtonAction?.Invoke();
    }

    public void OnClickThrowItem()
    {
        if ( nowItem == null ) return;
        
        
        Vector3 spawnPos = GameManager.Instance.Player.transform.position + (GameManager.Instance.Player.Controller.PlayerForward * 1.0f) + Vector3.up * 1.0f;
        ItemManager.Instance.SpawnDropItem(nowItem.Name, spawnPos);
        nowItem.Throw( GameManager.Instance.Player );

        OnItemButtonAction?.Invoke();
    }
}