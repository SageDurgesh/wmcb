/*
 * Bootstrap Image Gallery JS Demo 3.0.1
 * https://github.com/blueimp/Bootstrap-Image-Gallery
 *
 * Copyright 2013, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint unparam: true */
/*global blueimp, $ */

$(function () {
  
    'use strict';
    var result = [{"url": "http://2.bp.blogspot.com/-H6MAoWN-UIE/TuRwLbHRSWI/AAAAAAAABBk/89iiEulVsyg/s400/Free%2BNature%2BPhoto.jpg", "title": "1" },
    { "url": "http://www.virginia.org/uploadedImages/virginiaorg/Images/OrgImages/H/HamptonConventionVisitorBureau/Grandview_Nature_Preserve.jpg?width=300&height=200&scale=upscalecanvas","title":"2" }];
        var linksContainer = $('#links'),
            baseUrl;
        // Add the demo images as links with thumbnails to the page:
        $.each(result, function (index, photo) {
            
            $('<a/>')
                .append($('<img>').prop('src', photo.url))
                .prop('href', photo.url)
                .prop('title', photo.title)
                .attr('data-gallery', '')
                .appendTo(linksContainer);
        });

    $('#borderless-checkbox').on('change', function () {
        var borderless = $(this).is(':checked');
        $('#blueimp-gallery').data('useBootstrapModal', !borderless);
        $('#blueimp-gallery').toggleClass('blueimp-gallery-controls', borderless);
    });

    $('#fullscreen-checkbox').on('change', function () {
        $('#blueimp-gallery').data('fullScreen', $(this).is(':checked'));
    });

    $('#image-gallery-button').on('click', function (event) {
        event.preventDefault();
        blueimp.Gallery($('#links a'), $('#blueimp-gallery').data());
    });
});