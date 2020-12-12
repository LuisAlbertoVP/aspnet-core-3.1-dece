$(document).ready(function () {

    function createRow(user) {
        //Create
        var tr = document.createElement('tr');
        var td = document.createElement('td');
        var td1 = document.createElement('td');
        var td2 = document.createElement('td');
        var td3 = document.createElement('td');
        var td4 = document.createElement('td');
        var td5 = document.createElement('td');
        var td6 = document.createElement('td');
        var td7 = document.createElement('td');
        var modif = document.createElement("a");
        var tdmod = document.createElement('td');
        var del = document.createElement("a");
        var tdel = document.createElement('td');

        //Style
        modif.type = "button"; del.type = "button";
        modif.classList.add("btn", "btn-info", "d-flex", "justify-content-center", "align-items-center", "mb-2");
        del.classList.add("btn", "btn-danger", "d-flex", "justify-content-center", "align-items-center");

        //Url
        modif.href = "/Usuario/Actualizar/" + user['id'];       
        if (user['estado'] == 1) {
            del.innerHTML = "Desactivar";
            del.href = "/Usuario/Desactivar/" + user['id'];
        } else {
            del.innerHTML = "Activar";
            del.href = "/Usuario/Activar/" + user['id'];
        }

        //Event
        del.onclick = function (e) {
            e.preventDefault();
            swal({
                title: "Desea realizar esta acción?",
                text: "Cambiara el estado del Usuario",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    swal("Estado del Usuario cambiado", {
                        icon: "success",
                    }).then((success) => window.location.href = del.href);
                } else {
                    swal("Cancelado!");
                }
            });
        };

        //Values
        modif.innerHTML = "Modificar";
        td.innerHTML = user['nombreCompleto'];
        td1.innerHTML = user['id'];
        td2.innerHTML = user['empresa']['nombre'];
        td3.innerHTML = user['fechaExpiracion'];
        td4.innerHTML = user['usuarioIngreso'];
        td5.innerHTML = user['fechaIngreso'];
        td6.innerHTML = user['usuarioModificacion'];
        td7.innerHTML = user['fechaModificacion'];

        //Append
        tdmod.appendChild(modif);
        tdel.appendChild(del);
        tr.append(tdmod, td, td1, td2, td3, td4, td5, td6, td7, tdel);
        return tr;
    }

    function createRows() {
        var tbody = document.getElementById("tbody_usuarios");
        var url = host + "/Usuario/GetUsuarios/all";
        var http = new XMLHttpRequest();
       
        http.open("GET", url, true);
        http.setRequestHeader("Content-Type", "application/json");

        http.onreadystatechange = function () {
            if (http.readyState === 4 && http.status === 200) {
                var json = http.responseText;
                while (tbody.hasChildNodes()) {
                    tbody.removeChild(tbody.lastChild);
                }
                try {
                    var users = JSON.parse(json);
                    for (var i = 0; i < users.length; i++) {
                        var tr = createRow(users[i]);
                        tbody.appendChild(tr);
                    }
                    var options = {
                        filters_cell_tag: 'th',
                        themes: [{ name: 'transparent' }],
                        col_0: 'none',
                        col_9: 'none',
                        filters_row_index: 1,
                        auto_filter: { delay: 300 }
                    };
                    var tf = new TableFilter('tabla-usuarios', options);
                    tf.init();
                } catch (e) {
                    showAlert("Advertencia", "Ha ocurrido un problema al cargar usuarios!");
                }
            }
        };
        http.send();
    }

    createRows();
});