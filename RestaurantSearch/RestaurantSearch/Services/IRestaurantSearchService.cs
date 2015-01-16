using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace RestaurantSearch
{
	public interface IRestaurantSearchService
	{
		List<Restaurant> Index();

		List<Restaurant> Search(string q);
	}
}

