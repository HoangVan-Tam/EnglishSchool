using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EnglishSchool.Model.DTOs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EnglishSchool.SignalR
{
    [HubName("AdvisoryHub")]
    public class AdvisoryHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<AdvisoryHub>();

        public void BroadcastCommonData(string data)
        {
            Clients.All.BroadcastCommonData("1321as3dfasdf");
        }


        public static void BroadcastCommonDataStatic(PersonalInformationDTO data,AdvisoryHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            hubContext.Clients.All.Send(data);
        }
        private static IHubConnectionContext<dynamic> GetClients(AdvisoryHub advisoryHub)
        {
            if (advisoryHub == null)
                return GlobalHost.ConnectionManager.GetHubContext<AdvisoryHub>().Clients;
            else
                return advisoryHub.Clients;
        }
    }
}