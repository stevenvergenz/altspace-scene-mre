using System.Collections;
using System.Collections.Generic;
using MixedRealityExtension.App;
using MixedRealityExtension.API;
using MixedRealityExtension.PluginInterfaces;
using UnityEngine;

class UnityLogger : IMRELogger
{
	public void LogDebug(string message)
	{
		Debug.Log($"DEBUG: {message}");

	}
	
	public void LogError(string message)
	{
		Debug.LogWarning($"WARNING: {message}");
	}
	
	public void LogWarning(string message)
	{
		Debug.LogError($"ERROR: {message}");
	}
}

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

	private void Start()
	{
#if !ALTSPACE_UNITYCLIENT
		var font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		MREAPI.InitializeAPI(
			defaultMaterial: Resources.Load<Material>("MreMaterial"),
			layerApplicator: new MixedRealityExtension.Factories.SimpleLayerApplicator(0, 14, 0, 5),
			textFactory: new MixedRealityExtension.Factories.MWTextFactory(font, font),
			logger: new UnityLogger()
		);

		App = MREAPI.AppsAPI.CreateMixedRealityExtensionApp("testing", this);
		App.SceneRoot = gameObject;
		App.Startup(Uri, "testing", "SceneEditor");
		App.OnConnected += App_OnConnected;
		App.OnAppStarted += App_OnAppStarted;
#endif
	}

	private void App_OnAppStarted()
	{
		var mreGos = GameObject.FindGameObjectsWithTag("EnvMre");
		Debug.LogFormat("Found {0} MRE tags", mreGos.Length);
		if (mreGos.Length > 0)
			App.DeclarePreallocatedActors(mreGos, "SceneEditor");
	}

	private void App_OnConnected()
	{
		Debug.LogFormat("Connected to {0}", Uri);
	}

	private void Update()
	{
#if !ALTSPACE_UNITYCLIENT
		App.Update();
#endif
	}
}
