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

            //[{"featureType":"administrative","elementType":"labels.text.fill","stylers":[{"lightness":"-30"}]},{"featureType":"landscape","elementType":"all","stylers":[{"color":"#f2f2f2"}]},{"featureType":"landscape.man_made","elementType":"geometry","stylers":[{"hue":"#00bfff"},{"lightness":"0"}]},{"featureType":"poi","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"poi.park","elementType":"geometry.fill","stylers":[{"visibility":"on"},{"saturation":"18"},{"lightness":"57"}]},{"featureType":"road","elementType":"all","stylers":[{"saturation":-100},{"lightness":45}]},{"featureType":"road","elementType":"geometry.fill","stylers":[{"lightness":"9"}]},{"featureType":"road","elementType":"geometry.stroke","stylers":[{"lightness":"6"}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"lightness":"-43"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"road.arterial","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"transit.line","elementType":"geometry","stylers":[{"hue":"#52ff00"},{"lightness":"-50"},{"gamma":"1.00"}]},{"featureType":"transit.station.airport","elementType":"geometry","stylers":[{"saturation":"-77"},{"gamma":"1.79"},{"lightness":"23"}]},{"featureType":"transit.station.bus","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"transit.station.rail","elementType":"all","stylers":[{"visibility":"on"},{"hue":"#ff7e00"}]},{"featureType":"transit.station.rail","elementType":"labels.text.fill","stylers":[{"weight":"1.00"},{"gamma":"1.00"},{"lightness":"0"},{"saturation":"0"}]},{"featureType":"water","elementType":"all","stylers":[{"color":"#ccd7dc"},{"visibility":"on"}]},{"featureType":"water","elementType":"geometry.fill","stylers":[{"visibility":"on"},{"lightness":"20"},{"saturation":"63"}]},{"featureType":"water","elementType":"labels.text.fill","stylers":[{"lightness":"-39"}]},{"featureType":"water","elementType":"labels.text.stroke","stylers":[{"lightness":"55"}]}]
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
                "visibility": "off"
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
            {
                "lightness": "57"
            }
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
   
              //Add key to map
              this.map.controls[google.maps.ControlPosition.BOTTOM_LEFT].push(document.getElementById('mapKey'));

              Garages.all().then(garagesSuccessFn, garagesErrorFn);
              console.log(vm.garages);

              this.initialized = true;
          } //end intialize() function


        /*
          $scope.$on('receiveGarages', function () {
              vm.garages = Garages.garages;
              PlaceMarkers(vm.map, vm.garages, vm.garageMarkers);
          });
          */
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
                  var styleString = '<div style="font-size:12pt; color: #4F4F4F; text-shadow: 1px 1px 0 #FFF, -1px 1px 0 #FFF, 1px -1px 0 #FFF, -1px -1px #FFF;">';
                  var label = new ELabel(map, position, styleString + garages[i].name + '</div>', null, new google.maps.Size(-20, -32), false);
                  label.setMap(map);


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