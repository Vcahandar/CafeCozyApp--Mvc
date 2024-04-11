$(document).ready(function () {

    $(".sweet-delete").click(function (e) {
        e.preventDefault();

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                let url = $(this).attr("href");
                fetch(url).then(response => {
                    if (response.ok) {
                        window.location.reload();
                        Swal.fire(
                            'Your product has been succesfully deleted.'
                        )
                    }
                    else {
                        console.log(response);
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Please, select right product!',
                            footer: '<a href="">Product not found!</a>'
                        })
                    }
                })
                
            }
        })
    })

})