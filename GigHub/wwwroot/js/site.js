// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
    $(".js-toggle-attendance").click(function (e) {
        var button = $(e.target);
        $.ajax({
            url: "/api/attendances",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: JSON.stringify({ gigId: button.attr("data-gig-id") })
        })
            .done(function () {
                button
                    .removeClass("btn-default")
                    .addClass("btn-info")
                    .text("Going");
            })
            .fail(function () {
                alert("Something failed!");
            });
    });

    $(".js-toggle-following").click(function (e) {
        var button = $(e.target);
        $.ajax({
            url: "/api/followings",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            data: JSON.stringify({ followeeId: button.attr("data-following-id") })
        })
            .done(function () {
                button
                    .text("Following");
            })
            .fail(function () {
                alert("Something failed!");
            });
    });
});