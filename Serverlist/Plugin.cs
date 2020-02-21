using System;
using EXILED;
using Serverlist.Extensions;
using MEC;
using System.Collections.Generic;
using Utf8Json;
using System.Text;
using System.Threading;

namespace Serverlist
{
	public class Plugin : EXILED.Plugin
	{
		private static readonly string ServerAuthUrl = "https://api.southwoodstudios.com/addservertolist/";
		public static bool Update = true;

		private string apiToken = "null";
		private readonly string version = "2.0.0";

		private Thread updatePlayerlist;
		private Thread setUpdate;

		public override void OnEnable()
		{
			if(!Plugin.Config.GetBool("sw_enable", true))
			{
				return;
			}

			try
			{
				Log.Debug("Initializing event handlers..");

				apiToken = Plugin.Config.GetString("sw_api", "null");

				updatePlayerlist = new Thread(UpdatePlayerlist);
				setUpdate = new Thread(SetUpdate);
				updatePlayerlist.Start();
				setUpdate.Start();

				Log.Info($"Serverlist loaded.");
			}
			catch (Exception e)
			{
				Log.Error($"There was an error loading the plugin: {e}");
			}
		}

		private void SetUpdate()
		{
			while (true)
			{
				Thread.Sleep(300 * 1000);
				Update = true;
			}
		}

		private void UpdatePlayerlist()
		{
			while (ServerConsole.Ip == null)
			{
				Thread.Sleep(5 * 1000);
			}
			while (true)
			{
				ServerInfoPacker.ServerInfo info = ServerInfoPacker.GetServerInfo(apiToken, version);
				byte[] response = null;
				try
				{
					response = WebExtensions.WebRequestBytes(ServerAuthUrl, info.param, info.values);
				}
				catch (Exception e)
				{
					Log.Error($"[Web error] {e.Message}");
				}
				if (response != null)
				{
					try
					{
						ListResponse lr = JsonSerializer.Deserialize<ListResponse>(response);

						Log.Debug($"Dump info: Type: {lr.type}, Success: {lr.success}.");
						if (lr.update)
						{
							Log.Info($"Please update to the latest version of the serverlist for best compatibility. (Latest version: {lr.latestVersion}, Your version: {version})");
						}
						if (lr.error != null)
						{
							Log.Error(lr.error);
						}
					}
					catch (Exception e)
					{
						Log.Error($"[Json error] {e.Message}\n{Encoding.UTF8.GetString(response)}");
					}
				}

				Thread.Sleep(30 * 1000);
			}
		}

		public override void OnDisable()
		{
			updatePlayerlist.Abort();
			setUpdate.Abort();
			updatePlayerlist = null;
			setUpdate = null;
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