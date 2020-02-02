using System;
using EXILED;
using Serverlist.Extensions;
using MEC;
using System.Collections.Generic;

namespace Serverlist
{
	public class Plugin : EXILED.Plugin
	{
		private static string ServerAuthUrl = "https://api.southwoodstudios.com/addservertolist/";
		public static bool Update = true;

		public override void OnEnable()
		{
			try
			{
				Debug("Initializing event handlers..");

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
				ServerInfoPacker._ServerInfo info = ServerInfoPacker.GetServerInfo();
				Debug(WebExtensions.WebRequest(ServerAuthUrl, info.param, info.values));
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
}