$(document).ready(function () {

    function createRow(rol) {
        //Create
        var tr = document.createElement('tr');
        var td = document.createElement('td');
        var td1 = document.createElement('td');
        var td2 = document.createElement('td');
        var td3 = document.createElement('td');
        var td4 = document.createElement('td');
        var td5 = document.createElement('td');
        var td6 = document.createElement('td');
        var modif = document.createElement("a");
        var tdmod = document.createElement('td');
        var del = document.createElement("a");
        var tdel = document.createElement('td');

        //Style
        modif.type = "button"; del.type = "button";
        modif.classList.add("btn", "btn-info", "d-flex", "justify-content-center", "align-items-center", "mb-2");
        del.classList.add("btn", "btn-danger", "d-flex", "justify-content-center", "align-items-center");

        //Url
        modif.href = "/Rol/Actualizar/" + rol['id'];
        if (rol['estado'] == 1) {
            del.innerHTML = "Desactivar";
            del.href = "/Rol/Desactivar/" + rol['id'];
        } else {
            del.innerHTML = "Activar";
            del.href = "/Rol/Activar/" + rol['id'];
        }

        //Event
        del.onclick = function (e) {
            e.preventDefault();
            swal({
                title: "Desea realizar esta acción?",
                text: "Cambiara el estado del Rol",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    swal("Estado del Rol cambiado", {
                        icon: "success",
                    }).then((success) => window.location.href = del.href);             
                } else {
                    swal("Cancelado!");
                }
            });
        };

        //Values
        modif.innerHTML = "Modificar";
        td.innerHTML = rol['descripcion'];
        td1.innerHTML = rol['id'];
        td2.innerHTML = rol['empresa']['nombre'];
        td3.innerHTML = rol['usuarioIngreso'];
        td4.innerHTML = rol['fechaIngreso'];
        td5.innerHTML = rol['usuarioModificacion'];
        td6.innerHTML = rol['fechaModificacion'];


        //Append
        tdmod.appendChild(modif);
        tdel.appendChild(del);
        tr.append(tdmod, td, td1, td2, td3, td4, td5, td6, tdel);
        return tr;
    }

    function createRows() {
        var tbody = document.getElementById("tbody_roles");
        var url = host + "/Rol/GetRoles/-1";
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
                    var roles = JSON.parse(json);
                    for (var i = 0; i < roles.length; i++) {
                        var tr = createRow(roles[i]);
                        tbody.appendChild(tr);
                    }
                    var options = {
                        filters_cell_tag: 'th',
                        themes: [{ name: 'transparent' }],
                        col_0: 'none',
                        col_8: 'none',
                        filters_row_index: 1,
                        auto_filter: { delay: 300 }
                    };
                    var tf = new TableFilter('tabla-roles', options);
                    tf.init();
                } catch (e) {
                    showAlert("Advertencia", "Ha ocurrido un problema al cargar roles!" + e);
                }
            }
        };
        http.send();
    }

    createRows();
});