

setTimeout(function () {
    $(".alert").fadeOut("slow", function () {
        $(this).alert('close');
    })
}, 5000);


$(document).ready(function () {
    $('#tblAssociados').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum registro disponivel",
            "info": "_START_ de _END_ em _TOTAL_ registros",
            "infoEmpty": "0 de 0 em 0 registros",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ registros",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "Nenhum registro encontrado...",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Proximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });
});

$(document).ready(function () {
    $('#tblEmpresas').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum registro disponivel",
            "info": "_START_ de _END_ em _TOTAL_ registros",
            "infoEmpty": "0 de 0 em 0 registros",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ registros",
            "loadingRecords": "Carregando...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "Nenhum registro encontrado...",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Proximo",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            }
        }
    });
});