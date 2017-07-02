$(document).ready(function () {

	var host = "http://localhost:";
	var port = "61255";
	var filmoviEndpoint = "/api/filmovi/";
	var reziseriEndpoint = "/api/reziseri/";


	$("#btnReziseri").click(function () {
		// ucitavanje rezisera
		var fullURL = host + port + reziseriEndpoint;
		console.log("URL zahteva: " + fullURL);
		$.getJSON(fullURL, getReziseri);
	});

	$("#btnFilmovi").click(function () {
		// ucitavanje filmova
		var fullURL = host + port + filmoviEndpoint;
		console.log("URL zahteva: " + fullURL);
		$.getJSON(fullURL, getFilmovi);
	});

	$("body").on("click", "#btn_Film_Delete", deleteFilm);
	$("body").on("click", "#btn_Film_Edit", editFilm);

	$("body").on("click", "#btn_Reziser_Delete", deleteReziser);
	$("body").on("click", "#btn_Reziser_Edit", editReziser);


	$("#formAddF").submit(addFilm);
		
	$("#formEditF").submit(function (e) {
		e.preventDefault();
		var id = $("#fId").val();
		var naziv = $("#fNaziv_Edit").val();
		var zanr = $("#fZanr_Edit").val();
		var godina = $("#fGodina_Edit").val();
		var reziser = $("#fReziser_Edit").val();

		var sendData = {
			"Id": id,
			"Naziv": naziv,
			"Zanr": zanr,
			"Godina": godina,
			"ReziserId": reziser
		};

		console.log("Objekat za slanje");
		console.log(sendData);

		var editURL = host + port + filmoviEndpoint + id;
		$.ajax({
			url: editURL,
			dataType: 'json',
			method: "PUT",
			data: sendData,
		})
			.done(function (data, status) { updateFilmoviUI(); })
			.fail(function (data, status) {alert("Greška!");});

	});


	$("#formAddR").submit(addReziser);

	
	$("#formEditR").submit(function (e) {

		e.preventDefault();

		var id = $("#rId").val();
		var ime = $("#rIme_Edit").val();
		var prezime = $("#rPrezime_Edit").val();
		var starost = $("#rStarost_Edit").val();

		var sendData = {
			"Id": id,
			"Ime": ime,
			"Prezime": prezime,
			"Starost": starost
		};

		console.log("Objekat za slanje");
		console.log(sendData);


		var EditURL = host + port + reziseriEndpoint + id;
		$.ajax({
			url: EditURL,
			dataType: 'json',
			method: "PUT",
			data: sendData,
		})
			.done(function (data, status) { updateReziseriUI(); })
			.fail(function (data, status) {alert("Greška!");
			});

	});

     // display Filmovi table funkcija
	function getFilmovi(data, status) {
		console.log("Status: " + status);


		var container = $("#data");
		container.empty();

		$("#divAddR").css("display", "none");
		$("#divEditR").css("display", "none");
		$("#divEditF").css("display", "none");
		//
		$("#divAddF").css("display", "block");

		if (status == "success") {
			console.log(data);

			var div = $("<div></div>");
			var h1 = $("<h2>Filmovi:</h2>");
			div.append(h1);

			var table = $("<table class='table table-bordered  table-striped'></table>");
			var header = $("<thead><tr><th>Naziv</th><th>Žanr</th><th>Godina</th><th>Režiser</th><th></th></tr></thead>");
			table.append(header);
			var redovi= "<tbody>";

			for (var i = 0; i < data.length; i++) {

				var displayData = "<tr><td>" + data[i].Naziv + "</td><td>" + data[i].Zanr + "</td><td>" + data[i].Godina + "</td><td>" + data[i].Reziser.Ime + " " + data[i].Reziser.Prezime + "</td>";
				var ID = data[i].Id.toString();
				var displayEdit = "<td><button class='btn btn-success btn-sm' id='btn_Film_Edit' name=" + ID + ">Izmeni</button>&nbsp;&nbsp;";
				var displayDelete = "<button class='btn btn-danger btn-sm' id='btn_Film_Delete' name=" + ID + ">Obriši</button></td>";
				redovi += displayData + displayEdit + displayDelete + "</tr>";


			}
			redovi += "</tbody>";
			table.append(redovi);

			div.append(table);



			var fullURL = host + port + reziseriEndpoint;

			$.getJSON(fullURL).done(function (data) {

				$("#fReziser option").remove();
				console.log(data);

				for (var i = 0; i < data.length; i++) {
					$("#fReziser").append("<option value=" + data[i].Id + ">" + data[i].Ime + " " + data[i].Prezime + "</option>");
				}
			});

			container.append(div);
		}
		else {
			var div = $("<div></div>");
			var h1 = $("<h1>Greška pri učitavanju filmova.</h1>");
			div.append(h1);
			container.append(div);
		}
	}

	function addFilm(event) {
		event.preventDefault();

		var naziv = $("#fNaziv").val();
		var zanr = $("#fZanr").val();
		var godina = $("#fGodina").val();
		var reziser = $("#fReziser").val();

		var sendData = {
			"Naziv": naziv,
			"Zanr": zanr,
			"Godina": godina,
			"ReziserId": reziser
		};

		console.log("Objekat za slanje");
		console.log(sendData);

		var postURL = host + port + filmoviEndpoint;
		$.ajax({
			url: postURL,
			method: "POST",
			data: sendData,
		})
			.done(function (data, status) {updateFilmoviUI();})
			.fail(function (data, status) {alert("Greška!");});

	}

    // poziva getFilm action
	function editFilm() {

		$("#divAddF").css("display", "none");
		$("#editZanrDiv").css("display", "none");
		$("#divEditF").css("display", "block");

		var filmID = this.name;
		var FilmURL = host + port + filmoviEndpoint + filmID.toString();
		var ReziseriURL = host + port + reziseriEndpoint;
		console.log(FilmURL);

		$.getJSON(FilmURL)
			.done(function (data, status) {
				console.log(data);
				$('#fId').val(data.Id);
				$('#fNaziv_Edit').val(data.Naziv);
				$('#fZanr_Edit').val(data.Zanr);
				$('#fGodina_Edit').val(data.Godina);

				var reziserID = data.ReziserId;

				$.getJSON(ReziseriURL)
					.done(function (data) {
						$("#fReziser_Edit option").remove();
						var optionString = "";

						for (i = 0; i < data.length; i++) {
							if (reziserID === data[i].Id) {
								optionString = "<option selected value=" + data[i].Id + ">" + data[i].Ime + " " + data[i].Prezime + "</option>";
							}
							else {
								optionString = "<option value=" + data[i].Id + ">" + data[i].Ime + " " + data[i].Prezime + "</option>";
							}
							$("#fReziser_Edit").append(optionString);
						}
					});
			})
			.fail(function (data, status) { alert("Greška!"); });
	}

	function deleteFilm() {
		var filmID = this.name;

		deleteURL = host + port + filmoviEndpoint + filmID.toString();
		$.ajax({
			url: deleteURL,
			method: "DELETE",
		})
			.done(function (data, status) {updateFilmoviUI();})
			.fail(function (data, status) { alert("Greška!"); });
	}


	// display Reziseri table funkcija
	function getReziseri(data, status) {
		console.log("Status: " + status);

		var container = $("#data");
		container.empty();

		$("#divAddF").css("display", "none");
		$("#divEditF").css("display", "none");
		$("#divEditR").css("display", "none");
		//
		$("#divAddR").css("display", "block");

		if (status == "success") {
			console.log(data);

			var div = $("<div></div>");
			var h2 = $("<h2>Režiseri:</h2>");
			div.append(h2);

			var table = $("<table class='table table-striped table-bordered'></table>");
			var header = $('<thead><tr><th>Ime</th><th>Prezime</th><th>Starost</th><th></th></tr></thead>');
			table.append(header);
			var redovi = "<tbody>";

			for (i = 0; i < data.length; i++) {

				var displayData = "<tr><td>" + data[i].Ime + "</td><td>" + data[i].Prezime + "</td><td>" + data[i].Starost + "</td>";
				var ID = data[i].Id.toString();
				var displayEdit = "<td style='width: 150px'><button class='btn btn-success btn-sm' id='btn_Reziser_Edit' name=" + ID + ">Izmeni</button>&nbsp;&nbsp;";
				var displayDelete = "<button class='btn btn-danger btn-sm' id='btn_Reziser_Delete' name=" + ID + ">Obriši</button></td> ";
				redovi += displayData + displayEdit + displayDelete + "</tr>";


			}
			redovi += "</tbody>";
			table.append(redovi);

			div.append(table);


			container.append(div);
		}
		else {
			var div = $("<div></div>");
			var h1 = $("<h1>Greška pri učitavanju režisera!</h1>");
			div.append(h1);
			container.append(div);
		}
	}

	function addReziser(event) {
		event.preventDefault();

		var ime = $("#rIme").val();
		var prezime = $("#rPrezime").val();
		var starost = $("#rStarost").val();

		var sendData = {
			"Ime": ime,
			"Prezime": prezime,
			"Starost": starost
		};

		console.log("Objekat za slanje");
		console.log(sendData);

		var postURL = host + port + reziseriEndpoint;
		$.ajax({
			url: postURL,
			method: "POST",
			data: sendData,
		})
			.done(function (data, status) {updateReziseriUI();})
			.fail(function (data, status) {alert("Greška!");});

	}

		// poziva getReziser action
	function editReziser() {

		$("#divAddR").css("display", "none");
		$("#divEditF").css("display", "none");
		$("#divEditR").css("display", "block");

		var editID = this.name;
		var editURL = host + port + reziseriEndpoint + editID.toString();
		console.log(editURL);
		$.getJSON(editURL)
			.done(function (data, status) {
				console.log(data);
				$('#rId').val(data.Id);
				$('#rIme_Edit').val(data.Ime);
				$('#rPrezime_Edit').val(data.Prezime);
				$('#rStarost_Edit').val(data.Starost);
			})
			.fail(function (data, status) {alert("Greška!");});
	}

	function deleteReziser() {
		var deleteID = this.name;

		var deleteURL = host + port + reziseriEndpoint + deleteID.toString();
		$.ajax({
			url: deleteURL,
			method: "DELETE",
		})
			.done(function (data, status) {updateReziseriUI();})
			.fail(function (data, status) {alert("Greška!");});
	}



	function updateReziseriUI() {
		$("#formAddR")[0].reset();
		$("#btnReziseri").trigger("click");
	}

	function updateFilmoviUI() {
		$("#formAddF")[0].reset();
		$("#btnFilmovi").trigger("click");
	}

});
