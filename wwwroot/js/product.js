var dataTable;

$(document).ready(function () {
    console.log('Document ready');
    loadDataTable();
});

function loadDataTable() {
    dataTable=
    $('#tblTable').DataTable({
        "ajax": { "url": "/admin/product/getall" },
        "columns": [
            { "data": "title", "width": "25%" },
            { "data": "isbn", "width": "15%" },
            { "data": "listPrice", "width": "10%" },
            { "data": "author", "width": "15%" },
            { "data": 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function(data){
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                    <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                    </div>`;
                },
                "width":"25%"
            }
        ],
        "drawCallback": function() {
            // Apply inline CSS styles here
            $('.dataTables_filter input').css({
                'background-color': '#333',
                'color': 'white',
                'border': '1px solid #444'
            });

            $('.dataTables_paginate .paginate_button').css({
                'background-color': '#333',
                'color': 'white',
                'border': '1px solid #444'
            }).hover(function () {
                $(this).css({
                    'background-color': '#555',
                    'color': 'white'
                });
            }, function () {
                $(this).css({
                    'background-color': '#333',
                    'color': 'white'
                });
            });

            $('#tblTable thead th').css({
                'background-color': '#222',
                'color': 'white'
            });

            $('#tblTable tbody tr').css({
                'background-color': '#333',
                'color': 'white'
            }).hover(function () {
                $(this).css('background-color', '#444');
            }, function () {
                $(this).css('background-color', '#333');
            });

            // Apply styles to DataTables pre-defined text
            $('.dataTables_info, .dataTables_empty, .dataTables_paginate .paginate_button, .dataTables_filter label').css({
                'color': 'white'
            });

            $('.dataTables_wrapper').css({
                'color': 'white'
            });

            // Style the "Show entries" dropdown and labels
            $('.dataTables_length label').css({
                'color': 'white'
            });

            // Ensure dropdown label and text are visible
            $('.dataTables_length').css({
                'color': 'white'
            });

            $('.dataTables_length').find('label').css({
                'color': 'white'
            });
        }
    });
}
function Delete(url){
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
      }).then((result) => {
        if (result.isConfirmed) {
         $.ajax({
            url: url,
            type:'DELETE',
            success: function(data)
            {
                dataTable.ajax.reload();
                toastr.success(data.message);
            }
         })
        }
      })


}