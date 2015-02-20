/**
* Map controller
* @namespace pera.dashboard.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.dashboard.controllers')
      .controller('MapController', MapController);

    MapController.$inject = ['$scope','Garages', 'Snackbar'];

    /**
    * @namespace MapController
    */
    function MapController($scope, Garages, Snackbar) {
        var vm = this;
        vm.map;
        vm.garages = [];
        vm.garageMarkers = [];
        vm.initialized = false; //tracks if the maps has already been initialized
        vm.garages = []; //the list of garages to be returned

        //initialize the map
        this.initialize = initialize;

          function initialize() {
              var styleArray = [
                  {
                      featureType: "water",
                      stylers: [
                          { color: '#666666' }
                      ]
                  }, {
                      featureType: "all",
                      stylers: [
                          { saturation: -25 }
                      ]
                  }, {
                      featureType: "road",
                      elementType: "geometry",
                      stylers: [
                          { visibility: "simplified" }
                      ]
                  }, {
                      featureType: "poi.business",
                      elementType: "labels",
                      stylers: [
                          { visibility: "off" }
                      ]
                  }
              ];

              var mapOptions = {
                  center: { lat: 42.33242, lng: -83.04646 },
                  zoom: 16,
                  styles: styleArray,
                  panControl: false,
                  zoomControl: true,
                  mapTypeControl: true,
                  scaleControl: false,
                  streetViewControl: false,
                  overviewMapControl: false
              };

              this.map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

              Garages.all().then(garagesSuccessFn, garagesErrorFn);

              this.initialized = true;
          } //end intialize() function


          function garagesSuccessFn(data, status, headers, config) {
              vm.garages = data.data;         //this will depend on what the API returns, it may have to change
              PlaceMarkers(vm.map, vm.garages, vm.garageMarkers);
          }

          function garagesErrorFn(data, status, headers, config) {
              Snackbar.error(data.data.error);
          }

          function PlaceMarkers(map, garages, garageMarkers) {
              for (var i = 0; i < garages.length; i++) {
                  var marker = new google.maps.Marker({
                      map: map,
                      position: new google.maps.LatLng(garages[i].latitude, garages[i].longitude),
                  });

                  var load = parseFloat(garages[i].numberOfLeasedSpaces) - parseFloat(garages[i].numberOfTeamMemberSpaces);
                  if (100 < load) {
                      marker.setIcon('../../Content/Images/parking_logo_green.png'); //TODO: Fix click zone
                  }
                  else if ( 50 < load && load < 100) {
                      console.log(load);
                      marker.setIcon('../../Content/Images/parking_logo_yellow.png');
                  }
                  else if (load < 50 && load > 0) {
                      marker.setIcon('../../Content/Images/parking_logo_red.png');
                  }
                  else if (load == 0) {
                      marker.setIcon('../../Content/Images/parking_logo_black.png');
                  }
                  garageMarkers.push(marker);
              }
          }
      }


    var garages = [
	    { name: "One Campus Martius", address: "1188 Farmer St, Detroit MI 48226", latitude: 42.3333543, longitude: -83.04649310000002, load: "low" },
	    { name: "Premier", address: "1206 Woodward Ave, Detroit MI 48226", latitude: 42.3337978, longitude: -83.0480407, load: "high" },
	    { name: "1001 Woodward", address: "1001 Woodward Ave, Detroit MI 48226", latitude: 42.3320291, longitude: -83.04760010000001, load: "low" },
	    { name: "Book Cadillac", address: "150 Michigan Ave, Detroit MI 48226", latitude: 42.331956, longitude: -83.04931999999997, load: "medium" },
	    { name: "1 Detroit", address: "200 Larned St, Detroit MI 48226", latitude: 42.3298491, longitude: -83.0442246, load: "low" },
	    { name: "2 Detroit", address: "160 E Congress St, Detroit MI 48226", latitude: 42.3308968, longitude: -83.0436712, load: "medium" },
	    { name: "COBO Congress", address: "467 W Congress St, Detroit MI 48226", latitude: 42.3280065, longitude: -83.0508577, load: "full" },
	    { name: "COBO Rooftop", address: "625 W Congress St, Detroit MI 48226", latitude: 42.3269497, longitude: -83.05304419999999, load: "medium" },
	    { name: "COBO Washington", address: "475 Washington Ave, Detroit MI 48226", latitude: 42.3279516, longitude: -83.0485974, load: "low" },
	    { name: "Detroit Opera House", address: "1426 Broadway St, Detroit MI 48226", latitude: 42.3354605, longitude: -83.04736889999998, load: "high" },
	    { name: "Financial District", address: "730 Shelby St, Detroit MI 48226", latitude: 42.330903, longitude: -83.04879690000001, load: "low" },
	    { name: "AT&T Lot", address: "421 Bagley St, Detroit MI 48226", latitude: 42.33304, longitude: -83.05418789999999, load: "low" },
	    { name: "Brush St / Greektown", address: "1001 Brush St, Detroit MI 48226", latitude: 42.333784, longitude: -83.043768, load: "medium" },
	    { name: "Fort St / Greektown", address: "419 E Fort St, Detroit MI 48226", latitude: 42.333098, longitude: -83.04220499999997, load: "full" },
	    { name: "First National Garage", address: "660 Woodward Ave, Detroit MI 48226", latitude: 42.3311256, longitude: -83.0459323, load: "low" },
	    { name: "The Z", address: "1234 Library St, Detroit MI 48226", latitude: 42.334347, longitude: -83.0462809, load: "low" },
	    { name: "Howard Lot A", address: "1240 Abbot St, Detroit MI 48226", latitude: 42.3287717, longitude: -83.06081230000001, load: "high" },
	    { name: "Howard Lot B", address: "1300 Abbot St, Detroit MI 48226", latitude: 42.3282795, longitude: -83.06088899999997, load: "medium" },
	    { name: "Howard Lot C", address: "1100 Brooklyn St, Detroit MI 48226", latitude: 42.3269463, longitude: -83.0601658, load: "low" }
    ];



})();







//==================================== OLD CRAP ==========================================================


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