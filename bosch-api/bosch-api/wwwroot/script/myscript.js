const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://bosch-api.azurewebsites.net/boschapihub")
    .build();

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

connection.on("NewIn", (cameraId) => {
    console.log("NewIn: " + cameraId);
	
	fetch("https://bosch-api.azurewebsites.net/gettodayentries?cameraid=1&recordcount=50000")
		.then(response =>{
			return response.json();
		})
		.then(data=>{
			console.log(data);
			var table = document.getElementById("EntryTable");
			
			while (table.rows.length > 1) {
				table.deleteRow(1);
			}
			
			for (var i = 0; i < data.records.length; i++) {
				
				var tr = table.insertRow(-1);
				
				for (var j = 0; j < 2; j++) {
					var tabCell = tr.insertCell(-1);
					
					if(j==0)
					{
						tabCell.innerHTML = data.records[i].timestamp;
					}
					else
					{
						tabCell.style.textAlign = "center";
						tabCell.innerHTML = data.records[i].count;
					}
				}
			}
		})
});

connection.on("NewOut", (cameraId) => {
    console.log("NewOut: " + cameraId);
	
	fetch("https://bosch-api.azurewebsites.net/gettodayexits?cameraid=1&recordcount=50000")
		.then(response =>{
			return response.json();
		})
		.then(data=>{
			console.log(data);
			var table = document.getElementById("ExitTable");
			
			while (table.rows.length > 1) {
				table.deleteRow(1);
			}
			
			for (var i = 0; i < data.records.length; i++) {
				
				var tr = table.insertRow(-1);
				
				for (var j = 0; j < 2; j++) {
					var tabCell = tr.insertCell(-1);
					
					if(j==0)
					{
						tabCell.innerHTML = data.records[i].timestamp;
					}
					else
					{
						tabCell.style.textAlign = "center";
						tabCell.innerHTML = data.records[i].count;
					}
				}
			}
		})
});

connection.on("CrowdDensityChanged", (cameraId, density) => {
    console.log("CrowdDensityChanged: " + cameraId + ", " + density);
});

// Quick and simple export target #table_id into a csv
function download_table_as_csv(table_id) {
    // Select rows from table_id
    var rows = document.querySelectorAll('table#' + table_id + ' tr');
    // Construct csv
    var csv = [];
    for (var i = 0; i < rows.length; i++) {
        var row = [], cols = rows[i].querySelectorAll('td, th');
        for (var j = 0; j < cols.length; j++) {
            // Clean innertext to remove multiple spaces and jumpline (break csv)
            var data = cols[j].innerText.replace(/(\r\n|\n|\r)/gm, '').replace(/(\s\s)/gm, ' ')
            // Escape double-quote with double-double-quote (see https://stackoverflow.com/questions/17808511/properly-escape-a-double-quote-in-csv)
            data = data.replace(/"/g, '""');
            // Push escaped string
            row.push('"' + data + '"');
        }
        csv.push(row.join(','));
    }
    var csv_string = csv.join('\n');
    // Download it
    var filename = 'export_' + table_id + '_' + new Date().toLocaleDateString() + '.csv';
    var link = document.createElement('a');
    link.style.display = 'none';
    link.setAttribute('target', '_blank');
    link.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(csv_string));
    link.setAttribute('download', filename);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

//function setImageVisible(id, visible) {
//    var img = document.getElementById(id);
//    img.style.visibility = (visible ? 'visible' : 'hidden');
//}

start();
//setImageVisible("alramimage",false);
//setImageVisible();
//setImageVisible();