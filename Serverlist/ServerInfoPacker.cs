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
        public struct _ServerInfo
        {
            public List<string> param;
            public List<string> values;
        }

        public static _ServerInfo GetServerInfo()
        {
            _ServerInfo info = new _ServerInfo();

            info.param = Plugin.Update ? new List<string> { "ip", "players", "playerList", "port", "pastebin", "gameVersion", "info", "privateBeta", "staffRA", "friendlyFire", "geoblocking", "modded", "whitelist", "accessRestrictions", "emailSet", "enforceSameIp", "enforceSameAsn" } : new List<string> { "ip", "players", "port", "enforceSameIp", "enforceSameAsn" };

            if (Plugin.Update)
            {
                Plugin.Debug("Sending Big Dump:");
                Plugin.Debug(ServerConsole.Ip);
                Plugin.Debug($"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}");
                Plugin.Debug(ServerConsole._verificationPlayersList);
                Plugin.Debug(CustomLiteNetLib4MirrorTransport.Singleton.port.ToString());
                Plugin.Debug(GameCore.ConfigFile.ServerConfig.GetString("serverinfo_pastebin_id", "7wV681fT"));
                Plugin.Debug(CustomNetworkManager.CompatibleVersions[0]);
                Plugin.Debug(Misc.Base64Encode(ServerConsole.singleton.RefreshServerNameSafe()));
                Plugin.Debug(CustomNetworkManager.isPrivateBeta.ToString());
                Plugin.Debug(ServerStatic.PermissionsHandler.StaffAccess.ToString());
                Plugin.Debug(ServerConsole.FriendlyFire.ToString());
                Plugin.Debug(((byte)CustomLiteNetLib4MirrorTransport.Geoblocking).ToString());
                Plugin.Debug(CustomNetworkManager.Modded.ToString());
                Plugin.Debug(ServerConsole.WhiteListEnabled.ToString());
                Plugin.Debug(ServerConsole.AccessRestriction.ToString());
                Plugin.Debug(ServerConsole._emailSet.ToString());
                Plugin.Debug(ServerConsole.EnforceSameIp.ToString());
                Plugin.Debug((ServerConsole.EnforceSameAsn).ToString());

                info.values = new List<string> { ServerConsole.Ip, $"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}", ServerConsole._verificationPlayersList, CustomLiteNetLib4MirrorTransport.Singleton.port.ToString(), GameCore.ConfigFile.ServerConfig.GetString("serverinfo_pastebin_id", "7wV681fT"), CustomNetworkManager.CompatibleVersions[0], Misc.Base64Encode(ServerConsole.singleton.RefreshServerNameSafe()), CustomNetworkManager.isPrivateBeta.ToString(), ServerStatic.PermissionsHandler.StaffAccess.ToString(), ServerConsole.FriendlyFire.ToString(), ((byte)CustomLiteNetLib4MirrorTransport.Geoblocking).ToString(), CustomNetworkManager.Modded.ToString(), ServerConsole.WhiteListEnabled.ToString(), ServerConsole.AccessRestriction.ToString(), ServerConsole._emailSet.ToString(), ServerConsole.EnforceSameIp.ToString(), ServerConsole.EnforceSameAsn.ToString() };
            }
            else
            {

                Plugin.Debug("Sending Smol Dump:");
                Plugin.Debug(ServerConsole.Ip);
                Plugin.Debug($"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}");
                Plugin.Debug(CustomLiteNetLib4MirrorTransport.Singleton.port.ToString());
                Plugin.Debug(ServerConsole.EnforceSameIp.ToString());
                Plugin.Debug(ServerConsole.EnforceSameAsn.ToString());

                info.values = new List<string> { ServerConsole.Ip, $"{ServerConsole.PlayersAmount}/{CustomNetworkManager.slots}", CustomLiteNetLib4MirrorTransport.Singleton.port.ToString(), ServerConsole.EnforceSameIp.ToString(), ServerConsole.EnforceSameAsn.ToString() };
            }

            Plugin.Update = false;
            return info;
        }
    }
}
