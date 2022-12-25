﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microservice.Smart.Services.GoogleMapInfo.Models.Responses
{
	public class Element
	{
		[JsonPropertyName("distance")]
		public Distance Distance { get; set; }
		
		[JsonPropertyName("duration")]
		public Duration Duration { get; set; }
		
		[JsonPropertyName("status")]
		public string Status { get; set; }
	}
}
