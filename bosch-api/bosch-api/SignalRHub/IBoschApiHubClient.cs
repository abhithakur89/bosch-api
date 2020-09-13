using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bosch_api.SignalRHub
{
    public interface IBoschApiHubClient
    {
        Task NewIn();
        Task NewOut();
        Task CrowdDensityChanged(string density);
    }
}
