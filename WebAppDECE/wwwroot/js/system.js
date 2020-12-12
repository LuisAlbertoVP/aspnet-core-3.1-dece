var host = "https://localhost:44398";

$(document).ready(function () {
    var menu_principal = document.getElementById('menu_principal');


    function back() {
        let li = document.createElement('li');
        li.className = 'go-back';
        li.innerHTML = 'Atr&aacute;s';
        return li;
    }

    function administrarHead() {
        let a = document.createElement('a');
        a.setAttribute('href', '#');
        a.classList.add('fa', 'fa-wrench', 'icon-menu');
        return a;
    }

    function administrarBody() {
        let li = document.createElement('li');
        li.className = 'title-menu';
        li.classList.add('fa', 'fa-wrench', 'icon-menu');
        return li;
    }

    function logout() {
        let li = document.createElement('li');
        let a = document.createElement('a');
        a.href = "/Home/LogOut";
        a.innerHTML = '<span class="fa fa-sign-out icon-menu"></span>Cerrar Sesi&oacute;n';
        li.appendChild(a);
        return li;
    }

    function checkModulo(id) {
        let modulo = document.querySelector('[data-menu-modulo="' + id + '"]');
        if (modulo) {
            return modulo;
        }
        return null;
    }

    function checkOption(id) {
        let option = document.querySelector('[data-menu-opcion="' + id + '"]');
        if (option) {
            return option;
        }
        return null;
    }

    function createModulo(modulo) {
        let li = checkModulo(modulo['id']);
        if (li != null) {
            return li;
        }
        li = document.createElement('li');
        li.setAttribute("data-menu-modulo", modulo['id']);
        li.className = 'item-submenu';
        li.setAttribute('menu', modulo['id']);
        let head = administrarHead();
        head.innerHTML = ' &nbsp; ' + modulo['descripcion'];
        let ul = document.createElement('ul');
        ul.className = 'submenu';
        let body = administrarBody();
        body.innerHTML = ' &nbsp; ' + modulo['descripcion'];
        ul.append(body, back());
        li.append(head, ul);
        return li;
    }

    function createOption(opcion) {
        let li = checkOption(opcion['id']);
        if (li != null) {
            return li;
        }
        li = document.createElement('li');
        li.setAttribute("data-menu-opcion", opcion['id']);
        li.setAttribute('smenu', opcion['orden']);
        let a = document.createElement('a');
        a.href = opcion['rutaDll'];
        a.innerHTML = ' &nbsp; ' + opcion['descripcionLarga'];
        li.appendChild(a);
        return li;
    }

    function createMenu(modulos) {
        for (let i = 0; i < modulos.length; i++) {

            let li_mod = createModulo(modulos[i]);

            let opciones = modulos[i]['opciones'];
            for (let j = 0; j < opciones.length; j++) {
                let li_op = createOption(opciones[j]);
                           
                if (checkOption(opciones[j]['id']) == null) {
                    li_mod.lastChild.appendChild(li_op);
                }
            }
            if (checkModulo(modulos[i]['id']) == null) {
                menu_principal.appendChild(li_mod);
            }
        }
    }

    function generateMenu() {
        var http = new XMLHttpRequest();
        http.open("GET", host + "/Menu/GetMenu", false);
        http.setRequestHeader("Content-Type", "application/json");

        http.onreadystatechange = function () {
            if (http.readyState === 4 && http.status === 200) {
                var json = http.responseText;
                try {
                    var listaModulos = JSON.parse(json);
                    for (var i = 0; i < listaModulos.length; i++) {
                        createMenu(listaModulos[i]);
                    }
                } catch (e) {}
            }
        };
        http.send();
    }

    generateMenu();

    menu_principal.appendChild(logout());

});