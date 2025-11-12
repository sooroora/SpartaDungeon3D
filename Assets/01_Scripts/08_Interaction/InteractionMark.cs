using System;
using UnityEngine;

public enum InteractionMarkType
{
    Question = 0,
    Alert,
    Interaction,
}

public class InteractionMark : MonoBehaviour
{
    private Transform target;

    [ SerializeField ] SpriteRenderer spriteRenderer;
    [ SerializeField ] Sprite[]       sprites;

    private void Awake()
    {
        gameObject.SetActive( false );
    }

    public void Show( Transform _target, InteractionMarkType type )
    {
        target = _target;
        if ( target != null ) this.transform.position = target.position + ( Vector3.up * 2.0f );

        spriteRenderer.sprite = sprites[ ( int )type ];
        gameObject.SetActive( true );
    }

    private void Update()
    {
        if ( target == null ) return;
        this.transform.position = target.position + ( Vector3.up * 2.0f );
    }

    public void Hide()
    {
        gameObject.SetActive( false );
    }
}