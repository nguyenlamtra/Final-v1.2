function getSelectedValue(listDropdownItem) {
    for (var i = 0; i < listDropdownItem.length; i++) {
        var item = listDropdownItem[i];
        if ($(item).hasClass('active')) {
            id = $(item).data('value');
            return id;
        }
    }
    return 0;
}

function filter(resetPageNumber) {

    var subCategoryIds = $('.sub-category-id');
    var priceLevels = $('.price-level');
    var sortOptions = $('.sort-by');
    var numberProductPerPages = $('.number-product-per-page');
    var pageNumbers = $('.page-number');

    var subCategoryId = getSelectedValue(subCategoryIds);
    var priceLevel = getSelectedValue(priceLevels);
    var sortOption = getSelectedValue(sortOptions);
    var numberProductPerPage = getSelectedValue(numberProductPerPages);
    var pageNumber = getSelectedValue(pageNumbers);

    if (priceLevel !== 0)
        $('#priceLevel').val(priceLevel);
    if (subCategoryId !== 0)
        $('#subCategoryId').val(subCategoryId);
    if (sortOption !== 0)
        $('#sortBy').val(sortOption);
    if (numberProductPerPage !== 0)
        $('#numberProductPerPage').val(numberProductPerPage);

    if (pageNumber !== 0)
        $('#pageNumber').val(pageNumber);
    if (resetPageNumber === true)
        $('#pageNumber').val(1);
    if ($('#next-page').hasClass('active')) {
        var temp = parseInt($('#pageNumber').val());
        $('#pageNumber').val(temp + 3)
    }

    if ($('#previous-page').hasClass('active')) {
         temp = parseInt($('#pageNumber').val());
        $('#pageNumber').val(temp - 3)
    }


    $('#form').submit();

}

$(document).ready(function () {
    $('.sub-category-id').on('click', function () {
        $('.sub-category-id').removeClass('active selected');
        $(this).addClass('active selected');
        filter(true);
    })

    $('.price-level').on('click', function () {
        $('.price-level').removeClass('active selected');
        $(this).addClass('active selected');

        filter(true);
    })

    $('.sort-by').on('click', function () {
        $('.sort-by').removeClass('active selected');
        $(this).addClass('active selected');

        filter(true);
    })

    $('.number-product-per-page').on('click', function () {
        $('.number-product-per-page').removeClass('active selected');
        $(this).addClass('active selected');

        filter(true);
    })

    $('.page-number').on('click', function () {
        $('.page-number').removeClass('active selected');
        $(this).addClass('active selected');

        filter(false);
    })

    $('#next-page').on('click', function () {
        $('#next-page').addClass('active selected');
        filter(false);
    })

    $('#previous-page').on('click', function () {
        $('#previous-page').addClass('active selected');
        filter(false);
    })

    $('.add-to-card').click(function () {
        var productId = parseInt($(this).parents('.product-id').data('id'));
        $.ajax({
            url: '/ShoppingCart/AddCart',
            data: JSON.stringify({ productId: productId, quantity: 1 }),
            type: 'POST',
            contentType: 'application/json',
            dataType: '',
            success: function (status) {
                if (status === 'success' || status === 'success-increase') {
                    var $messageSelector = $('#addToCartMessage');
                    if ($messageSelector.is(':visible')) {
                        $messageSelector.promise().done(setTimeout(function () {
                            $messageSelector.fadeOut();
                        }, 3000));
                    } else {
                        $messageSelector.show(200).promise().done(setTimeout(function () {
                            $messageSelector.fadeOut();
                        }, 3000));
                    }
                  
                    //if (status === 'success-increase') {
                    var selectedProducts = parseInt($('#shopCart').text());
                    selectedProducts = isNaN(selectedProducts) ? 0 : selectedProducts;
                    //}
                    $('#shopCart').text(selectedProducts + 1).append('<i class="shop bag icon"></i>');

                } else {
                    console.log('product null');
                }
            },
            error: function () {
                console.log('error');
            }
        })

    });

    $('#GetEmail').modal('show').modal({
        onHidden: function () {
            $('#email').val('noValue');
            SendEmail();
        }
    });

    $('#OrderSuccess').modal('show');

    $('#email').keypress(function (e) {
        var key = e.which;
        if (key === 13)  // the enter key code
        {
            SendEmail();
            return false;
        }
    });
})

function SendEmail() {
    $.ajax({
        url: '/Home/GetEmail',
        data: { 'email': $('#email').val() },
        type: 'POST',
        success: function () {
            console.log('success');
        },
        error: function () {
            console.log('error');
        }
    })
}