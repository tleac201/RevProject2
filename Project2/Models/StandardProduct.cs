using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.DAL;
namespace Project2
{
	public partial class StandardProduct
	{
		public decimal Ini(IStandardProductRepo standardProductRepo)
		{
			Price = standardProductRepo.RetrieveById(Id).Price;
			return Price;
		}
	}
}