(function() 
{
	var app = angular.module('parkingDashboard', []);


	app.controller('ParkingController', function()
	{
		this.garages = garages;
	});
	
	
	
	var garages = [
	    {name:"One Campus Martius", address:"1188 Farmer St, Detroit MI 48226", load:"low"},
	    {name:"Premier", address:"1206 Woodward Ave, Detroit MI 48226", load:"high"},
	    {name:"1001 Woodward", address:"1001 Woodward Ave, Detroit MI 48226", load:"low"},
	    {name:"Book Cadillac", address:"150 Michigan Ave, Detroit MI 48226", load:"medium"},
	    {name:"1 Detroit", address:"200 Larned St, Detroit MI 48226", load:"low"},
	    {name:"2 Detroit", address:"160 E Congress St, Detroit MI 48226", load:"medium"},
	    {name:"COBO Congress", address:"467 W Congress St, Detroit MI 48226", load:"full"},
	    {name:"COBO Rooftop", address:"625 W Congress St, Detroit MI 48226", load:"medium"},
	    {name:"COBO Washington", address:"475 Washington Ave, Detroit MI 48226", load:"low"},
	    {name:"Detroit Opera House", address:"1426 Broadway St, Detroit MI 48226", load:"high"},
	    {name:"Financial District", address:"730 Shelby St, Detroit MI 48226", load:"low"},
	    {name:"AT&T Lot", address:"421 Bagley St, Detroit MI 48226", load:"low"},
	    {name:"Brush St / Greektown", address:"1001 Brush St, Detroit MI 48226", load:"medium"},
	    {name:"Fort St / Greektown", address:"419 E Fort St, Detroit MI 48226", load:"full"},
	    {name:"First National Garage", address:"660 Woodward Ave, Detroit MI 48226", load:"low"},
	    {name:"The Z", address:"1234 Library St, Detroit MI 48226", load:"low"},
	    {name:"Howard Lot A", address:"1240 Abbot St, Detroit MI 48226", load:"high"},
	    {name:"Howard Lot B", address:"1300 Abbot St, Detroit MI 48226", load:"medium"},
	    {name:"Howard Lot C", address:"1100 Brooklyn St, Detroit MI 48226", load:"low"}
	];
	
	
	
	
	
	
	
	/*
	garageMarkers = [];
	geocoder = new google.maps.Geocoder();

	function AddGarage(name, address, load, garageNumber)
	{
		//TODO: Geocoding should only be done once upon garage entry into the database, not on each page load!!!
		//Convert Address to Latitude & Longitude and Create Marker/Marker Listener
		geocoder.geocode( { 'address': address }, function(results, status) 
		{
			if (status == google.maps.GeocoderStatus.OK) 
			{
				//Create Marker
				garageMarkers[garageNumber] = new google.maps.Marker({
					map: map,
					position: results[0].geometry.location,
				});

				if (load == 'low')
				{
					garageMarkers[garageNumber].setIcon('parking_logo_green.png'); //TODO: Fix click zone
				}
				else if (load == 'medium')
				{
					garageMarkers[garageNumber].setIcon('parking_logo_yellow.png');
				}
				else if (load == 'high')
				{
					garageMarkers[garageNumber].setIcon('parking_logo_red.png');
				}
				else if (load == 'full')
				{
					garageMarkers[garageNumber].setIcon('parking_logo_black.png');
				}
				else
				{
					console.log("load read error for " + name);
				}

				//Create Marker Listener
				google.maps.event.addListener(garageMarkers[garageNumber], 'click', function() {
					//TODO: Change sideBar
				});
			} 
			else 
			{
				//alert('Geocode was not successful for the following reason: ' + status);
			};
		});


		//Create Dropdown Entry
		// var newButton = $("<li id='garageButton" + garageNumber + "'></li>");
		// var newAnchor = $("<a href='#'>" + name + "</a>");
		
		// newButton.html(newAnchor);
		// $('#dropdownTopBar').append(newButton);


		//Make Dropdown Entry Do Something //TODO: no worky, use angularJS
		// $("#garageButton" + garageNumber).on('click', function() 
		// {
		// 	alert("here I am!"); //TODO
  // 			//map.setZoom(16);
  //   		//map.setCenter(garageMarkers[garageNumber].getPosition());
  // 		});


		// var newButton = $("<li id='garageButton" + garageNumber + "'></li>");
		// var newAnchor = $("<a href='#'>" + name + "</a>")
		// console.log("here");
		// console.log(newAnchor)
		
		// newButton.html(newAnchor);
		// $('#dropdownTopBar').append(newButton);

		// var c = $('#garageButton' + garageNumber).find('a');
		// c.on('click', function(event) {
		// 	event.preventDefault();
		// 	console.log("CLICK");
		// })

	};


   



	// var garagesArray = [];
   
	// //Read Garages From JSON file
	// $.getJSON('garages.json', function(data) 
	// {    
	// 	for(var i=0; i<data.garage.length; i++)
	// 	{
	// 		var garagesArray[i] = data.garage[i];
	// 		AddGarage(garagesArray[i].name, garagesArray[i].address, garagesArray[i].load, i);
	// 	}
	// });
	*/
	
	
	
	
	
	
	


})();