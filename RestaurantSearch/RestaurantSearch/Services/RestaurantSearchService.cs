using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Providers;
using Nest;
using OT.Core.Configuration;

namespace RestaurantSearch
{
	public class RestaurantSearchService : IRestaurantSearchService
	{
		private IServiceConfiguration config;

		private readonly string restaurantsSourceUrl; 

		private readonly string elasticSearchUrl;

		public RestaurantSearchService(IServiceConfiguration config)
		{
			this.config = config;
			this.restaurantsSourceUrl = (string) config.Items ["RestaurantSourceUrl"];
			this.elasticSearchUrl = (string)config.Items ["ElasticSearchUrl"];
		}

		public List<Restaurant> Search (string term)
		{
			var node = new Uri(elasticSearchUrl);
			var settings = new ConnectionSettings(
				node, 
				defaultIndex: "places"
			);
			var client = new ElasticClient(settings);

			var fields = new List<string> ();
			fields.Add ("name");
			fields.Add ("rid");
			fields.Add ("address.city");
			fields.Add ("address.province");
			fields.Add ("address.country");

			var restaurants = client.Search<Restaurant> (s => s.Type ("venue")
				.From (0)
				.Size (1000)
				.Query (q => q.MultiMatch 
					(mq => mq.Type (TextQueryType.PhrasePrefix).OnFields (fields).Query (term))))
				.Documents
				.ToList ();

			return restaurants;
		}

		public List<Restaurant> Index ()
		{
			var searchIndexUrl = elasticSearchUrl + "/places/venue";
			using (var client = new HttpClient ()) 
			{
				var response = client.GetStringAsync (restaurantsSourceUrl).Result;
				var vrestaurants = JsonConvert.DeserializeObject<List<VenueRestaurant>> (response);
				var restaurants = vrestaurants.Select (x => Restaurant.From (x)).ToList ();

				foreach (var restaurant in restaurants) 
				{
					var response1 = client.PostAsJsonAsync (searchIndexUrl, restaurant).Result;
				}

				return restaurants;
			}
		}
	}
}
