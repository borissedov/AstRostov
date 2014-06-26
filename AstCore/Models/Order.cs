using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstCore.Models
{
    public class Order
    {
        public Order()
        {
            OrderLineItems = new Collection<OrderLineItem>();
        }

        [Key]
        public int OrderId { get; set; }

        public virtual User Account { get; set; }

        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public ShippingType ShippingType { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        #region Totals

        [Required]
        public decimal ShippingCost { get; set; }

        [Required]
        public decimal ItemsSubtotal { get; set; }

        [Required]
        public decimal DiscountTotal { get; set; }

        [Required]
        public decimal CommissionTotal { get; set; }

        [Required]
        public decimal Total { get; set; }

        #endregion

        #region Address

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string DocumentType { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        #endregion

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CustomerComment { get; set; }

        public string AdminComment { get; set; }

        public virtual Preorder Preorder { get; set; }

    }

    public enum PaymentMethod
    {
        [Description("Оплата банковским переводом")]
        BankTransfer = 1,

        [Description("Оплата через систему Robokassa")]
        Robokassa = 2
    }

    public enum ShippingType
    {
        [Description("Самовывоз")]
        PickUp = 1,

        [Description("Транспортная компания")]
        ShippingCompany = 2
    }

    public enum OrderState
    {
        [Description("Ожидает оплаты")]
        Pending = 1,

        [Description("Оплата отправлена")]
        PaymentSent = 2,

        [Description("Оплата подтверждена")]
        PaymentConfirmed = 3,

        [Description("Отправлен")]
        Shipped = 4,

        [Description("Получен")]
        Recieved = 5,

        [Description("Отклонен")]
        Declined = 6
    }
}
