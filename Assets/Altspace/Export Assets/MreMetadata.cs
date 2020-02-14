using UnityEngine;

/// <summary>
/// Configure an MRE that binds to the scene
/// </summary>
[CreateAssetMenu(menuName = "MRE Configuration")]
public class MreMetadata : ScriptableObject
{
    /// <summary>
    /// A URI to the MRE server
    /// </summary>
    [Tooltip("A URI to the MRE server")]
    public string Uri;

    /// <summary>
    /// Whether the MRE should track users
    /// </summary>
    [Tooltip("Whether the MRE should track users")]
    public bool TrackUsers;
}
