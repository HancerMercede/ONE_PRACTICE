// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function edad(e) {
    edadMS = Date.parse(Date()) - Date.parse(e.target.value);
    edads = new Date();
    edads.setTime(edadMS);
    resultado = edads.getFullYear() - 1970;
    res = (resultado <= 0) ? 0 : resultado; // Para evitar que sea negativo
    document.getElementById('eda').innerHTML = res;
}

ShowInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

