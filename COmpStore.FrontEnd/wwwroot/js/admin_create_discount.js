
$(document).ready(function () {

    var numOfCurrentProductRows = $('#import_tickets_table > tbody tr').length;

    // Init bootstrap multiselect
    $('#products_select').multiselect({
        enableFiltering: true,
        filterBehavior: 'text',
        buttonWidth: '100%',
        nonSelectedText: 'Chọn sản phẩm',
        filterPlaceholder: 'Nhập tên sản phẩm...',
        buttonText: function (options, select) {
            var text = 'Chọn sản phẩm';
            var numOfSelected = options.length;

            if (numOfSelected > 0) {
                text = 'Đã chọn: ' + numOfSelected;
            }

            return text;
        },
        onChange: function (option, checked) {
            var products = $('#products_select option:selected');
            var selected = [];

            var product = {
                id: $(option).val(),
                name: $(option).text(),
                price: 0
            }

            if (checked) {
                $.ajax({
                    type: 'GET',
                    url: '/admin/product/get-product-price/' + product.id,
                    dataType: 'json',
                    success: function (result) {
                        product.price = result;
                        addProductRow(product);
                    },
                    error: function () {
                        addProductRow(product);
                        console.log('Something wrong when get product thumbnail!');
                    }
                });
            }
            else {
                removeProductRow(product.id);
            }
        }
    });


    $(document).on('change', '#import_tickets_table > tbody .import-quantity, #import_tickets_table > tbody .import-price', function () {
        reCalculateTotal();
    });

    $(document).on('click', '.remove-import-product', function () {
        var currentRowId = $(this).closest('tr').data('product-id');
        removeProductRow(currentRowId);
    });

    $('#importTicketForm').submit(function () {
        if (numOfCurrentProductRows == 0) {
            bootbox.alert("Vui lòng chọn sản phẩm muốn nhập vào kho!");

            return false;
        }
        else {
            bootbox.confirm("Bạn có chắc muốn nhập những sản phẩm này vào kho? Lưu ý sau khi nhập hàng thành công sẽ không thể hoàn tác", function (result) {
                if (result) {
                    return true;
                }

                return false;
            });
        }
    });

    function addProductRow(product) {
        var percent = parseInt($('#Percent').val());
        percent = isNaN(percent) ? 0 : percent;
        var $tableContent = $('#import_tickets_table > tbody');
        var productRowHtml = '<tr data-product-id="' + product.id + '">' +
            '<td class="import-row-index">' + (numOfCurrentProductRows + 1) + '</td>' +
            '<td>' + product.name + '<input type="hidden" class="import-product-name" value="' + product.name + '" /><input type="hidden" class="import-product-id" value="' + product.id + '"/>' +
            '<input type="hidden" class="import-product-price" value="' + product.price + '" /></td>'+
            '<td class="price-before-discount">' + addCommas(product.price) + '</td>' +
            '<td class="price-after-discount">'+addCommas(product.price - product.price*percent/100)+'</td>' +
            '<td class="text-center"><a class="text-danger remove-import-product" title="Xóa"><span class="glyphicon glyphicon-remove"></span></a></td>' +
            '</tr >';

        $tableContent.append(productRowHtml);

        calculateRowIndex(product.id);
        reCalculateTotal();
    }

    function removeProductRow(productId) {
        var $productRow = $('#import_tickets_table > tbody tr[data-product-id="' + productId + '"]');
        if ($productRow) {
            var $afterProductRows = $productRow.nextAll();
            $productRow.remove();

            $('#products_select').multiselect('deselect', [productId]);

            $afterProductRows.each(function () {
                var productId = $(this).data('product-id');
                console.log(productId);
                calculateRowIndex(productId);
            });

            reCalculateTotal();
        }
    }

    function reCalculateTotal() {

        updateCurrentRows();

        var $tfoot = $('#import_tickets_table > tfoot');
        var $totalImportPrice = $('#totalImportPrice');
        var $totalQuantity = $('#totalQuantity');
        var totalQuantity = 0, totalImportPrice = 0;

        totalQuantity = sumOfColumn($('#import_tickets_table > tbody .import-quantity'));
        totalImportPrice = sumOfColumn($('#import_tickets_table > tbody .import-price'));

        if (numOfCurrentProductRows > 0) {
            $totalQuantity.html(totalQuantity);
            $totalImportPrice.html(totalImportPrice);
            $tfoot.show();
        }
        else {
            $tfoot.hide();
        }
    }

    function sumOfColumn($selector) {
        var result = 0;
        $selector.each(function () {
            var temp = parseInt($(this).val());
            if (temp == undefined || temp == 0 || temp == null) {
                return true;
            }
            result += temp;
        });
        return result;
    }

    function updateCurrentRows() {
        numOfCurrentProductRows = $('#import_tickets_table > tbody tr').length;
    }

    function calculateRowIndex(handlingProductId) {
        var $thisProductRow = $('#import_tickets_table > tbody tr[data-product-id="' + handlingProductId + '"]');
        var index = $thisProductRow.index();

        var $orderCol = $thisProductRow.find('td.import-row-index');
        var $productIdCol = $thisProductRow.find('td .import-product-id');
        var $productNameCol = $thisProductRow.find('td .import-product-name');
        var $productPriceCol = $thisProductRow.find('td .import-product-price');

        $thisProductRow.attr('data-row-index', index);
        $productIdCol.attr('name', 'DiscountDetails[' + index + '].ProductId');
        $productNameCol.attr('name', 'DiscountDetails[' + index + '].Product.Name');
        $productPriceCol.attr('name', 'DiscountDetails[' + index + '].Product.Prices[0].Price');
        $orderCol.text(index + 1);
    }

    $(function () {
        $('#datetimepicker6').datetimepicker();
        $('#datetimepicker7').datetimepicker({
            useCurrent: false //Important! See issue #1075
        });
        $("#datetimepicker6").on("dp.change", function (e) {
            $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
        });
        $("#datetimepicker7").on("dp.change", function (e) {
            $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
        });
    });

    $('#Percent').on('blur', function () {
        reCaculatePriceAfterDiscount();
    })

    selectProductOnload();
});

function reCaculatePriceAfterDiscount() {
    var percent = parseInt($('#Percent').val());
    percent = isNaN(percent) ? 0 : percent;
    var priceAfterDiscounts = $('.price-after-discount');
    priceAfterDiscounts.each(function () {
        var priceBeforeDiscount = parseInt($(this).siblings('.price-before-discount').data('price-before-discount'));
        $(this).text(addCommas(priceBeforeDiscount - priceBeforeDiscount * percent / 100));
    })
}

function selectProductOnload() {
    var indexes = $('.import-product-id');
    if (indexes.length > 0) {
        indexes.each(function () {
            $('#products_select').multiselect('select', [parseInt($(this).val())]);
        })
    }
    
}

function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

