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
                  zoom: 17,
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
                  var position = new google.maps.LatLng(garages[i].latitude, garages[i].longitude);

                  var marker = new google.maps.Marker({
                      map: map,
                      position: position
                  });

                  var label = new ELabel(map, position, '<div style="font-size:1.5em; font-weight:bold;">' + garages[i].name + '</div>', null, new google.maps.Size(-20, -72), false);
                  label.setMap(map);


                  var load = parseFloat(garages[i].numberOfLeasedSpaces) - parseFloat(garages[i].numberOfTeamMemberSpaces);
                  if (100 < load) {
                      marker.setIcon('../../Content/Images/parking_logo_green.png'); //TODO: Fix click zone
                  }
                  else if ( 50 < load && load < 100) {
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