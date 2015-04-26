/**
* Export Reports Controller
* @namespace pera.export.controllers
*/
(function () {
    'use strict';

    angular
      .module('pera.export.controllers')
      .controller('ExportReportController', ExportReportController);

    //ExportReportController.$inject = ['WriteRandomValuesDOM'];

    function ExportReportController() {
        var vm = this;
        vm.ExportReport = ExportReport;
        function ExportReport() {
            alert("ASNKAJDNKLASMDKLSA");
            console.log("Succesuful");
            $.post('./api/exportreport/WriteRandomValuesDOM', { }, function (data) { alert("Success "); });
            //WriteRandomValuesDOM("./api/exportreport/WriteRandomValuesDOM");
        }

    }

})();