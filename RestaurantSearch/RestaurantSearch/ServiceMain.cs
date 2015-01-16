using System;
using OpenTable.Services.Components.Logging;
using OpenTable.Services.Components.Logging.Log4Net;
using log4net.Config;
using OT.Owin.Common.WebAPI;
using Config = OT.Core.Configuration.ServiceConfiguration;
using Newtonsoft.Json;

namespace RestaurantSearch
{
	public class ServiceMain
	{
		private static readonly ILogger logger = new BasicLogger<ServiceMain> ();

		public static void Main(string[] args)
		{
			try
			{
				XmlConfigurator.Configure();
				var service = Service.Create(Config.Instance.Name).SetOptions(args);
				logger.Log(Level.Info, string.Format("Service Information: {0}", service));
				logger.Log(Level.Info, string.Format("Service Configuration: {0}", Config.Instance.ToString()));
				ServiceStarter.Start<SearchApi>(service);
			}
			catch(Exception ex) 
			{
				logger.LogException (Level.Error, ex, string.Empty);
			}
		}
	}
}

