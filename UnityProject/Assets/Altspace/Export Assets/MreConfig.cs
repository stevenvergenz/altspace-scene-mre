using System.Collections;
using System.Collections.Generic;
using MixedRealityExtension.App;
using MixedRealityExtension.API;
using UnityEngine;

public class MreConfig : MonoBehaviour
{
    /// <summary>
    /// The URI to an MRE
    /// </summary>
    [Tooltip("The URI to an MRE")]
    [SerializeField]
    private string Uri = "ws://localhost:3901";

    /// <summary>
    /// Whether that MRE needs user data to operate
    /// </summary>
    [Tooltip("Whether that MRE needs user data to operate")]
    [SerializeField]
    private bool NeedsUserData = false;

    private IMixedRealityExtensionApp App;

    /* private void Start()
    {
        var font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        MREAPI.InitializeAPI(
            defaultMaterial: Resources.Load<Material>("MreMaterial"),
            layerApplicator: new MixedRealityExtension.Factories.SimpleLayerApplicator(0, 14, 0, 5),
            textFactory: new MixedRealityExtension.Factories.MWTextFactory(font, font)
        );

        App = MREAPI.AppsAPI.CreateMixedRealityExtensionApp("testing", this);
        App.Startup(Uri, "testing", "SceneEditor");
        App.OnConnected += App_OnConnected;
    }

    private void App_OnConnected()
    {
        Debug.LogFormat("Connected to {0}", Uri);
    }

    private void Update()
    {
        App.Update();
    } */
}
