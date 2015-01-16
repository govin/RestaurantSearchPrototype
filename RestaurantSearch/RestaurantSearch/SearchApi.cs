using System;
using OT.Owin.Common.WebAPI;
using SimpleInjector;

namespace RestaurantSearch
{
	public class SearchApi : ApiBase
	{
		public override void AddServices (SimpleInjector.Container container)
		{
			container.RegisterWebApiRequest<IRestaurantSearchService, RestaurantSearchService>();
		}
	}
}

