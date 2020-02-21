using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.CommandInterpolation;
using EXILED;

namespace Serverlist
{
    static class ServerInfoPacker
    {
        public struct ServerInfo
        {
            public List<string> param;
            public List<string> values;
        }

        private static string ipAddress;

        public static ServerInfo GetServerInfo(string token = "null", string version = "0.0")
        {
            ServerInfo info = new ServerInfo();

            if (ipAddress == null)
            {
                ipAddress = GameCore.ConfigFile.ServerConfig.GetString("server_ip", "127.0.0.1");
            }

            info.param = Plugin.Update ? new List<string> { "ip", "players", "playerList", "port", "pastebin", "gameVersion", "info", "privateBeta", "staffRA", "friendlyFire", "geoblocking", "modded", "whitelist", "accessRestrictions", "emailSet", "enforceSameIp", "enforceSameAsn", "maxPlayers", "curPlayers", "pluginVersion" } : new List<string> { "ip", "players", "port", "enforceSameIp", "enforceSameAsn", "curPlayers" };

            if (Plugin.Update)
            {
                Log.Debug("Sending Big Dump:");
                Log.Debug(ipAddress);
                Log.Debug($"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}");
                Log.Debug(ServerConsole._verificationPlayersList);
                Log.Debug(CustomLiteNetLib4MirrorTransport.Singleton.port.ToString());
                Log.Debug(GameCore.ConfigFile.ServerConfig.GetString("serverinfo_pastebin_id", "7wV681fT"));
                Log.Debug(CustomNetworkManager.CompatibleVersions[0]);
                Log.Debug(Misc.Base64Encode(ServerConsole.singleton.RefreshServerNameSafe()));
                Log.Debug(CustomNetworkManager.isPrivateBeta.ToString());
                Log.Debug(ServerStatic.PermissionsHandler.StaffAccess.ToString());
                Log.Debug(ServerConsole.FriendlyFire.ToString());
                Log.Debug(((byte)CustomLiteNetLib4MirrorTransport.Geoblocking).ToString());
                Log.Debug(CustomNetworkManager.Modded.ToString());
                Log.Debug(ServerConsole.WhiteListEnabled.ToString());
                Log.Debug(ServerConsole.AccessRestriction.ToString());
                Log.Debug(ServerConsole._emailSet.ToString());
                Log.Debug(ServerConsole.EnforceSameIp.ToString());
                Log.Debug((ServerConsole.EnforceSameAsn).ToString());

                info.values = new List<string> { ipAddress, $"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}", ServerConsole._verificationPlayersList, CustomLiteNetLib4MirrorTransport.Singleton.port.ToString(), GameCore.ConfigFile.ServerConfig.GetString("serverinfo_pastebin_id", "7wV681fT"), CustomNetworkManager.CompatibleVersions[0], Misc.Base64Encode(ServerConsole.singleton.RefreshServerNameSafe()), CustomNetworkManager.isPrivateBeta.ToString(), ServerStatic.PermissionsHandler.StaffAccess.ToString(), ServerConsole.FriendlyFire.ToString(), ((byte)CustomLiteNetLib4MirrorTransport.Geoblocking).ToString(), CustomNetworkManager.Modded.ToString(), ServerConsole.WhiteListEnabled.ToString(), ServerConsole.AccessRestriction.ToString(), ServerConsole._emailSet.ToString(), ServerConsole.EnforceSameIp.ToString(), ServerConsole.EnforceSameAsn.ToString(), CustomNetworkManager.slots.ToString(), ServerConsole.PlayersAmount.ToString(), version };
                info.param.Add("apiToken");
                info.values.Add(token);
            }
            else
            {

                Log.Debug("Sending Smol Dump:");
                Log.Debug(ipAddress);
                Log.Debug($"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}");
                Log.Debug(CustomLiteNetLib4MirrorTransport.Singleton.port.ToString());
                Log.Debug(ServerConsole.EnforceSameIp.ToString());
                Log.Debug(ServerConsole.EnforceSameAsn.ToString());

                info.values = new List<string> { ipAddress, $"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}", CustomLiteNetLib4MirrorTransport.Singleton.port.ToString(), ServerConsole.EnforceSameIp.ToString(), ServerConsole.EnforceSameAsn.ToString(), ServerConsole.PlayersAmount.ToString() };
            }

            Plugin.Update = false;
            return info;
        }
    }
}
