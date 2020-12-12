function createRow(id, emp) {
    //Create
    var row = document.createElement("tr");
    var plus = document.createElement("button");
    var n = document.createElement("td");
    var idEmpresa = document.createElement("td");
    var nombre = document.createElement("td");
    var telef = document.createElement("td");
    var mail = document.createElement("td");
    var modifTd = document.createElement("td");
    var modif = document.createElement("a");
    var delTd = document.createElement("td");
    var del = document.createElement("a");

    //Style
    modif.type = "button"; del.type = "button";
    plus.style.padding = "5px 12px";
    plus.style.marginTop = "10px";
    plus.classList.add("btn", "btn-success");
    modif.classList.add("btn", "btn-primary");
    del.classList.add("btn", "btn-danger");

    //Url
    modif.href = "/Empresa/Modificar/" + emp['id'];

    //Values
    n.innerHTML = id;
    idEmpresa.innerHTML = emp['id'];
    nombre.innerHTML = emp['nombre'];
    telef.innerHTML = emp['telefono'];
    mail.innerHTML = emp['mail'];
    modif.innerHTML = "Modificar";
    if (emp['estado'] == 1) {
        del.innerHTML = "Desactivar";
        del.href = "/Empresa/Desactivar/" + emp['id'];
    } else {
        del.innerHTML = "Activar";
        del.href = "/Empresa/Activar/" + emp['id'];
    }
    plus.id = emp['id'];
    var usuarios = emp['usuarios'];
    var userDescripcion = '';
    for (var i = 0; i < usuarios.length; i++) {
        userDescripcion += usuarios[i]['nombreCompleto'] + ' ';
    }
    var roles = emp['roles'];
    var rolDescripcion = '';
    for (var i = 0; i < roles.length; i++) {
        rolDescripcion += roles[i]['descripcion'] + ' ';
    }
    plus.innerHTML = '+' +
        '<pre style="display: none; " id="pre-' + emp['id'] + '">' + `
        Id Usuario:            ` + emp['id'] + `
        Ruc:                   ` + emp['ruc'] + `
        Direccion:             ` + emp['direccion'] + `
        Telefono:              ` + emp['telefono'] + `
        RazonSocial:           ` + emp['razonSocial'] + `
        Mail:                  ` + emp['mail'] + `
        Fecha Expiracion:      ` + emp['fechaExpiracion'] + `
        Ingreso Usuario:       ` + emp['usuarioIngreso'] + `
        Fecha de ingreso:      ` + emp['fechaIngreso'] + `
        Modifico Usuario:      ` + emp['usuarioModificacion'] + `
        Fecha Modificacion:    ` + emp['fechaModificacion'] + `
        Usuarios:              ` + userDescripcion + `
        Rol:                   ` + rolDescripcion +
        '</pre>';

    //Event
    plus.onclick = function () { showAlertDescription(emp['id'], emp['nombre']); };

    //Append
    modifTd.appendChild(modif); delTd.appendChild(del);
    row.append(plus, n, idEmpresa, nombre, telef, mail, modifTd, delTd);

    return row;
}

function fillTable() {
    var id = document.getElementById("search").value;
    if (id.length === 0) {
        document.getElementById('titleModal').innerHTML = 'Advertencia';
        document.getElementById('bodyModal').innerHTML = 'El campo de busqueda esta vacio!';
        $('#message').modal('show');
        return false;
    }
    var table = document.getElementById("tablaEmpresas");
    var url = host + "/Empresa/GetEmpresas/" + id;
    var http = new XMLHttpRequest();

    http.open("GET", url, true);
    http.setRequestHeader("Content-Type", "application/json");

    http.onreadystatechange = function () {
        if (http.readyState === 4 && http.status === 200) {
            var json = http.responseText;
            while (table.hasChildNodes()) {
                table.removeChild(table.lastChild);
            }
            try {
                var empresas = JSON.parse(json);
                for (var i = 0; i < empresas.length; i++) {
                    var row = createRow(i, empresas[i]);
                    table.appendChild(row);
                }
            } catch (e) {
                showAlert("Advertencia", "Empresa no encontrada!");
            }
        }
    };
    http.send();
}