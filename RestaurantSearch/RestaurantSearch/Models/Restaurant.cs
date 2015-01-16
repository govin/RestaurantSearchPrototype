using System;
using Newtonsoft.Json;

namespace RestaurantSearch
{
	public class Restaurant
	{
		[JsonProperty("rid")]
		public string RID { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("address")]
		public Address Address { get; set; } 

		public static Restaurant From(VenueRestaurant vrestaurant)
		{
			var r = new Restaurant ();
			r.RID = vrestaurant.RID.ToString();
			r.Name = vrestaurant.Name;
			r.Address = new Address ();
			r.Address.Street1 = vrestaurant.Address1;
			r.Address.Street2 = vrestaurant.Address2;
			r.Address.City = vrestaurant.City;
			r.Address.Province = vrestaurant.State;
			r.Address.Country = vrestaurant.Country;
			return r;
		}
	}
}

