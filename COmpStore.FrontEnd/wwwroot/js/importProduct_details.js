$('.insert-history').on('click', function () {
    var id = parseInt($(this).data('id'));
    $.ajax({
        url: '/ImportProduct/ImportTicketHistory',
        data: { 'productId': id },
        type: 'POST',
        success: function (data) {
            $('#partialView').html(data);
        },
        error: function () {
            console.log('error');
        }
    })
})
