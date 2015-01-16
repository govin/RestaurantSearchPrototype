using System;
using Newtonsoft.Json;

namespace RestaurantSearch
{
	public class Address
	{
		[JsonProperty("street1")]
		public string Street1 { get; set; }

		[JsonProperty("street2")]
		public string Street2 { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("province")]
		public string Province { get; set; }

		[JsonProperty("provinceCode")]
		public string ProvinceCode { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("countryCode")]
		public string CountryCode { get; set; }

		[JsonProperty("postalCode")]
		public string PostalCode { get; set; }

		public static Address From(string street1 = "", string street2 = "", string city = "", string province = "", string country = "", string countryCode = "", string postalCode="")
		{
			return new Address { Street1 = street1, Street2 = street2, City = city, Province = province, Country = country, CountryCode = countryCode, PostalCode = postalCode};
		}
	}
}

