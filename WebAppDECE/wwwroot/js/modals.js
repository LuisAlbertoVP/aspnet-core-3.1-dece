
function showAlert(title, body) {
    document.getElementById('titleModalPrincipal').innerHTML = title;
    document.getElementById('bodyModalPrincipal').innerHTML = body;
    $('#ModalPrincipal').modal('show');
}

function createCustomModal(title) {
    createModal(title);
    document.getElementById('title' + title).innerHTML = title;
    return {
        nodes: function (element) {
            var body = document.getElementById('body' + title);
            if (element instanceof Element) {
                while (body.hasChildNodes()) {
                    body.removeChild(bodyModal.lastChild);
                }
                body.appendChild(element);
            } else {
                body.innerHTML = element;
            }       
        },
        fire: function () {
            $('#' + title).modal('show');
        },
        onExit: function (fun) {
            $('#' + title).on('hidden.bs.modal', function (e) {
                fun();
            });
        }
    };
}

function createModal(id) {
    var principal = document.getElementById('myModal');

    var modal = document.createElement('div');
    var dialog = document.createElement('div');
    var content = document.createElement('div');
    var header = document.createElement('div');
    var body = document.createElement('div');
    var footer = document.createElement('div');

    modal.classList.add('modal', 'fade');
    modal.id = id;
    modal.tabIndex = -1;
    modal.setAttribute('role', 'dialog');
    modal.setAttribute('aria-hidden', 'true');
    modal.style.setProperty('z-index', '999', 'important');

    dialog.className = 'modal-dialog';
    dialog.setAttribute('role', 'document');

    content.className = 'modal-content';

    header.className = 'modal-header';
    var title = document.createElement('h5');
    title.id = 'title' + id;
    var button = document.createElement('button');
    button.type = 'button';
    button.className = 'close';
    button.setAttribute('data-dismiss', 'modal');
    button.setAttribute('aria-label', 'Close');
    var span = document.createElement('span');
    span.setAttribute('aria-hidden', 'true');
    span.innerHTML = '&times;';
    button.appendChild(span);
    header.append(title, button);

    body.className = 'modal-body';
    body.id = 'body' + id;

    footer.className = 'modal-footer';
    var button2 = document.createElement('button');
    button2.type = 'button';
    button2.setAttribute('data-dismiss', 'modal');
    button2.classList.add('btn', 'btn-primary');
    button2.innerHTML = 'Aceptar';
    var button3 = document.createElement('button');
    button3.type = 'button';
    button3.setAttribute('data-dismiss', 'modal');
    button3.classList.add('btn', 'btn-danger');
    button3.innerHTML = 'Cerrar';
    footer.append(button2);

    content.append(header, body, footer);
    dialog.appendChild(content);
    modal.appendChild(dialog);

    principal.appendChild(modal);
}

createModal("ModalPrincipal");