using System;
using EXILED;
using Serverlist.Extensions;
using MEC;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Serverlist
{
	public class Plugin : EXILED.Plugin
	{
		private static string ServerAuthUrl = "https://api.southwoodstudios.com/addservertolist/";
		public static bool Update = true;

		private string apiToken;
		private string version = "1.2";

		public override void OnEnable()
		{
			if(!Plugin.Config.GetBool("sw_enable", true))
			{
				return;
			}

			try
			{
				Debug("Initializing event handlers..");

				apiToken = Plugin.Config.GetString("sw_api", null);

				Timing.RunCoroutine(UpdatePlayerlist());
				Timing.RunCoroutine(SetUpdate());

				Info($"Serverlist loaded.");
			}
			catch (Exception e)
			{
				Error($"There was an error loading the plugin: {e}");
			}
		}

		private IEnumerator<float> SetUpdate()
		{
			while (true)
			{
				yield return Timing.WaitForSeconds(300);
				Update = true;
			}
		}

		private IEnumerator<float> UpdatePlayerlist()
		{
			yield return Timing.WaitForSeconds(3);
			while (true)
			{
				ServerInfoPacker._ServerInfo info = ServerInfoPacker.GetServerInfo(apiToken, version);
				string response = WebExtensions.WebRequest(ServerAuthUrl, info.param, info.values);
				JObject o = JObject.Parse(response);
				ListResponse lr = o.ToObject<ListResponse>();

				Debug($"Dump info: Type: {lr.type}, Success: {lr.success}.");
				if (lr.update)
				{
					Info($"Please update to the latest version of the serverlist for best compatibility. (Latest version: {lr.latestVersion}, Your version: {version})");
				}
				if (lr.error != null)
				{
					Error(lr.error);
				}

				//Debug(WebExtensions.WebRequest(ServerAuthUrl, info.param, info.values));
				yield return Timing.WaitForSeconds(30);
			}
		}

		public override void OnDisable()
		{
			
		}

		public override void OnReload()
		{
			
		}

		public override string getName { get; } = "Serverlist";
	}

	public class ListResponse
	{
		public bool success = false;
		public int type = 0;
		public string error = null;
		public bool update = false;
		public string latestVersion = null;
	}
}