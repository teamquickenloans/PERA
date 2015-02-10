(function() 
{
	var app = angular.module('parkingDashboard', [ ]);


	app.controller('GarageController', function()
	{
		this.
	});
	
	
	
	
	
	
	
	
	
	
	
	
	
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
		var newButton = $("<li id='garageButton" + garageNumber + "'></li>");
		var newAnchor = $("<a href='#'>" + name + "</a>");
		
		newButton.html(newAnchor);
		$('#dropdownTopBar').append(newButton);


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


   



	var garagesArray = [];
   
	//Read Garages From JSON file
	$.getJSON('garages.json', function(data) 
	{    
		for(var i=0; i<data.garage.length; i++)
		{
			var garagesArray[i] = data.garage[i];
			AddGarage(garagesArray[i].name, garagesArray[i].address, garagesArray[i].load, i);
		}
	});
	
	
	
	
	
	
	
	


})