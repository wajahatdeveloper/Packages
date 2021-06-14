using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 GetRandomPointInsideCollider( this BoxCollider boxCollider )
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y ),
            Random.Range( -extents.z, extents.z )
        )  + boxCollider.center;
        return boxCollider.transform.TransformPoint( point );
    }
    
    public static Vector2 GetRandomPointInsideCollider( this BoxCollider2D boxCollider )
    {
        Vector2 extents = boxCollider.size / 2f;
        Vector2 point = new Vector2(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y )
        )  + boxCollider.offset;
        return boxCollider.transform.TransformPoint( point );
    }
}