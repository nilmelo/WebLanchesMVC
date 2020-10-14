using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebLanchesMVC.Models
{
    public class Order
    {
		public int Id { get; set; }

		public List<OrderDetail> OrderItems { get; set; }

		[Required(ErrorMessage = "Informe o nome")]
		[Display(Name = "Nome")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Informe o sobrenome")]
		[Display(Name = "Sobrenome")]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Informe o endereço")]
		[Display(Name = "Endereço")]
		[StringLength(100)]
		public string Address1 { get; set; }

		[Required(ErrorMessage = "Informe o complemento do endereço")]
		[Display(Name = "Complemento")]
		[StringLength(100)]
		public string Address2 { get; set; }

		[Required(ErrorMessage = "Informe o CEP")]
		[Display(Name = "CEP")]
		[StringLength(10, MinimumLength = 8)]
		public string Cep { get; set; }

		[StringLength(50)]
		public string State { get; set; }

		[StringLength(50)]
		public string City { get; set; }

		[Required(ErrorMessage = "Informe o telefone")]
		[StringLength(25)]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Telefone")]
		public string Telephone { get; set; }

		[StringLength(80)]
        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
			ErrorMessage = "O e-mail não possui um formato correto")]
		public string Email { get; set; }

		[BindNever]
		public decimal OrderTotal { get; set; }

		[Display(Name = "Data/Hora de Recebimento do Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
		public DateTime OrderSent { get; set; }

		[Display(Name = "Data/Hora de Recebimento do Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
		public DateTime? OrderDelivered { get; set; }
    }
}
