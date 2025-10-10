function showButtonSpinner(buttonSelector) {
    var $btn = $(buttonSelector);
    $btn.children(':not(.spinner-border)').addClass('d-none');
    $btn.find('.spinner-border').removeClass('d-none');
    $btn.prop('disabled', true);
}

function hideButtonSpinner(buttonSelector) {
    var $btn = $(buttonSelector);
    $btn.children(':not(.spinner-border)').removeClass('d-none');
    $btn.find('.spinner-border').addClass('d-none');
    $btn.prop('disabled', false);
}
