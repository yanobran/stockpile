using StockPile.Model.Fulfillment;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Command
{
    public struct Test2 : ICommand
    {
        public Test2(string value)
        {
            this.Value = value;
        }

        public string Value;
    }

    public struct Test : ICommand
    {
        public Test(string value)
        {
            this.Value = value;
        }

        public string Value;
    }

    public struct SubmitCart : ICommand
    {
        public SubmitCart(ShoppingCart cart)
        {
            this.Cart = cart;
        }
        
        public ShoppingCart Cart;
    }

    public struct ProcessOrder : ICommand
    {
        public ProcessOrder(Guid orderId)
        {
            this.OrderId = orderId;
        }

        public Guid? OrderId;
    }
}
