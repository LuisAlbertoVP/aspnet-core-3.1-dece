$(document).ready(function () {

    var modal = createCustomModal("Empresas");

    var inputEmpresa = document.getElementById('empresa');
    var inputEmpresaNombre = document.getElementById('empresa-nombre');

    var http = new XMLHttpRequest();
    http.open("GET", host + "/Empresa/GetEmpresas/-1", true);
    http.setRequestHeader("Content-Type", "application/json");

    http.onreadystatechange = function () {
        if (http.readyState === 4 && http.status === 200) {
            var json = http.responseText;
            try {
                var empresas = JSON.parse(json);
                var div = document.createElement('div');
                for (var i = 0; i < empresas.length; i++) {
                    var emp = empresas[i];
                    var label = document.createElement('label');
                    label.innerHTML = emp['nombre'] + '&nbsp;';
                    var radio = document.createElement('input');
                    radio.type = 'radio';
                    radio.name = 'emp';
                    radio.value = emp['id'];
                    radio.setAttribute('data-empresa', emp['nombre']);
                    if (inputEmpresa.value == emp['id']) {
                        radio.checked = true;
                        inputEmpresa.value = emp['id'];
                        inputEmpresaNombre.value = emp['nombre'];
                    }
                    div.append(label, radio, document.createElement('br'));
                }
                modal.nodes(div);
            } catch (e) {
                showAlert("Advertencia", "Empresas no encontradas!");
            }
        }
    };

    http.send();


    $("#admin").click(function (e) {
        e.preventDefault();
        modal.fire();
    });

    modal.onExit(function () {
        var radios = document.getElementsByName('emp');
        for (var i = 0; i < radios.length; i++) {
            if (radios[i].checked) {
                inputEmpresa.value = radios[i].value;
                inputEmpresaNombre.value = radios[i].getAttribute('data-empresa');
            }
        }
    });
});
