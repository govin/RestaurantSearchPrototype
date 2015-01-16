using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Globalization;
using System.Threading;
using NodaTime;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace RestaurantSearch
{
	public class SearchController : ApiController
	{
		private readonly IRestaurantSearchService service;


		public SearchController(IRestaurantSearchService service)
		{
			this.service = service;
		}

		[Route("api/indexRestaurants")]
		[HttpPost]
		public List<Restaurant> PostIndex()
		{	
			var restaurants = service.Index ();
			return restaurants;
		}

		[Route("api/search")]
		[HttpGet]
		public HttpResponseMessage GetRestaurants(string q)
		{
			var results = service.Search (q);
			return Request.CreateResponse (HttpStatusCode.OK, results);
		}
	}
}

