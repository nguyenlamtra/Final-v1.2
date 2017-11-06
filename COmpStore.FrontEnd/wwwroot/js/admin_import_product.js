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
                image: 'empty-img.png'
            }

            if (checked) {
                $.ajax({
                    type: 'GET',
                    url: '/admin/product/get-product-thumbnail/' + product.id,
                    dataType: 'json',
                    success: function (result) {
                        if (result != '') {
                            product.image = result;
                        }
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

    $('#importTicketForm').submit(function (e) {
        var currentForm = this;
        e.preventDefault();

        if (numOfCurrentProductRows == 0) {
            bootbox.alert("Vui lòng chọn sản phẩm muốn nhập vào kho!");

            return false;
        }
        else {
            bootbox.confirm("Bạn có chắc muốn nhập những sản phẩm này vào kho? Lưu ý sau khi nhập hàng thành công sẽ không thể hoàn tác", function (result) {
                if (result) {
                    currentForm.submit();
                }
            });
        }
    });

    function addProductRow(product) {
        var $tableContent = $('#import_tickets_table > tbody');
        var productRowHtml = '<tr data-product-id="' + product.id + '">' +
            '<td class="import-row-index">' + (numOfCurrentProductRows + 1) + '</td>' +
            '<td>' + product.name + '<input type="hidden" class="import-product-name" value="' + product.name + '" /><input type="hidden" class="import-product-id" value="' + product.id + '"/></td>' +
            '<td><img class="img-thumbnail" src="/images/products/' + product.image + '" /><input type="hidden" value="' + product.image + '" class="import-product-image" /></td>' +
            '<td><input value="1" class="form-control import-quantity" type="number" /><span class="text-danger"></span></td>' +
            '<td><input value="100000" class="form-control import-price" type="number" /><span class="text-danger"></td>' +
            '<td><textarea class="form-control import-note"></textarea></td>' +
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
        var $productImageCol = $thisProductRow.find('td .import-product-image');
        var $importNoteCol = $thisProductRow.find('td .import-note');
        var $productPriceCol = $thisProductRow.find('td .import-price');
        var $productQuantityCol = $thisProductRow.find('td .import-quantity');

        $thisProductRow.attr('data-row-index', index);
        $productIdCol.attr('name', '[' + index + '].ProductId');
        $productNameCol.attr('name', '[' + index + '].ProductName');
        $productImageCol.attr('name', '[' + index + '].ProductImage');
        $importNoteCol.attr('name', '[' + index + '].Note');
        $productPriceCol.attr('name', '[' + index + '].ImportPrice');
        //$productPriceCol.attr('name', '[' + index + '].ImportPrice');
        $productQuantityCol.attr('name', '[' + index + '].Quantity');
        //$productQuantityCol.attr('name', '[' + index + '].Quantity');
        $orderCol.text(index + 1);
    }
});

