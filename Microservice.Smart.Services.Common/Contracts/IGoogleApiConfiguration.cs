using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Smart.Services.Common.Contracts
{
	public interface IGoogleApiConfiguration
	{
		public string GoogleApiBaseUrl { get; set; }	
		public string GoogleApiKey { get; set; }	
		public string GoogleApiUrl { get; set; }	
		public string GoogleApiUrlTemplate { get; set; }
	}
}
