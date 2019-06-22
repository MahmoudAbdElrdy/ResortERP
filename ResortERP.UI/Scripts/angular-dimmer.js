/**
 * angular-dimmer v1.0
 *
 * Copyright (c) 2014 Yuthasak Tanruengsri yuthasak.tanruengsri@googlemail.com
 * https://github.com/ytanruengsri/angular-dimmer
 *
 * License: MIT
 */

'use strict';

angular.module('dimmerModule', [])
    .constant('$dimmerConstant', {
        'defaultZIndex': 20000,
        'defaultOption': {
            lines: 13, // The number of lines to draw
            length: 20, // The length of each line
            width: 10, // The line thickness
            radius: 30, // The radius of the inner circle
            corners: 1, // Corner roundness (0..1)
            rotate: 0, // The rotation offset
            direction: 1, // 1: clockwise, -1: counterclockwise
            color: '#FFF', // #rgb or #rrggbb or array of colors
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: '50%', // Top position relative to parent
            left: '50%' // Left position relative to parent
        }
    })
    .factory('$dimmer', [function () {
        var elem = null;

        return {
            /* ============ SETTER FN ============*/
            setElement: function(element) {
                elem =  element;
            },
            show: function() {
                elem.css('display', 'block');
            },
            hide: function() {
                elem.css('display', 'none');
            }
        };
    }])
    .directive('dimmer', ['$dimmerConstant', '$dimmer',
        function($dimmerConstant, $dimmer) {
            return {
                restrict: 'E',
                replace: true,
                scope: {
                    zIndex: '='
                },
                template:
                    '<div class="dimmer">' +
                    '   <div class="content">' +
                    '       <div class="center">' +
                    '           <div class="loading"></div>' +
                    '       </div>' +
                    '   </div>' +
                    '</div>',
                link: function(scope, elem) {
                    $dimmer.setElement(elem);

                    var zIndex = angular.isDefined(scope.zIndex) ? scope.zIndex : $dimmerConstant.defaultZIndex;
                    elem.css('zIndex', zIndex);

                    var spinner = new Spinner($dimmerConstant.defaultOption).spin(elem[0]);

                    scope.$on('$destroy', function() {
                        spinner.stop();
                        elem.remove();
                    });
                }
            };
        }]);