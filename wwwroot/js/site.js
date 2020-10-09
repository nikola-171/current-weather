// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#btnSubmit").click(function () {
    $("#prikaz_prognoze").hide();
        var cityname = $("#txtCity").val();
        if (cityname.length > 0)
        {
            $.ajax({
                url: '/Home/Daj',
                data: { cityname: cityname },
                type: "POST",
                success: function (rsltval) {
                    if (rsltval === "404") {
                        $("#load").removeClass("spinner-border");

                        alert("City not found.");
                    } else if (rsltval === "missing" ) {
                        $("#load").removeClass("spinner-border");
                        alert("Your API ID is missing.")
                    }

                    else {
                        $("#load").removeClass("spinner-border");

                        let data = JSON.parse(rsltval);
                        $("#lblCity").html(data["city"]);
                        $("#lblCountry").text(data["country"]);
                        $("#lblLat").text(data.lat);
                        $("#lblLon").text(data.lon);
                        $("#lblDescription").text(data.description);
                        $("#lblHumidity").text(data.humidity);
                        $("#lblTempFeelsLike").text(data.tempFeelsLike);
                        $("#lblTemp").text(data.temp);
                        $("#lblTempMax").text(data.tempMax);
                        $("#lblTempMin").text(data.tempMin);
                        $("#imgWeatherIconUrl").attr("src", "https://openweathermap.org/img/wn/" + data.weatherIcon + "@2x.png");
                        $("#prikaz_prognoze").fadeIn();
                        //data - response from server
                    }
            },
            error: function (xhr, errorType, exceptionThrown) {
               
                },
            beforeSend: function () {
                $("#load").addClass("spinner-border");
                }
            });
        }
        else
        {   
            $("#load").removeClass("spinner-border");
            alert("City Not Found");
            $("#txtCity").reset();
        }
    });

