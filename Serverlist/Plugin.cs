using System;
using EXILED;
using Serverlist.Extensions;
using MEC;
using System.Collections.Generic;
using Utf8Json;
using System.Text;

namespace Serverlist
{
	public class Plugin : EXILED.Plugin
	{
		private static string ServerAuthUrl = "https://api.southwoodstudios.com/addservertolist/";
		public static bool Update = true;

		private string apiToken = "null";
		private string version = "1.3";

		public override void OnEnable()
		{
			if(!Plugin.Config.GetBool("sw_enable", true))
			{
				return;
			}

			try
			{
				Debug("Initializing event handlers..");

				apiToken = Plugin.Config.GetString("sw_api", "null");

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
			while (ServerConsole.Ip == null)
			{
				yield return Timing.WaitForSeconds(5);
			}
			while (true)
			{
				ServerInfoPacker._ServerInfo info = ServerInfoPacker.GetServerInfo(apiToken, version);
				byte[] response = null;
				try
				{
					response = WebExtensions.WebRequestBytes(ServerAuthUrl, info.param, info.values);
				}
				catch (Exception e)
				{
					Error($"[Web error] {e.Message}");
				}
				if (response != null)
				{
					try
					{
						ListResponse lr = JsonSerializer.Deserialize<ListResponse>(response);

						Debug($"Dump info: Type: {lr.type}, Success: {lr.success}.");
						if (lr.update)
						{
							Info($"Please update to the latest version of the serverlist for best compatibility. (Latest version: {lr.latestVersion}, Your version: {version})");
						}
						if (lr.error != null)
						{
							Error(lr.error);
						}
					}
					catch (Exception e)
					{
						Error($"[Json error] {e.Message}\n{Encoding.UTF8.GetString(response)}");
					}
				}
				
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