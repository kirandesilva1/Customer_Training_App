using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using IO.Swagger.Models;
using IO.Swagger.Services;
using MongoDB.Driver.Core.WireProtocol.Messages;

namespace IO.Swagger.BusinessLayer
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderService _orderService;
        public OrderManager(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // TODO: Order Manager should not be returning HttpStatusCode and Result this should be in the controller 
        // Separation of concerns issue
        public (HttpStatusCode, IEnumerable<Order>) GetOrders()
        {
            HttpStatusCode statusCodeResult;
            IEnumerable<Order> _returnOrders;
           
            IEnumerable<Order> _orders = _orderService.GetOrders();

            if (_orders.Any())
            {
                statusCodeResult = HttpStatusCode.OK;
                _returnOrders = _orders;
            }
            else
            {
                statusCodeResult = HttpStatusCode.Forbidden;
                _returnOrders = _orders;
            }
            
            return (statusCodeResult,_returnOrders);

        }

        public Order GetOrder(string orderId)
        {
            Order _order = _orderService.GetOrder(orderId);
            
            return  _order;
        }

        // TODO: Order Manager should not be returning HttpStatusCode and Result this should be in the controller 
        // Separation of concerns issue
        public (HttpStatusCode, string Message) ShipOrder(string orderId)
        {
            Order _order = _orderService.GetOrder(orderId);

            HttpStatusCode statusCodeResult;
            string message;
            
            if (_order.Customer.Billing == null)
            {
                statusCodeResult = HttpStatusCode.Forbidden;
                message = "Please enter billing info!";
            }
            else
            {
                statusCodeResult = HttpStatusCode.OK;
                message = "Billing info available";
            }
            
            return  (statusCodeResult,  message);

        }
    }
}