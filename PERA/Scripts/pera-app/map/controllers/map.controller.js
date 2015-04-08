/**
* Map controller
* @namespace pera.map.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.map.controllers')
      .controller('MapController', MapController);

    MapController.$inject = ['$scope','Garages', 'Snackbar', 'Initializer', 'SideBar', '$state', '$rootScope'];

    /**
    * @namespace MapController
    */
    function MapController($scope, Garages, Snackbar, Initializer, SideBar, $state, $rootScope) {

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
        $rootScope.$state = $state;

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
                  var promise = $state.go('garage.map.garage').then(
                      function () {
                          SideBar.setCurrent(marker.garage);
                          //console.log(marker.garage.name);
                      });
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
