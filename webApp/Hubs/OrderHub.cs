using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Logging;

namespace webApp.Hubs
{
    public class OrderHub : Hub
    {
        private readonly ILogger<OrderHub> _logger;

        public OrderHub(ILogger<OrderHub> logger)
        {
            _logger = logger;
        }
        public void RefreshOrders()
        {
            try
            {
                Clients.All.send("refreshOrders");
                _logger.LogInformation("Orders refreshed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while refreshing orders");
                throw;
            }
        }
    }
}
