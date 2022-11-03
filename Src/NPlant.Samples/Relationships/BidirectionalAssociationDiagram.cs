// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="BidirectionalAssociationDiagram.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NPlant.Samples.Relationships
{
    public class BidirectionalAssociationDiagram : ClassDiagram
    {
        #region Constructors and Destructors

        public BidirectionalAssociationDiagram()
        {
            base.AddClass<Order>();
        }

        #endregion
    }

    public class Order
    {
        #region Fields

        public long Id;

        public IList<OrderItem> Items;

        #endregion
    }

    public class OrderItem
    {
        #region Fields

        public long Id;

        public Order Order;

        public Price Price;

        public Product Product;

        #endregion
    }

    public class Product
    {
        #region Fields

        public string Description;

        public long Id;

        public string Name;

        #endregion
    }

    public class Price
    {
        #region Fields

        public decimal Amount;

        public string Description;

        public long Id;

        public string Name;

        #endregion
    }
}