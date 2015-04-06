/**
* Map controller
* @namespace pera.map.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.map.controllers')
      .controller('MapController', MapController);

    MapController.$inject = ['$scope','Garages', 'Snackbar', 'Initializer', 'SideBar'];

    /**
    * @namespace MapController
    */
    function MapController($scope, Garages, Snackbar, Initializer, SideBar) {

        var styleArray = [
              {
                  "featureType": "all",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "gamma": "0.82"
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "geometry.fill",
                  "stylers": [
                  {
                      "gamma": "1.21"
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "labels",
                  "stylers": [
                  {
                      "lightness": "-60"
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "labels.text",
                  "stylers": [
                  {
                      "gamma": "5.37"
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "labels.text.fill",
                  "stylers": [
                  {
                      "color": "#757B7B"
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "labels.text.stroke",
                  "stylers": [
                  {
                      "visibility": "on"
                  },
                  {
                      "color": "#ffffff"
                  },
                  {
                      "lightness": 16
                  }
                  ]
              },
              {
                  "featureType": "all",
                  "elementType": "labels.icon",
                  "stylers": [
                  {
                      "visibility": "off"
                  }
                  ]
              },
              {
                  "featureType": "administrative",
                  "elementType": "geometry.fill",
                  "stylers": [
                  {
                      "color": "#fefefe"
                  },
                  {
                      "lightness": 20
                  }
                  ]
              },
              {
                  "featureType": "administrative",
                  "elementType": "geometry.stroke",
                  "stylers": [
                  {
                      "color": "#fefefe"
                  },
                  {
                      "lightness": 17
                  },
                  {
                      "weight": 1.2
                  }
                  ]
              },
              {
                  "featureType": "landscape",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "color": "#f5f5f5"
                  },
                  {
                      "lightness": 20
                  }
                  ]
              },
              {
                  "featureType": "landscape.natural",
                  "elementType": "geometry.fill",
                  "stylers": [
                  {
                      "saturation": "0"
                  }
                  ]
              },
              {
                  "featureType": "poi",
                  "elementType": "all",
                  "stylers": [
                      {
                          //"visibility": "off"

                      },
                      {
                          "lightness": "35"
                      }
                  ]
              },
              {
                  "featureType": "poi.park",
                  "elementType": "geometry.fill",
                  "stylers": [
                      {
                          "visibility": "on"
                      },
                      {
                          "saturation": "18"
                      },
                  ]
              },
              {
                  "featureType": "road.highway",
                  "elementType": "geometry.fill",
                  "stylers": [
                  {
                      "color": "#ffffff"
                  },
                  {
                      "lightness": 17
                  }
                  ]
              },
              {
                  "featureType": "road.highway",
                  "elementType": "geometry.stroke",
                  "stylers": [
                  {
                      "color": "#ffffff"
                  },
                  {
                      "lightness": 29
                  },
                  {
                      "weight": 0.2
                  }
                  ]
              },
              {
                  "featureType": "road.arterial",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "color": "#ffffff"
                  },
                  {
                      "lightness": 18
                  }
                  ]
              },
              {
                  "featureType": "road.local",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "color": "#ffffff"
                  },
                  {
                      "lightness": 16
                  }
                  ]
              },
              {
                  "featureType": "transit",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "color": "#f2f2f2"
                  },
                  {
                      "lightness": 19
                  }
                  ]
              },
              {
                  "featureType": "water",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "color": "#e9e9e9"
                  },
                  {
                      "lightness": 17
                  }
                  ]
              },
              {
                  "featureType": "water",
                  "elementType": "geometry.fill",
                  "stylers": [
                  {
                      "color": "#42738d"
                  },
                  {
                      "gamma": "5.37"
                  }
                  ]
              },
              {
                  "featureType": "road",
                  "elementType": "geometry",
                  "stylers": [
                  {
                      "visibility": "simplified"
                  }
                  ]
              },
              {
                  "featureType": "poi",
                  "elementType": "labels",
                  "stylers": [
                  {
                      "visibility": "off"
                  }
                  ]
              },
              {
                  "featureType": "transit",
                  "elementType": "labels",
                  "stylers": [
                  {
                      "visibility": "off"
                  }
                  ]
              },
              {
                  "featureType": "landscape",
                  "elementType": "labels",
                  "stylers": [
                  {
                      "visibility": "off"
                  }
                  ]
              }
        ];


        var vm = this;
        vm.map;
        vm.garages = [];
        vm.garageMarkers = [];
        vm.initialized = false; //tracks if the maps has already been initialized

        vm.styleArray = styleArray;

        //initialize the map
        console.log('map ctrl');
        vm.initialize = initialize;
        initialize();
        //window.onload = initialize;
        //initialize();

        function initialize() {
            if (vm.initialized == true)
                return;

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
            console.log("initalize");

            /*Initializer.mapsInitialized
                .then(function () {
                    console.log('create map');

                    vm.map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);*/

            vm.map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            //Add Elabel
            //var element = document.createElement("script");
            //element.setAttribute("type", "text/javascript");
            //element.src = "~/Scripts/js/ELabels3.js";
            //document.body.appendChild(element);

            //Add key to map
            vm.map.controls[google.maps.ControlPosition.BOTTOM_LEFT].push(document.getElementById('mapKey'));
            Garages.all().then(garagesSuccessFn, garagesErrorFn);
            console.log(vm.garages);
            vm.initialized = true;
          } //end intialize function

          function garagesSuccessFn(data, status, headers, config) {
              vm.garages = data.data;         //this will depend on what the API returns, it may have to change
              PlaceMarkers(vm.map, vm.garages, vm.garageMarkers);
          }
          
          function garagesErrorFn(data, status, headers, config) {
              //Snackbar.error(data.data.error);
          }
          function attachGarage(marker, garage) {
                  google.maps.event.addListener(marker, 'click', function () {
                      SideBar.setCurrent(marker.garage);
                      console.log(marker.garage.name);
                      //TODO: set state garage.map.garage
                      alert("Congratulations, you've clicked on a garage!");
                      //TODO: tab.setSideTab(i); //This is what I want to do
                  });
          }
          function PlaceMarkers(map, garages, garageMarkers) {
              for (var i = 0; i < garages.length; i++) {
                  var position = new google.maps.LatLng(garages[i].latitude, garages[i].longitude);

                  //create marker
                  var marker = new google.maps.Marker({
                      map: map,
                      position: position,
                      garage: garages[i]
                  });

                  //add label
                  var styleString = '<div style="font-size:14px; color: #4F4F4F; text-shadow: 1px 1px 0 #FFF, -1px 1px 0 #FFF, 1px -1px 0 #FFF, -1px -1px #FFF;">';
                  var label = new ELabel(map, position, styleString + garages[i].name + '</div>', null, new google.maps.Size(-20, -32), false);
                  label.setMap(map);
                  var garage = garages[i];
                  //add click event
                  attachGarage(marker, garage);

                  //add marker image
                  var load = parseFloat(garages[i].numberOfLeasedSpaces) - parseFloat(garages[i].numberOfTeamMemberSpaces);
                  if (load > 100) {
                      marker.setIcon('../../Content/Images/parking_green.png'); //TODO: Fix click zone
                  }
                  else if ( load > 50 && load <= 100) {
                      marker.setIcon('../../Content/Images/parking_yellow.png');
                  }
                  else if (load <= 50 && load > 0) {
                      marker.setIcon('../../Content/Images/parking_red.png');
                  }
                  else if (load == 0) {
                      marker.setIcon('../../Content/Images/parking_black.png');
                  }

                  vm.garageMarkers.push(marker);
              }
          }
    } //End MapController
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