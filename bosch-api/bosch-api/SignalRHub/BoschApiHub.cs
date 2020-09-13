using BoschApi.Entities.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bosch_api.SignalRHub
{
    public class BoschApiHub: Hub<IBoschApiHubClient>
    {
        private IConfiguration Configuration;
        private readonly ILogger<BoschApiHub> _logger;

        private ApplicationDbContext DbContext { get; set; }

        public BoschApiHub(IConfiguration configuration, ApplicationDbContext applicationDbContext, ILogger<BoschApiHub> logger)
        {
            Configuration = configuration;
            DbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task NewIn(string message)
        {

        }

        public async Task NewOut(string message)
        {

        }

        public async Task CrowdDensityChanged(string message)
        {

        }
    }
}
